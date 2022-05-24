using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Soul soulPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private UltEvent<Soul> onSoulSpawnedEvent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnSoul()
    {
        throw new System.NotImplementedException();
    }
}
