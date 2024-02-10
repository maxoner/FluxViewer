using System;
using System.Collections.Generic;
using System.Drawing;
using FluxViewer.Core.GraphThemes;
using ZedGraph;

namespace FluxViewer.Core.Controllers;

/// <summary>
/// Класс с бизнес-логикой, отвечающей за любую работу с графиком.
/// </summary>
public class GraphController
{
    private readonly GraphPane _graphPane; // Панель с графиком
    private readonly LineItem _graphCurve; // Кривая на графике
    private readonly PointPairList _graphPoints; // Точки графика
    private GraphTheme _graphTheme;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="graphPane">Графическа панель, с которой будет работать контроллер</param>
    public GraphController(GraphPane graphPane)
    {
        _graphPane = graphPane;
        _graphPoints = new PointPairList();
        _graphCurve = _graphPane.AddCurve("", _graphPoints, Color.Black, SymbolType.None);
        _graphPane.XAxis.Type = AxisType.Date;
    }

    /// <summary>
    /// Добавить точки к графику
    /// </summary>
    /// <param name="pointPairList">Последовательность точек, которая будет добавлена к графику</param>
    public void AddPoints(IEnumerable<PointPair> pointPairList)
    {
        _graphPoints.AddRange(pointPairList);
    }

    /// <summary>
    /// Очистить все точки на графике
    /// </summary>
    public void ClearPoints()
    {
        _graphPoints.Clear();
    }

    /// <summary>
    /// Показать сетку на графике
    /// </summary>
    public void ShowGrid()
    {
        _graphPane.XAxis.MajorGrid.IsVisible = true;
        // Длина штрихов равна 10 пикселям, ...
        _graphPane.XAxis.MajorGrid.DashOn = 10;
        // затем 5 пикселей - пропуск
        _graphPane.XAxis.MajorGrid.DashOff = 5;
        // Включаем отображение сетки напротив крупных рисок по оси Y
        _graphPane.YAxis.MajorGrid.IsVisible = true;
        // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
        _graphPane.YAxis.MajorGrid.DashOn = 10;
        _graphPane.YAxis.MajorGrid.DashOff = 5;
        // Включаем отображение сетки напротив мелких рисок по оси X
        _graphPane.YAxis.MinorGrid.IsVisible = true;
        // Задаем вид пунктирной линии для крупных рисок по оси Y:
        // Длина штрихов равна одному пикселю, ...
        _graphPane.YAxis.MinorGrid.DashOn = 1;
        // затем 2 пикселя - пропуск
        _graphPane.YAxis.MinorGrid.DashOff = 2;
        // Включаем отображение сетки напротив мелких рисок по оси Y
        _graphPane.XAxis.MinorGrid.IsVisible = true;
        // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
        _graphPane.XAxis.MinorGrid.DashOn = 1;
        _graphPane.XAxis.MinorGrid.DashOff = 2;
    }

    /// <summary>
    /// Скрыть сетку на графике
    /// </summary>
    public void HideGrid()
    {
        _graphPane.XAxis.MajorGrid.IsVisible = false;
        _graphPane.YAxis.MajorGrid.IsVisible = false;
        _graphPane.YAxis.MinorGrid.IsVisible = false;
        _graphPane.XAxis.MinorGrid.IsVisible = false;
    }

    /// <summary>
    /// Установить цветовую тему графика.
    /// </summary>
    /// <param name="graphTheme">Цветовая тема графика, которая будет применена</param>
    public void SetGraphTheme(GraphTheme graphTheme)
    {
        _graphTheme = graphTheme;

        _graphPane.Border.Color = graphTheme.BorderColor;
        _graphPane.Chart.Border.Color = graphTheme.ChartBorderColor;

        _graphPane.Fill.Type = FillType.Solid;
        _graphPane.Fill.Color = graphTheme.FillColor;

        _graphPane.Chart.Fill.Type = FillType.Solid;
        _graphPane.Chart.Fill.Color = graphTheme.ActiveChartFillColor;

        _graphCurve.Line.Color = graphTheme.CurveColor;

        _graphPane.XAxis.Color = graphTheme.XAxisColor;
        _graphPane.YAxis.Color = graphTheme.YAxisColor;

        _graphPane.XAxis.MajorGrid.IsZeroLine = true;
        _graphPane.YAxis.MajorGrid.IsZeroLine = true;
        _graphPane.XAxis.MajorGrid.Color = graphTheme.XAxisMajorGridColor;
        _graphPane.YAxis.MajorGrid.Color = graphTheme.YAxisMajorGridColor;

        _graphPane.XAxis.Title.FontSpec.FontColor = graphTheme.XAxisTitleFontSpecFontColor;
        _graphPane.YAxis.Title.FontSpec.FontColor = graphTheme.YAxisTitleFontSpecFontColor;

        _graphPane.XAxis.Scale.FontSpec.FontColor = graphTheme.XAxisScaleFontSpecFontColor;
        _graphPane.YAxis.Scale.FontSpec.FontColor = graphTheme.YAxisScaleFontSpecFontColor;

        _graphPane.Title.FontSpec.FontColor = graphTheme.TitleFontSpecFontColor;
    }

