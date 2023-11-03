using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
   [SerializeField] private int rangeX;
    [SerializeField] private int rangeZ;
    [SerializeField] private float amountPerUnit;
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        Debug.Log(amountPerUnit * rangeX * rangeZ);
        for (int i = 0; i <= amountPerUnit*rangeX*rangeZ; i++)
        {
            Vector3 target = transform.position + new Vector3(Random.Range(-rangeX/2, rangeX/2), 100, Random.Range(-rangeZ/2, rangeZ/2));
            Ray ray = new Ray(target, Vector3.down);
            RaycastHit hit;
            Physics.Raycast(ray, out hit,Mathf.Infinity, LayerMask.GetMask("GroundTerrain"));
            GameObject spawnee =Instantiate(prefab,transform);
            spawnee.transform.position = hit.point;
           
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position,new Vector3(rangeX,100,rangeZ));
    }
}
