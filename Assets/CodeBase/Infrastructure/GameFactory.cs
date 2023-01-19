using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string PlayerPath = "Player/player";
        private const string HUDPath = "Hud/hud";
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetsProvider)
        {
            _assetProvider = assetsProvider;
        }

        public GameObject CreatePlayer(GameObject at) =>
            _assetProvider.Instantiate(PlayerPath, at: at.transform.position);

        public void CreateHud() =>
            _assetProvider.Instantiate(HUDPath);
    }
}