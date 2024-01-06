using System.Drawing;

namespace FluxViewer.Core.GraphThemes;

public abstract class GraphTheme
{
    /// <summary>
    /// Цвет рамки для всего компонента
    /// </summary>
    public Color BorderColor;

    /// <summary>
    /// Цвет заливки всего компонента
    /// </summary>
    public Color FillColor;

    /// <summary>
    ///  Цвет рамки вокруг графика
    /// </summary>
    public Color ChartBorderColor;

    /// <summary>
    ///  Цвет заливки рамки графика (когда график активен)
    /// </summary>
    public Color ActiveChartFillColor;
    
    /// <summary>
    ///  Цвет заливки рамки графика (когда график неактивен)
    /// </summary>
    public Color InactiveChartFillColor;
    
    /// <summary>
    /// Цвет линии графика
    /// </summary>
    public Color CurveColor;
    
    /// <summary>
    /// Цвет оси X
    /// </summary>
    public Color XAxisColor;

    /// <summary>
    ///  Цвет оси X
    /// </summary>
    public Color YAxisColor;

    /// <summary>
    /// Цвет осей сетки X
    /// </summary>
    public Color XAxisMajorGridColor;

    /// <summary>
    /// Цвет осей сетки X
    /// </summary>
    public Color YAxisMajorGridColor;

    /// <summary>
    /// Цвет подписей рядом с осями
    /// </summary>
    public Color XAxisTitleFontSpecFontColor;

    /// <summary>
    /// Цвет подписей рядом с осями
    /// </summary>
    public Color YAxisTitleFontSpecFontColor;

    /// <summary>
    /// Цвет подписей под метками
    /// </summary>
    public Color XAxisScaleFontSpecFontColor;

    /// <summary>
    /// Цвет подписей под метками
    /// </summary>
    public Color YAxisScaleFontSpecFontColor;

    /// <summary>
    /// Цвет заголовка над графиком
    /// </summary>
    public Color TitleFontSpecFontColor;
}