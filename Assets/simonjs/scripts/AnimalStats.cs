using UnityEngine;

public class AnimalStats : MonoBehaviour
{
    //DnaStats

    public AnimalDna dna;
    private int maxWater;
    private int maxFood;
    private float birthCd;
    //stats
    public float water;
    public float food;
    //
    private float NextBirth;
    private void Awake()
    {
        maxWater = dna.waterStorage * dna.mass;
        water = maxWater;
        maxFood = dna.foodStorage * dna.mass;
        food = maxFood;

        birthCd = dna.BreedingSpeed * dna.mass * 1.1f;
        NextBirth = Time.time + birthCd;


    }
    private void Update()
    {
        food -= 1 * Time.deltaTime;
        // water -= 1 * Time.deltaTime;
        if (food <= 0 || water <= 0)
        {
            Destroy(this.gameObject);
        }
        if (Time.time >= NextBirth)
        {
            NextBirth = Time.time + birthCd;
            Instantiate(this.gameObject);
        }
    }
    public void Feed(float amount)
    {
        food += amount * dna.foodEfficiency / 10;
        if (food > maxFood) 
        {
            food = maxFood; 
        }
    }

}
[System.Serializable]
public class AnimalDna
{

    public int mass = 3;
    //totalpoints is 40 atm
    public int waterStorage = 10;
    public int foodEfficiency = 10;
    public int foodStorage = 10;
    public int BreedingSpeed = 10;
    //behaviour
    public int watersearch;
    public int foodsearch;
}
