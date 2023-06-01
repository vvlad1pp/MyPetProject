using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriter { get; } = new List<ISavedProgress>();
        public event Action PlayerCreated;
        public GameObject PlayerGameObject { get; set; }

        public GameFactory(IAsset assets) =>
            _asset = assets;

        public GameObject CreatePlayer(GameObject at)
        { 
            PlayerGameObject = InstantiateRegister(AssetPath.PlayerPath, at.transform.position);
            PlayerCreated?.Invoke();
            return PlayerGameObject;

        }

        public void CreateHud() =>
            InstantiateRegister(AssetPath.HUDPath);

        private GameObject InstantiateRegister(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _asset.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegister(string prefabPath)
        {
            GameObject gameObject = _asset.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressReaders.Clear();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriter.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}