using CodeBase.Infrastructure.Data;
using CodeBase.Services.ServiceLocator;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}