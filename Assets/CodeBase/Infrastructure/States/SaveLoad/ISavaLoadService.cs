using CodeBase.Infrastructure.Data;
using CodeBase.Services.ServiceLocator;

namespace CodeBase.Infrastructure.States.SaveLoad
{
    public interface ISavaLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}