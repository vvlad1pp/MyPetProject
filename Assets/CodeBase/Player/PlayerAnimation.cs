using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private static readonly int MoveHash = Animator.StringToHash("Walking");
        public Animator animator;
        public CharacterController characterController;

        private void Update()
        {
            WalkingState();
        }

        private void WalkingState() => 
            animator.SetFloat(MoveHash, characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }
}