namespace DuckLib.Configs.Location
{
    public class RemoteLocation : ILocation
    {
        public string GetPath(string file, string extension)
        {
            return file;
        }
    }
}