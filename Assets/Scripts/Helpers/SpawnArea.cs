using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic functionality for spawning items inside an area
/// </summary>
public class SpawnArea : MonoBehaviour
{
    public AreaType areaType;
    public bool spawnOnStart;
    public bool spawnAll = false; //check this if you want wo spawn all gameobjects in this area
    public int amountOfItems = 1; // number of items to spawn, default is 1
    public bool spawnInRandom = true; // spawns random items, uses 'Amount Of Items'
    public GameObject[] items; // items to spawn
    private BoxCollider area; 

    public void SpawnItems()
    {
        area = GetComponent<BoxCollider>();
        
        if (spawnAll)
        {
            foreach (var item in items)
            {
                //Debug.Log("Spawn point X: " + spawnPointX + ", Spawn point Z: " + spawnPointZ + ", Spawn point position: " + spawnPosition);
                GameObject newItem = Instantiate(item, GetRandomPosition(), Quaternion.identity, area.transform) as GameObject;
            }
            return;
        }
        if(spawnInRandom)
        {
            for(int itemIndex=0; itemIndex < amountOfItems; itemIndex++)
            {
                var randomItem = items[Random.Range(0, items.Length - 1)];
                GameObject newItem = Instantiate(randomItem, GetRandomPosition(), Quaternion.identity, area.transform) as GameObject;
            }
            return;
        }
    }
    /// <summary>
    /// Method for calculating item's random position
    /// </summary>
    /// <returns>Position inside box area</returns>
    Vector3 GetRandomPosition()
    {
        float spawnPointX = Random.Range(area.bounds.min.x, area.bounds.max.x);
        float spawnPointZ = Random.Range(area.bounds.min.z, area.bounds.max.z);
        return new Vector3(spawnPointX, 1.5f, spawnPointZ);
    }
    void Start()
    {
        if (spawnOnStart)
        {
            SpawnItems();
        }
    }
}
public enum AreaType
{
    PropArea,
    EnemySpawner
}
