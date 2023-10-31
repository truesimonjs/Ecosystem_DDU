using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public int food;
    public string myState;
    [SerializeField]
    public IState currentState;
    //
    [HideInInspector] public testState testState;
    [HideInInspector] public WanderState wanderstate;
    [HideInInspector] public EatingState eatingState;

    private void Start()
    {

        //states
        testState = gameObject.AddComponent<testState>();
        wanderstate = gameObject.AddComponent<WanderState>();
        eatingState = gameObject.AddComponent<EatingState>();

        //first state
        currentState = testState;
        currentState.EnterState();

    }
    private void Update()
    {
        currentState.StateUpdate();
    }
    public void ChangeState(IState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
        myState = state.ToString();
    }

}

