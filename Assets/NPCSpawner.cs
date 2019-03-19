using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject BugPrefab;
    public GameObject FeaturePrefab;
    public AudioManager audioManager;

    public bool ShouldSpawn = true;

    public float nextSpawn = 0f;

    private float actualMinSpawnTime = 0.25f;
    private float actualMaxSpawnTime = 0.50f;

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

    public void KillAllBugs()
    {
        foreach(var bug in GetComponentsInChildren<Bug>())
        {
            bug.OnDeath();
        }
    }

    public void SpawnBug()
    {
        Instantiate(BugPrefab, transform.position, Quaternion.identity, transform);
    }

    public void SpawnFeature()
    {
        Instantiate(FeaturePrefab, transform.position, Quaternion.identity);
    }

    internal void BugReachesDoor(float decreaseSpawnTimeWhenBugReachesDoor)
    {
        minSpawnTime = Mathf.Max(actualMinSpawnTime, minSpawnTime -= decreaseSpawnTimeWhenBugReachesDoor);
        maxSpawnTime = Mathf.Max(actualMaxSpawnTime, maxSpawnTime -= decreaseSpawnTimeWhenBugReachesDoor);
    }

    private void Spawn()
    {
        if (ShouldSpawn)
        {
            SpawnBug();

            audioManager.PlayAngryCustomerSound();

            CalculateNextSpawn();
        }
    }

    private void CalculateNextSpawn()
    {
        nextSpawn = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
    }
}
