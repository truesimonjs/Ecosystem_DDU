using UnityEngine;

public class SenseTarget : MonoBehaviour
{

    [SerializeField] private SenseTag[] myTags;

    public bool Istarget(SenseTag targetTag)
    {

        foreach (SenseTag myTag in myTags)
        {
            if (myTag == targetTag)
            {
                return true;
            }
        }
        return false;
    }
}
public enum SenseTag
{
    prey, predator, plant
}