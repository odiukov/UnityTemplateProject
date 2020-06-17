namespace DuckLib.Configs.Location
{
    public interface ILocation
    {
        string GetPath(string file, string extension);
    }
}