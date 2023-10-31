using UnityEngine;

public class TestSense : MonoBehaviour, ISense
{
    public float range = 5;
    public GameObject[] UseSense(SenseTag tag)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Detectable"));
        GameObject[] gameobjects = new GameObject[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            gameobjects[i] = colliders[i].gameObject;
        }
        return gameobjects;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
