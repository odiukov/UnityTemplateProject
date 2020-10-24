namespace DuckLib.Core.Services
{
    public interface IIdentifierService
    {
        int Next(Identity identity);
    }

    public enum Identity
    {
        General
    }
}