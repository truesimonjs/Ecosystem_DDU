using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public float timescale;
    public GameObject animalPrefab;
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
