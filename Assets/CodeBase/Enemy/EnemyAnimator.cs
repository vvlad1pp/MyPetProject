using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour , IAnimationStateReader
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        
        private static readonly int IdleStateHash = Animator.StringToHash("idle");
        private static readonly int AttackStateHash = Animator.StringToHash("attack");
        private static readonly int WalkingStateHash = Animator.StringToHash("move");
        private static readonly int DeadStateHash = Animator.StringToHash("die");
        
        private Animator _animator;
        
        public event Action<MyAnimatorStates> StateEnter;
        public event Action<MyAnimatorStates> StateExit;
        
        public MyAnimatorStates State { get; set; }

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayHit() => _animator.SetTrigger(Hit);
        public void PlayDeath() => _animator.SetTrigger(Die);

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving() => _animator.SetBool(IsMoving, false);
        public void PlayAttack() => _animator.SetTrigger(Attack);
        public void EnterState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEnter?.Invoke(State);
        }

        public void ExitState(int stateHash) =>
            StateExit?.Invoke(StateFor(stateHash));

        private MyAnimatorStates StateFor(int stateHash)
        {
            MyAnimatorStates state;
            if (stateHash == IdleStateHash)
                state = MyAnimatorStates.Idle;
            else if(stateHash == AttackStateHash) 
                state = MyAnimatorStates.Attack;
            else if(stateHash == WalkingStateHash) 
                state = MyAnimatorStates.Walking;
            else if(stateHash == DeadStateHash) 
                state = MyAnimatorStates.Died;
            else
                state = MyAnimatorStates.Unknown;
            return state;
        }
    }
}