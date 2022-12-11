using CodeBase.CameraLogic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string nameScene) => 
            _sceneLoader.Load(nameScene, OnLoaded);

        private void OnLoaded()
        {
            GameObject player = Instantiate("Player/player");
            Instantiate("Resources/Hud");
            CameraFollow(player);
        }
        
        private void CameraFollow(GameObject player){
            Camera.main.GetComponent<CameraFollow>().Follow(player);
        }

        private GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public void Exit()
        {
             
        }
    }
}