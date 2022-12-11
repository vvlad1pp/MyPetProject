using CodeBase.CameraLogic;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerInitialPoint = "PlayerInitialPoint";
        private const string PlayerPath = "Player/player";
        private const string HUDPath = "Hud/hud";
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
            var playerInitialPoint = GameObject.FindWithTag(PlayerInitialPoint);
            GameObject player = Instantiate(PlayerPath, at: playerInitialPoint.transform.position);
            Instantiate(HUDPath);
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
        private GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at,quaternion.identity);
        }

        public void Exit()
        {
             
        }
    }
}