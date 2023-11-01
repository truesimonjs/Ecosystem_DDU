using System.Collections.Generic;
using UnityEngine;



public class theSensitiveone : MonoBehaviour, ISense
{
    public float range;
    public Collider[] detectable;
    public List<GameObject> targets;
    public float fov = 30;
    public SenseTag p_tag;




    public GameObject[] UseSense(SenseTag tag)
    {

        p_tag = tag;
        InRange();
        for (int i = 0; i < targets.Count; i++)
        {
            GameObject target = targets[i];
            if (!targetInFOV(target, fov) || Obstructed(target))
            {
                targets.Remove(target);
            }
        }

        return targets.ToArray();
    }

    public void InRange()
    {
        targets = new List<GameObject>();
        detectable = Physics.OverlapSphere(this.transform.position, range, LayerMask.GetMask("detectable"));
        for (int i = 0; i < detectable.Length; i++)
        {
            GameObject target = detectable[i].gameObject;
            if (target.GetComponent<SenseTarget>().Istarget(p_tag))
            {
                targets.Add(target);
            }
        }
    }

    public bool targetInFOV(GameObject target, float FOV)
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;

        return Vector3.Angle(direction, transform.forward) <= FOV;
    }

    public bool Obstructed(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Ray ray = new Ray(this.transform.position, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);


        return hit.transform.gameObject != target;
    }

    private void OnDrawGizmos()
    {
        //shows ray from eyes to index 0 of targets (targets is a list of all things it can currently see)
        if (detectable.Length > 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(this.transform.position, detectable[0].transform.position - transform.position);
        }
        //2 rays that show the fov of the eyes
        Vector3 right = Quaternion.Euler(0, fov, 0) * transform.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, right * range);
        Vector3 left = Quaternion.Euler(0, -fov, 0) * transform.forward;
        Gizmos.DrawRay(this.transform.position, left * range);
    }

}
