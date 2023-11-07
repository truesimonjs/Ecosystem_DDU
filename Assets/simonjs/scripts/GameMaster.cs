using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public float timescale;
    public GameObject animalPrefab;
    public GameObject animalHolder;
    public List<AnimalStats> Bears = new List<AnimalStats>();
    public List<Vector2> BearsByTime;
    public Window_Graph graph;

    private void Start()
    {
        Time.timeScale = timescale;
        graph = GameObject.FindAnyObjectByType<Window_Graph>();
        InvokeRepeating("CountBears", 0, 60);
    }
    private void OnValidate()
    {
       if (Application.isPlaying)
        {
            Time.timeScale = timescale;
        }
    }
    public void CountBears()
    {
        BearsByTime.Add(new Vector2(Time.time/60,Bears.Count));
        graph.TakeList(BearsByTime);

    }

    public void addBear(AnimalStats bear)
    {
        Bears.Add(bear);
    }
    public void removeBear(AnimalStats bear)
    {
        Bears.Remove(bear);
    }
}
