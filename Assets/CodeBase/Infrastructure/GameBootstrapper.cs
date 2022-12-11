using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour , ICoroutineRunner
    {
        private Game game;

        private void Awake()
        {
            game = new Game();
            game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}