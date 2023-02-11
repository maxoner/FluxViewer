using System.Media;
using FluxViewer.App.Enums;
using FluxViewer.DataAccess.Controllers;
using FluxViewer.DataAccess.GraphThemes;
using FluxViewer.DataAccess.Models;
using ZedGraph;
using static System.String;

namespace FluxViewer.App;

/// <summary>
/// UI-логика, связанная с вкладкой "АРХИВ ДАННЫХ"
/// </summary>
partial class MainForm
{
    private GraphController _daGraphController;     // Контроллер, отвечающий за график на данной вкладке
    private PointPairList _daGraphPoints; // Точки графика

    private void InitDataArchiveTab()
    {
        _daGraphController = new GraphController(daMainZedGraphControl.GraphPane);
        _daGraphController.SetXAxisTitle("Время мм:cc");
        _daGraphController.SetLineWidth((int) num_linewidth.Value);
        if (check_grid.Checked)
            _daGraphController.ShowGrid();
        if (rb_templot_1.Checked)
            _daGraphController.SetGraphTheme(new WhiteGraphTheme());
        if (rb_templot_2.Checked)
            _daGraphController.SetGraphTheme(new BlackGraphTheme());

        InitChannelNameComboBox(); // Выставляем все доступные виды каналов для постройки графиков

        // Выставляем даты текущими по умолчанию
        daBeginDateDateTimePicker.Value = DateTime.Now.Date;
        daEndDateDateTimePicker.Value = DateTime.Now.Date;

        // Генерим заголовок над крутилкой точек        
        daNumOfPointsGroupBox.Text = $@"Детализация: {CalculateNumOfPoint()} точек";
    }

    private void InitChannelNameComboBox()
    {
        foreach (var graphType in Enum.GetValues<GraphType>())
        {
            daChannelNameComboBox.Items.Add(GraphTypeHelper.ToString(graphType));
        }

        daChannelNameComboBox.SelectedIndex = 0;
    }

    // Изменили "Дата начала"
    private void daBeginDateDateTimePicker_ValueChanged(object sender, EventArgs e)
    {
        CheckAndChangeDatesInDataArchiveTab();
        RedrawGraph();
    }

    // Изменили "Дата конца"
    private void daEndDateDateTimePicker_ValueChanged(object sender, EventArgs e)
    {
        CheckAndChangeDatesInDataArchiveTab();
        RedrawGraph();
    }

    // Выбрали нужный канал для отображения из списка каналов
    private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
    {
        RedrawGraph();
    }

    // Изменили ползунок "Количество точек"
    private void daNumOfPointsTrackBar_Scroll(object sender, EventArgs e)
    {
        var numOfPoints = CalculateNumOfPoint();
        daNumOfPointsGroupBox.Text = $@"Детализация: {numOfPoints} точек";
        RedrawGraph();
    }

    // Нажали кнопку "Автозум"
    private void btn_achive_autozoom_Click(object sender, EventArgs e)
    {
        _daGraphController.Autozoom();
        daMainZedGraphControl.AxisChange();
        daMainZedGraphControl.Invalidate();
    }

    // Нажали кнопку "Приблизить"
    private void btn_achive_plus_Click(object sender, EventArgs e)
    {
        _daGraphController.ZoomYAxis();
        daMainZedGraphControl.AxisChange();
        daMainZedGraphControl.Invalidate();
    }

    // Нажали кнопку "Отдалить"
    private void btn_achive_minus_Click(object sender, EventArgs e)
    {
        _daGraphController.ReduceYAxis();
        daMainZedGraphControl.AxisChange();
        daMainZedGraphControl.Invalidate();
    }

    // Нажали кнопку "Вверх"
    private void btn_achive_up_Click(object sender, EventArgs e)
    {
        _daGraphController.ScrollUpYAxis();
        daMainZedGraphControl.AxisChange();
        daMainZedGraphControl.Invalidate();
    }

    // Нажали кнопку "Вниз"
    private void btn_achive_down_Click(object sender, EventArgs e)
    {
        _daGraphController.ScrollDownYAxis();
        daMainZedGraphControl.AxisChange();
        daMainZedGraphControl.Invalidate();
    }

    private void CheckAndChangeDatesInDataArchiveTab()
    {
        var beginDate = daBeginDateDateTimePicker.Value.Date;
        var endDate = daEndDateDateTimePicker.Value.Date;
        if (beginDate <= endDate)
            return;

        // Не даём пользователю начальную дату сделать большей, чем конечную
        SystemSounds.Beep.Play();
        daBeginDateDateTimePicker.Value = endDate;
    }

    private void RedrawGraph()
    {
        var controller = GetDataArchiveController();
        var dataBatch = new List<NewData>();

        if (controller.HasDataBetweenTwoDates())
        {
            var numOfPoints = CalculateNumOfPoint();
            dataBatch.AddRange(controller.GetDataBetweenTwoDates(numOfPoints));
        }

        var channelName = daChannelNameComboBox.SelectedItem.ToString();
        var graphType = GraphTypeHelper.FromString(channelName ?? Empty);
        var points = GetGraphPointsFromDataBatchByGraphType(dataBatch, graphType);

        _daGraphController.ClearPoints();
        _daGraphController.AddPoints(points);
        _daGraphController.SetGraphTitle(daChannelNameComboBox.Text);
        _daGraphController.AutozoomY();
        if (daXAutoscalingCheckBox.Checked)
        {
            _daGraphController.AutozoomX();
        }
        else
        {
            _daGraphController.ManuallySetXAxis(controller.beginDate, controller.endDate);
        }

        daMainZedGraphControl.AxisChange();
        daMainZedGraphControl.Invalidate();
    }

    private DataArchiveController GetDataArchiveController()
    {
        return new DataArchiveController(
            new DateTime(
                daBeginDateDateTimePicker.Value.Year,
                daBeginDateDateTimePicker.Value.Month,
                daBeginDateDateTimePicker.Value.Day, 00, 00, 00, 000),
            new DateTime(
                daEndDateDateTimePicker.Value.Year,
                daEndDateDateTimePicker.Value.Month,
                daEndDateDateTimePicker.Value.Day, 23, 59, 59, 999),
            _storage
        );
    }

    private int CalculateNumOfPoint()
    {
        var normalizePosition = daNumOfPointsTrackBar.Value;
        return 100000 / daNumOfPointsTrackBar.Maximum * normalizePosition; // TODO: 100000 в константы!
    }

    private static IEnumerable<PointPair> GetGraphPointsFromDataBatchByGraphType(List<NewData> dataBatch,
        GraphType graphType)
    {
        var points = new List<PointPair>();
        foreach (var data in dataBatch)
        {
            switch (graphType)
            {
                case GraphType.FluxGraph:
                    points.Add(new PointPair(new XDate(data.DateTime), data.FluxSensorData));
                    break;
                case GraphType.TemperatureGraph:
                    points.Add(new PointPair(new XDate(data.DateTime), data.TempSensorData));
                    break;
                case GraphType.PressureGraph:
                    points.Add(new PointPair(new XDate(data.DateTime), data.PressureSensorData));
                    break;
                case GraphType.HumidityGraph:
                    points.Add(new PointPair(new XDate(data.DateTime), data.HumiditySensorData));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(graphType), graphType, null);
            }
        }

        return points;
    }
}