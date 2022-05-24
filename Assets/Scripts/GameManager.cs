using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Alignment { Heaven, Hell }

public class GameManager : Singleton<GameManager>
{    
    [SerializeField] private HeavenManager heavenManager;
    [SerializeField] private HellManager hellManager;
    [SerializeField] private int heavenStartingPoints = 20;
    [SerializeField] private int hellStartingPoints = 20;

    void Start()
    {
        heavenManager.AddSoulPoints(heavenStartingPoints);
        hellManager.AddSoulPoints(hellStartingPoints);
    }

    void Update()
    {
        
    }

    public static void SoulWentToAfterlife(Alignment afterlifePlace, int alignmentPoints)
    {
        throw new System.NotImplementedException();
    }

    public static Plot GetTargetPlot(int indexNumber)
    {
        return null;
    }
}
