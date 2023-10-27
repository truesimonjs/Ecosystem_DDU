using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : MonoBehaviour,IState
{
    private StateMachine statemachine;
    private NavMeshAgent agent;

    private void Awake()
    {
        statemachine = this.GetComponent<StateMachine>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }


    public void EnterState()
    {

        Vector3 PositionChange = new Vector3(Random.Range(1,10),0,Random.Range(1,10));
        agent.SetDestination(gameObject.transform.position + PositionChange);

    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void StateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
