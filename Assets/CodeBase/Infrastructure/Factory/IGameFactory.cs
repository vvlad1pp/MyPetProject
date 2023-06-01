using System;
using System.Collections.Generic;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.ServiceLocator;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject at);
        void CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriter { get; }
        event Action PlayerCreated; 
        GameObject PlayerGameObject { get; }
        void CleanUp();
    }
}