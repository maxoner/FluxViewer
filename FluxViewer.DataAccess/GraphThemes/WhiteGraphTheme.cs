using System.Drawing;

namespace FluxViewer.DataAccess.GraphThemes;

public class WhiteGraphTheme : GrapTheme
{
    /// <summary>
    /// Светлая тема графика
    /// </summary>
    public WhiteGraphTheme()
    {
        BorderColor = Color.Black;
        FillColor = Color.White;

        ChartBorderColor = Color.Black;
        ChartFillColor = Color.White;
        
        CurveColor = Color.Blue;
        
        XAxisColor = Color.Black;
        YAxisColor = Color.Black;

        XAxisMajorGridColor = Color.Black;
        YAxisMajorGridColor = Color.Black;

        XAxisTitleFontSpecFontColor = Color.Black;
        YAxisTitleFontSpecFontColor = Color.Black;

        XAxisScaleFontSpecFontColor = Color.Black;
        YAxisScaleFontSpecFontColor = Color.Black;

        TitleFontSpecFontColor = Color.Black;
    }
}