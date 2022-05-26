using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{    
    [SerializeField] private HeavenManager heavenManager;
    [SerializeField] private HellManager hellManager;
    [SerializeField] private int heavenStartingPoints = 20;
    [SerializeField] private int hellStartingPoints = 20;

    public HeavenManager HeavenManager { get => heavenManager; }
    public HellManager HellManager { get => hellManager; }

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
        if(afterlifePlace == Alignment.Heaven) Instance.heavenManager.AddSoulPoints(alignmentPoints);
        else Instance.hellManager.AddSoulPoints(alignmentPoints);
    }

    public static Plot GetTargetPlot(int indexNumber)
    {
        //decide if have to go to the heaven or hell plot

        int heavenTotal = Instance.heavenManager.GetIndexScore(indexNumber);
        int hellTotal = Instance.hellManager.GetIndexScore(indexNumber);

        // If the "value" at the given index is the same for both sides, return a random plot
        if (heavenTotal == hellTotal)
            return Random.Range(0, 1) == 0 ?
                Instance.heavenManager.GetFirstPlotInRow(indexNumber) :
                Instance.hellManager.GetFirstPlotInRow(indexNumber);

        return heavenTotal > hellTotal ?
            Instance.heavenManager.GetFirstPlotInRow(indexNumber) :
            Instance.hellManager.GetFirstPlotInRow(indexNumber);
    }
}
