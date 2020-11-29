
namespace DirectoryMonitorService
{
    interface IParse
    {
        T Parse<T>(string path) where T : new();
    }
}
