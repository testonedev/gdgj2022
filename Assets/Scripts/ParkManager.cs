using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkManager : MonoBehaviour
{
    private int soulPoints;
    private int totalCollectedPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSoulPoints(int pointsToAdd)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Tries to pay soul points
    /// </summary>
    /// <param name="soulPoints"></param>
    /// <returns>True if successfully deducted soul points</returns>
    public bool SpendSoulPoints(int soulPoints)
    {
        return true;
    }
}
