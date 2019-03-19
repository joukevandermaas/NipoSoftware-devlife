using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NPCSpawner spawner;

    public float spawnBugChance = 0.2f;
    public float spawnBugChanceIncreaseRate = 0.025f;

    public float decreaseSpawnTimeWhenBugReachesDoor = 0.2f;
    public float decreaseSpawnTimeWhenFeatureReachesDoor = 0.2f;

    public void ShotsFired()
    {
        var rnd = UnityEngine.Random.Range(0f, 1f);

        if(rnd < spawnBugChance)
        {
            SpawnBug();
            spawnBugChance += spawnBugChanceIncreaseRate;
        }
    }

    public void BugReachedDoor()
    {
    }

    public void FeatureReachedDoor()
    {

    }


    private void SpawnBug()
    {
        spawner.SpawnBug();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
