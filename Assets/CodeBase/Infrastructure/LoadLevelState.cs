using CodeBase.CameraLogic;
using CodeBase.Logic;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerInitialPoint = "PlayerInitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string nameScene)
        {
            _curtain.Show();
            _sceneLoader.Load(nameScene, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();                  

        private void OnLoaded()
        {
            GameObject player = _gameFactory.CreatePlayer(at: GameObject.FindWithTag(PlayerInitialPoint));

            _gameFactory.CreateHud();
            CameraFollow(player);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject player)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(player);
        }
    }
}