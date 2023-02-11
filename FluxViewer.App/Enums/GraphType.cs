namespace FluxViewer.App.Enums;

public enum GraphType
{
    FluxGraph,
    TemperatureGraph,
    PressureGraph,
    HumidityGraph,
}

public static class GraphTypeHelper
{
    public static string ToString(GraphType graphType)
    {
        return graphType switch
        {
            GraphType.FluxGraph => "Электростатическое поле, В/м",
            GraphType.TemperatureGraph => "Температура, гр. С",
            GraphType.PressureGraph => "Давление, мм. рт. ст.",
            GraphType.HumidityGraph => "Влажность, %",
            _ => throw new ArgumentOutOfRangeException(nameof(graphType), graphType, null)
        };
    }

    public static GraphType FromString(string value)
    {
        return value switch
        {
            "Электростатическое поле, В/м" => GraphType.FluxGraph,
            "Температура, гр. С" => GraphType.TemperatureGraph,
            "Давление, мм. рт. ст." => GraphType.PressureGraph,
            "Влажность, %" => GraphType.HumidityGraph,
            _ => throw new ArgumentException("Неизвестный тип графика")
        };
    }
}