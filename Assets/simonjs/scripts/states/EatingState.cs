using UnityEngine;
using UnityEngine.AI;

public class EatingState : MonoBehaviour, IState
{
    //component references
    private StateMachine statemachine;
    private NavMeshAgent agent;
    private AnimalStats animalStats;

    //
    private Vector3 targetPos;
    public GameObject food;
    private void Awake()
    {
        statemachine = this.GetComponent<StateMachine>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        animalStats = gameObject.GetComponent<AnimalStats>();
    }
    public void EnterState()
    {

        targetPos = food.transform.position;
        agent.SetDestination(targetPos);
    }

    public void ExitState()
    {

    }

    public void StateUpdate()
    {
        if (food.activeSelf)
        {
            agent.SetDestination(targetPos);
            if (Vector3.Distance(this.transform.position, targetPos) <= 2)
            {
                if (food.GetComponent<SenseTarget>().Istarget(SenseTag.plant))
                {
                    food.GetComponent<PlantScript>().Devour(gameObject);
                    statemachine.ChangeState(statemachine.wanderstate);
                }
            }
        }
        else
        {
            statemachine.ChangeState(statemachine.wanderstate);
        }
    }
}
