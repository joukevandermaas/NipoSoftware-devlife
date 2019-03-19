using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject BugPrefab;
    public GameObject FeaturePrefab;

    public bool ShouldSpawn = true;

    public float nextSpawn = 0f;

    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        CalculateNextSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawn -= Time.deltaTime;

        if(nextSpawn <= 0)
        {
            Spawn();
        }
    }

    public void SpawnBug()
    {
        Instantiate(BugPrefab, transform.position, Quaternion.identity);
    }

    public void SpawnFeature()
    {
        Instantiate(FeaturePrefab, transform.position, Quaternion.identity);
    }

    private void Spawn()
    {
        if (ShouldSpawn)
        {
            // change to feature once done.
            SpawnBug();

            CalculateNextSpawn();
        }
    }

    private void CalculateNextSpawn()
    {
        nextSpawn = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
    }
}
