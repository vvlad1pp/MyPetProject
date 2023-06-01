using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.States.SaveLoad;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISavaLoadService _saveLoadService;
        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISavaLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        public void Enter()
        {
            LoadProgressInitNew();
            _gameStateMachine.Enter<LoadLevelState,string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }
        public void Exit()
        {
        }
        private void LoadProgressInitNew() => 
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        private PlayerProgress NewProgress() => 
            new PlayerProgress("Main");
    }
}