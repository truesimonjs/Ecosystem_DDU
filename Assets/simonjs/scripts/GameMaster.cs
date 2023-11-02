using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public float timescale;
    public GameObject animalPrefab;
    public GameObject animalHolder;
    private void Start()
    {
        Time.timeScale = timescale;
    }
    private void OnValidate()
    {
       if (Application.isPlaying)
        {
            Time.timeScale = timescale;
        }
    }
}
