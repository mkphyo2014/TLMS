using ServiceStack.Configuration;
using StructureMap;

namespace TLMS.Infrastructure
{
    /// <summary>
    /// Run your DB related stuff. Aggregate any "list" of concrete classes here.
    /// </summary>
    public interface IPostInit
    {
        void PostInit(IContainer container, IAppSettings settings);
    }
}
