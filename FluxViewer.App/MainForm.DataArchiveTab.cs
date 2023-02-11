using System.Media;
using FluxViewer.App.Enums;
using FluxViewer.DataAccess.Controllers;
using FluxViewer.DataAccess.Models;
using ZedGraph;
using static System.String;

namespace FluxViewer.App;

/// <summary>
/// UI-логика, связанная с вкладкой "АРХИВ ДАННЫХ"
/// </summary>
partial class MainForm
{
    // Первый раз рендерится блок "Детализация"
    private void daNumOfPointsGroupBox_Layout(object sender, LayoutEventArgs e)
    {
        var numOfPoints = CalculateNumOfPoint();
        daNumOfPointsGroupBox.Text = $@"Детализация: {numOfPoints} точек";
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
        _list.Clear();
        _list.AddRange(points);

        _pane[4].Title.Text = daChannelNameComboBox.Text;
        _pane[4].XAxis.Type = AxisType.Date;
        _pane[4].YAxis.Scale.MinAuto = true;
        _pane[4].YAxis.Scale.MaxAuto = true;
        if (daXAutoscalingCheckBox.Checked)
        {
            _pane[4].XAxis.Scale.MinAuto = true;
            _pane[4].XAxis.Scale.MaxAuto = true;
        }
        else
        {
            _pane[4].XAxis.Scale.MinAuto = false;
            _pane[4].XAxis.Scale.MaxAuto = false;
            _pane[4].XAxis.Scale.Min = new XDate(controller.beginDate);
            _pane[4].XAxis.Scale.Max = new XDate(controller.endDate);
        }

        _pane[4].IsBoundedRanges = true;

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
        return 100000 / daNumOfPointsTrackBar.Maximum * normalizePosition;  // TODO: 100000 в константы!
    }

    private static IEnumerable<PointPair> GetGraphPointsFromDataBatchByGraphType(List<NewData> dataBatch, GraphType graphType)
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