using UnityEngine;

public class PlantScript : MonoBehaviour
{
    
    public int food = 10;
    public float cd = 10;
    public int counter;
    private void Start()
    {
        
    }
    public void Devour(GameObject caller)
    {
        if (gameObject.activeSelf)
        {
            counter += 1;
            float leftOver = caller.GetComponent<AnimalStats>().Feed(food);
            Invoke("activate", cd*(leftOver/food));
            Debug.Log(cd*(leftOver/food));
            gameObject.SetActive(false);
        }
    }
    public void activate()
    {

        counter = 0;
        gameObject.SetActive(true);
    }

   
}
