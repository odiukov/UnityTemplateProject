namespace DuckLib.Configs.Location
{
    public abstract class LocalLocation : ILocation
    {
        protected const string ConfigsFolder = "Configs/";
        protected string GetNameWithExtension(string name, string fileExtension)
        {
            return $"{name}.{fileExtension}";
        }
        public abstract string GetPath(string file, string extension);
    }
}