    /// <summary>
    /// Сделать график активным
    /// </summary>
    public void MakeGraphActive()
    {
        if (_graphTheme == null)
            throw new GraphThemeNotFoundException("Не задана тема графика");
        _graphPane.Chart.Fill.Color = _graphTheme.ActiveChartFillColor;
    }

    /// <summary>
    /// Сделать график неактивным
    /// </summary>
    public void MakeGraphInactive()
    {
        if (_graphTheme == null)
            throw new GraphThemeNotFoundException("Не задана тема графика");
        _graphPane.Chart.Fill.Color = _graphTheme.InactiveChartFillColor;
    }

    /// <summary>
    /// Установить заголовок всего графика
    /// </summary>
    /// <param name="graphTitle">Заголовок, отображаемый над графиком</param>
    public void SetGraphTitle(string graphTitle)
    {
        _graphPane.Title.Text = graphTitle;
    }

    /// <summary>
    /// Установить заголовок под осью X
    /// </summary>
    /// <param name="xAxisTitle">Заголовок, который будет размешён од осью X</param>
    public void SetXAxisTitle(string xAxisTitle)
    {
        _graphPane.XAxis.Title.Text = xAxisTitle;
    }

    /// <summary>
    /// Установить толщину линии графика
    /// </summary>
    /// <param name="lineWidth">Желаемая толщина линии графика</param>
    public void SetLineWidth(int lineWidth)
    {
        _graphCurve.Line.Width = lineWidth;
    }

    /// <summary>
    /// Автоматически подобрать размер обоих осей
    /// </summary>
    public void Autozoom()
    {
        _graphPane.YAxis.Scale.MinAuto = true;
        _graphPane.YAxis.Scale.MaxAuto = true;
        _graphPane.XAxis.Scale.MinAuto = true;
        _graphPane.XAxis.Scale.MaxAuto = true;
        _graphPane.IsBoundedRanges = true;
    }

    /// <summary>
    /// Автоматически подобрать размер оси Y
    /// </summary>
    public void AutozoomY()
    {
        _graphPane.YAxis.Scale.MinAuto = true;
        _graphPane.YAxis.Scale.MaxAuto = true;
        _graphPane.IsBoundedRanges = true;
    }

    /// <summary>
    /// Автоматически подобрать размер оси X
    /// </summary>
    public void AutozoomX()
    {
        _graphPane.XAxis.Scale.MinAuto = true;
        _graphPane.XAxis.Scale.MaxAuto = true;
        _graphPane.IsBoundedRanges = true;
    }

    /// <summary>
    /// Вручную установить размер оси X. 
    /// </summary>
    /// <param name="beginDate">Дата начала, с которой начинается ось X</param>
    /// <param name="endDate">Дата конца, которой заканчивается ось X</param>
    public void ManuallySetXAxis(DateTime beginDate, DateTime endDate)
    {
        _graphPane.XAxis.Scale.MinAuto = false;
        _graphPane.XAxis.Scale.MaxAuto = false;
        _graphPane.XAxis.Scale.Min = new XDate(beginDate);
        _graphPane.XAxis.Scale.Max = new XDate(endDate);
        _graphPane.IsBoundedRanges = true;
    }

    /// <summary>
    /// Растянуть ось Y
    /// </summary>
    public void ZoomYAxis()
    {
        var amp = (_graphPane.YAxis.Scale.Max - _graphPane.YAxis.Scale.Min) * 0.1;
        _graphPane.YAxis.Scale.Min += amp;
        _graphPane.YAxis.Scale.Max -= amp;
    }

    /// <summary>
    /// Стянуть ось Y
    /// </summary>
    public void ReduceYAxis()
    {
        var amp = (_graphPane.YAxis.Scale.Max - _graphPane.YAxis.Scale.Min) * 0.1;
        _graphPane.YAxis.Scale.Min -= amp;
        _graphPane.YAxis.Scale.Max += amp;
    }

    /// <summary>
    /// Незначительно прокрутить вверх ось Y.
    /// </summary>
    public void ScrollUpYAxis()
    {
        var amp = (_graphPane.YAxis.Scale.Max - _graphPane.YAxis.Scale.Min) * 0.1;
        _graphPane.YAxis.Scale.Min += amp;
        _graphPane.YAxis.Scale.Max += amp;
    }

    /// <summary>
    /// Незначительно прокрутить вниз ось Y.
    /// </summary>
    public void ScrollDownYAxis()
    {
        var amp = (_graphPane.YAxis.Scale.Max - _graphPane.YAxis.Scale.Min) * 0.1;
        _graphPane.YAxis.Scale.Min -= amp;
        _graphPane.YAxis.Scale.Max -= amp;
    }
}


public class GraphThemeNotFoundException : Exception
{
    public GraphThemeNotFoundException(string message) : base(message)
    {
    }
}