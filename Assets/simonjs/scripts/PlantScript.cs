using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public int food = 10;
    public void Devour(GameObject caller)
    {
        caller.GetComponent<AnimalStats>().Feed(food);
        Invoke("activate", 5);
        gameObject.SetActive(false);
    }
    public void activate()
    {
        gameObject.SetActive(true);
    }


}
