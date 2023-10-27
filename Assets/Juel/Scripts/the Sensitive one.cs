using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theSensitiveone : MonoBehaviour
{
    [SerializeField] float range;
    public Collider[] detectable;
    public List<Transform> targets;
    [SerializeField] float fov = 30;

    public List<Transform> activate()
    {
        InRange();
        for (int i = 0; i < targets.Count;  i++)
        {
            Transform target = targets[i];
            if (!targetInFOV(target, fov) || Obstructed(target))
            {
                targets.Remove(target);
            }
        }
    }

    public void InRange()
    {
        targets = new List<Transform>();
        detectable = Physics.OverlapSphere(this.transform.position, range, LayerMask.GetMask("Prey"));
        for (int i = 0; i < detectable.Length; i++)
        {
            Transform target = detectable[i].gameObject.transform;
            if (target.GetComponent<detectable>().HasSense(Sense.sight))
            {
                targets.Add(target);
            }
        }
    }

    public bool targetInFOV(Transform target, float FOV)
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;
        return Vector3.Angle(direction, transform.forward) <= FOV;
    }

    public bool Obstructed(Transform target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Ray ray = new Ray(this.transform.position, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit.transform != target;
    }


}
