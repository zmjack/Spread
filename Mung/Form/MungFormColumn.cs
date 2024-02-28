namespace Mung.Form;

public interface IMungFormColumn
{
    /// <summary>
    /// See <see cref="UIComponent" />.
    /// </summary>
    string Type { get; set; }
    string DataIndex { get; set; }
    string Label { get; set; }
    MungFormRule? Rule { get; set; }
}
