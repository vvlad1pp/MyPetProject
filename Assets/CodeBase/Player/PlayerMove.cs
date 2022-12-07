using CodeBase.CameraLogic;
using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;
        private IInputService inputService;
        private Camera _camera;

        private void Awake()
        {
            inputService = Game.inputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            CameraFollow();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            if (inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            characterController.Move(movementVector * (movementSpeed * Time.deltaTime));
        }

        private void CameraFollow(){
            if (_camera.GetComponent<CameraFollow>())
            {
                _camera.GetComponent<CameraFollow>().Follow(gameObject);
            } 
             
        }
    }
}