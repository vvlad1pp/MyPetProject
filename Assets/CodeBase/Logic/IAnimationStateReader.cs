namespace CodeBase.Logic
{
    public interface IAnimationStateReader
    {
        void EnterState(int stateHash);
        void ExitState(int stateHash);
        MyAnimatorStates State { get;}
    }
}