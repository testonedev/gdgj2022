using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class Spawner : MonoBehaviour
{
    private GameObject soulPrefab;
    private Transform spawnLocation;
    private UltEvent<Soul> onSoulSpawnedEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSoul()
    {
        throw new System.NotImplementedException();
    }
}
