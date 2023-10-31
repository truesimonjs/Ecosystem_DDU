using UnityEngine;

public class testState : MonoBehaviour, IState
{
    private StateMachine statemachine;

    private void Awake()
    {
        statemachine = this.GetComponent<StateMachine>();
    }
    public void EnterState()
    {
        statemachine.ChangeState(statemachine.wanderstate);
    }

    public void ExitState()
    {
        Debug.Log("exit state");
    }

    public void StateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
