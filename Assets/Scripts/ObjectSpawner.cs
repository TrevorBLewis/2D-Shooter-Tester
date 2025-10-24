using System.Collections.Generic;
using UnityEngine;

// This scripts helps to spawn a new set of objects onto the screen...specifically the asteroids
// Added random sizes for the spawn objects and when the player hits the boost button a moves fast
// I want whatever has spawned to move in relation to the speed of the player so when the player moves fast
// the objects speed up in their respected direction as well.

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;


    [System.SerializableAttribute]
    public class Wave
    {
        public GameObject prefab;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerWave;
        public int spawnObjectCount;
    }


    private void Update()
    {
        waves[waveNumber].spawnTimer += Time.deltaTime * PlayerController.Instance.boost;
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
            waves[waveNumber].spawnTimer = 0;
            SpawnObject();
        }

        if (waves[waveNumber].spawnObjectCount >= waves[waveNumber].objectsPerWave)
        {
            waves[waveNumber].spawnObjectCount = 0;
            waveNumber++;
            if ( waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }
    }


    private void SpawnObject()
    {
        Instantiate(waves[waveNumber].prefab, RandomSpawnPoint(), transform.rotation, transform);
        waves[waveNumber].spawnObjectCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.y = minPos.position.y;
        spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);

        return spawnPoint;
    }


}
