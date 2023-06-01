using CodeBase.Services;
using CodeBase.Services.ServiceLocator;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAsset: IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}