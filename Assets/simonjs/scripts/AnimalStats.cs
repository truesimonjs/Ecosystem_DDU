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

            AnimalStats baby = Instantiate(gameMaster.animalPrefab,gameMaster.animalHolder.transform,false).GetComponent<AnimalStats>();
            baby.transform.position = transform.position;
              baby.food = this.food / 2;
              this.food = food / 2;
            baby.dna = new AnimalDna(dna);
            baby.dna.mutate();
        }
    }
    public int Feed(int amount)
    {
        food += amount * (dna.foodEfficiency / 10);
        if (food > maxFood)
        {
           float Leftover = (food - maxFood)/(dna.foodEfficiency/10);            
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
        statsArray[Random.Range(0, statsArray.Length - 1)] -= 1;
        statsArray[Random.Range(0, statsArray.Length - 1)] += 1;
        statUpdate();
    }

}

