using UnityEngine;
using UnityEngine.AI;

public class AnimalStats : MonoBehaviour
{
    //DnaStats

    public AnimalDna dna;
    private int maxWater;
    private int maxFood;
    private float birthCd;

    //stats
    public float water;
    private float babyFood;
    public float food;
    //
    private float NextBirth;
    [SerializeField] private GameMaster gameMaster;
    private void Start()
    {
        gameMaster = GameObject.FindAnyObjectByType<GameMaster>();
        maxWater = dna.waterStorage * dna.mass;
        water = maxWater;
        maxFood = dna.foodStorage * dna.mass;
        food = maxFood;

        birthCd = (100 / dna.BreedingSpeed * dna.mass * 1.1f);
        NextBirth = Time.time + birthCd;
        babyFood = 0;
       

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

            AnimalStats baby = Instantiate(gameMaster.animalPrefab,gameMaster.transform.position,Quaternion.identity,gameMaster.animalHolder.transform).GetComponent<AnimalStats>();
            baby.food = babyFood;
            babyFood = 0;
            baby.GetComponent<NavMeshAgent>().Warp(transform.position);
              
            baby.dna = new AnimalDna(dna);
            baby.dna.mutate();
        }
    }
    public int Feed(int amount)
    {
        food += amount * (dna.foodEfficiency / 10);
        if (food > maxFood)
        {
            babyFood += (food - maxFood);
           float Leftover = (babyFood - maxFood)/(dna.foodEfficiency/10);
            babyFood = maxFood;
            food = maxFood;
            
           
            return (int)Leftover;
        }
        return 0;
    }

}
[System.Serializable]
public class AnimalDna
{

    public int mass = 3;
    //totalpoints is 40 atm
    int[] statsArray;
    public int waterStorage = 10;
    public int foodEfficiency = 10;
    public int foodStorage = 10;
    public int BreedingSpeed = 10;
    //behaviour
    public int watersearch;
    public int foodsearch;
    public AnimalDna()
    {
        statsArray = new int[4] { waterStorage, foodEfficiency, foodStorage, BreedingSpeed };
        
    }
    public AnimalDna(AnimalDna old)
    {
        statsArray = old.statsArray;
        statUpdate();
    }
    public void statUpdate()
    {
        waterStorage = statsArray[0];
        foodEfficiency = statsArray[1];
        foodStorage = statsArray[2];
        BreedingSpeed = statsArray[3];
    }
    public void mutate()
    {
        statsArray[Random.Range(0, statsArray.Length )] -= 1;
        statsArray[Random.Range(0, statsArray.Length)] += 1;
        statUpdate();
    }

}

