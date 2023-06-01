using CodeBase.Services.ServiceLocator;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService: IService
    {
        Vector2 Axis { get; }

        bool isAttackButton();
    }
}