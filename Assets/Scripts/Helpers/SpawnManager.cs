using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Level level;
    private List<GameObject> propSpawns, enemySpawns = new List<GameObject>();
    private GameObject[] spawners;
    public void GetLevelSpawners()
    {
        level = GetComponent<Level>();
        spawners = GameObject.FindGameObjectsWithTag("SpawnArea");
        Debug.Log("test i dunno" + spawners);
        if (spawners != null)
        {
            foreach(var item in spawners)
            {
                var spawnArea = item.GetComponent<SpawnArea>();
                Debug.Log(spawnArea);
                if(spawnArea.areaType == AreaType.PropArea)
                {
                    propSpawns.Add(item);
                }
                if(spawnArea.areaType == AreaType.EnemySpawner)
                {
                    enemySpawns.Add(item);
                }
            }
        }
        Debug.Log("Found: prop spawners:" + propSpawns + ", enemy spawners: " + enemySpawns);
    }
    public void ActivateSpawners(List<GameObject> spawnAreas)
    {
        foreach(var item in spawnAreas)
        {
            var spawnArea = item.GetComponent<SpawnArea>();
            spawnArea.SpawnItems();
        }
    }
}
