using System.Drawing;

namespace FluxViewer.DataAccess.GraphThemes;

public class BlackGraphTheme : GrapTheme
{
    /// <summary>
    /// Тёмная тема графика
    /// </summary>
    public BlackGraphTheme()
    {
        BorderColor = Color.Aqua;
        FillColor = Color.Silver;

        ChartBorderColor = Color.Green;
        ChartFillColor = Color.Black;

        CurveColor = Color.Yellow;
        
        XAxisColor = Color.Gray;
        YAxisColor = Color.Gray;

        XAxisMajorGridColor = Color.Cyan;
        YAxisMajorGridColor = Color.Cyan;

        XAxisTitleFontSpecFontColor = Color.Teal;
        YAxisTitleFontSpecFontColor = Color.Teal;

        XAxisScaleFontSpecFontColor = Color.Black;
        YAxisScaleFontSpecFontColor = Color.Black;

        TitleFontSpecFontColor = Color.Teal;
    }
}