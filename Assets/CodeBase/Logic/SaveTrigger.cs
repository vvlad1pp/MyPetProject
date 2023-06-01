using CodeBase.Infrastructure.States.SaveLoad;
using CodeBase.Services.ServiceLocator;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISavaLoadService _saveLoadService;
        public BoxCollider Collider;

        private void Awake() => 
            _saveLoadService = AllServices.Container.Single<ISavaLoadService>();

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress Save");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!Collider)
                return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position+Collider.center,Collider.size);
        }
    }
}