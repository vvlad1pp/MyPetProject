using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IGameFactory
    {
        GameObject CreatePlayer(GameObject at);
        void CreateHud();
    }
}