using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using Lean.Pool;

public class Spawner : MonoBehaviour
{
    public GameObject soulPrefab;
    public Transform spawnLocation;
    public float spawnRate;

    public UltEvent<GameObject> onSoulSpawnedEvent;

    //runtime
    private float nextSpawnTime;
    private GameObject spawnedObject;

    void Start()
    {
        nextSpawnTime = Time.time + (1 / spawnRate);
    }

    void Update()
    {
        if (!isActiveAndEnabled) return;
        
        if (nextSpawnTime <= Time.time) SpawnSoul();
    }

    public void SpawnSoul()
    {
        nextSpawnTime = Time.time + (1 / spawnRate);

        spawnedObject = LeanPool.Spawn(soulPrefab);
        spawnedObject.transform.position = spawnLocation.position;
        spawnedObject.GetComponent<Soul>().StartObject();

        onSoulSpawnedEvent.Invoke(spawnedObject);
    }
}
