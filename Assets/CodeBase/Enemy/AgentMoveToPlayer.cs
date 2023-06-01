using CodeBase.Infrastructure.Factory;
using CodeBase.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1f;
        public NavMeshAgent Agent;
        private Transform _playerTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.PlayerGameObject != null)
                InitializationPlayerTransform();
            else
            {
                _gameFactory.PlayerCreated += InitializationPlayerTransform;
            }
        }

        private void Update()
        {
            if (Initialized() && DistanceToThePlayer())
                Agent.destination = _playerTransform.position;
        }

        private bool Initialized() =>
            _playerTransform != null;

        private void InitializationPlayerTransform() =>
            _playerTransform = _gameFactory.PlayerGameObject.transform;

        private bool DistanceToThePlayer() =>
            Vector3.Distance(Agent.transform.position, _playerTransform.position) >= MinimalDistance;
    }
}