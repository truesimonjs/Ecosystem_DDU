using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void InitState(StateMachine machine);
    public abstract void StateUpdate();
    public abstract void EnterState();
    public abstract void ExitState();

}
