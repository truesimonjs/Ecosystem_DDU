using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public int food = 10;
    public float cd = 10;
    public int counter;
    public void Devour(GameObject caller)
    {
        if (gameObject.activeSelf)
        {
            counter += 1;
            caller.GetComponent<AnimalStats>().Feed(food);
            Invoke("activate", cd);
            gameObject.SetActive(false);
        }
    }
    public void activate()
    {

        counter = 0;
        gameObject.SetActive(true);
    }


}
