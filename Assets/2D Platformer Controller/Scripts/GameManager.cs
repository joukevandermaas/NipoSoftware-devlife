using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public NPCSpawner spawner;
    public AudioManager audioManager;

    public Text scoreUI;

    public static GameManager Instance;

    public int Score;

    public float spawnBugChance = 0.2f;
    public float spawnBugChanceIncreaseRate = 0.025f;

    public float decreaseSpawnTimeWhenBugReachesDoor = 0.2f;

    public void ShotsFired()
    {
        var rnd = UnityEngine.Random.Range(0f, 1f);

        if(rnd < spawnBugChance)
        {
            SpawnBug();
        }

        spawnBugChance += spawnBugChanceIncreaseRate;
        audioManager.PlayShootSound();
    }

    public void Reset()
    {
        spawner.KillAllBugs();

        Score = 0;

        spawnBugChance = 0.2f;
        spawnBugChanceIncreaseRate = 0.025f;

        decreaseSpawnTimeWhenBugReachesDoor = 0.2f;

        SetScore();
    }

    public void BugReachedDoor()
    {
        Score--;
        SetScore();

        spawner.BugReachesDoor(decreaseSpawnTimeWhenBugReachesDoor);
    }

    public void EnemyDied()
    {
        Score++;
        SetScore();
    }

    public void SetScore()
    {
        if(Score < 0)
        {
            Player.Instance.OnDeath();
        }

        scoreUI.text = $"Score: {Score}";
    }
    
    private void SpawnBug()
    {
        spawner.SpawnBug();
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        spawner.SpawnBug();
        spawner.SpawnBug();
        spawner.SpawnBug();
        spawner.SpawnBug();
        spawner.SpawnBug();
        spawner.SpawnBug();
        spawner.SpawnBug();
        audioManager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
