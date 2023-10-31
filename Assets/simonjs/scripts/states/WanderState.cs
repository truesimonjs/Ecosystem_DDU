using UnityEngine;
using UnityEngine.AI;

public class WanderState : MonoBehaviour, IState
{
    private StateMachine statemachine;
    private NavMeshAgent agent;
    private Vector3 targetPos;
    //searching
    private ISense sense;
    private GameObject[] searchTargets;
    private void Awake()
    {
        statemachine = this.GetComponent<StateMachine>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        sense = this.GetComponent<ISense>();
    }


    public void EnterState()
    {

        targetPos = this.transform.position;

    }

    public void ExitState()
    {

    }

    public void StateUpdate()
    {
        Search();
        if (Vector3.Distance(this.transform.position, targetPos) <= 2)
        {

            NavMeshHit hit;
            Vector3 PositionChange = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

            if (!NavMesh.SamplePosition(gameObject.transform.position + PositionChange, out hit, 2, NavMesh.AllAreas))
            {

                PositionChange = Vector3.zero;
            }
            targetPos = gameObject.transform.position + PositionChange;
            agent.SetDestination(targetPos);
        }

    }

    public void Search()
    {
        FoodSearch();
    }

    public void FoodSearch()
    {
        searchTargets = sense.UseSense(SenseTag.plant);
        if (searchTargets.Length != 0)
        {
            statemachine.eatingState.food = searchTargets[0];
            statemachine.ChangeState(statemachine.eatingState);
        }
    }

    public void WaterSearch()
    {

    }

}
