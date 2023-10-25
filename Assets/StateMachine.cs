using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private IState currentState;
    //
   [HideInInspector]
    public testState testState;
    [HideInInspector]
    public WanderState wanderstate;
    
    private void Start()
    {
        testState = gameObject.AddComponent<testState>();
        wanderstate = gameObject.AddComponent<WanderState>();

        //first state
        currentState = testState;
        currentState.EnterState();
    }
    public void ChangeState(IState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
        
    }
   
}

