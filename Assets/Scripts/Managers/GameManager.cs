using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{    
    [SerializeField] private HeavenManager heavenManager;
    [SerializeField] private HellManager hellManager;
    [SerializeField] private int heavenStartingPoints = 20;
    [SerializeField] private int hellStartingPoints = 20;
    [SerializeField] private int scoreGoal = 1000;
    [SerializeField] private bool gameOver = false;
    [SerializeField] private int maxUpgradeLevel = 10;

    public UltEvent heavenWins;
    public UltEvent hellWins;

    public HeavenManager HeavenManager { get => heavenManager; }
    public HellManager HellManager { get => hellManager; }
    public int MaxUpgradeLevel { get => maxUpgradeLevel; }

    void Start()
    {
        heavenManager.AddSoulPoints(heavenStartingPoints);
        hellManager.AddSoulPoints(hellStartingPoints);
    }

    public static void SoulWentToAfterlife(Alignment afterlifePlace, int alignmentPoints)
    {
        //no more points given when game is over
        if (Instance.gameOver) return;
        
        if(afterlifePlace == Alignment.Heaven) Instance.heavenManager.AddSoulPoints(alignmentPoints);
        else Instance.hellManager.AddSoulPoints(alignmentPoints);

        //check if hell or heaven reached score goal
        //if both scored the goal, hell wins - unfair, but that's how life is, right?
        if (Instance.heavenManager.TotalCollectedPoints >= Instance.scoreGoal && Instance.hellManager.TotalCollectedPoints >= Instance.scoreGoal) GameOver(Alignment.Hell);
        else if (Instance.heavenManager.TotalCollectedPoints >= Instance.scoreGoal) GameOver(Alignment.Heaven);
        else if (Instance.hellManager.TotalCollectedPoints >= Instance.scoreGoal) GameOver(Alignment.Hell);
    }

    public static Plot GetTargetPlot(int indexNumber)
    {
        //decide if have to go to the heaven or hell plot
        int heavenTotal = Instance.heavenManager.GetIndexScore(indexNumber);
        int hellTotal = Instance.hellManager.GetIndexScore(indexNumber);

        // If the "value" at the given index is the same for both sides, return a random plot
        if (heavenTotal == hellTotal)
            return Random.Range(0, 2) == 0 ?
                Instance.heavenManager.GetFirstPlotInRow(indexNumber) :
                Instance.hellManager.GetFirstPlotInRow(indexNumber);

        return heavenTotal > hellTotal ?
            Instance.heavenManager.GetFirstPlotInRow(indexNumber) :
            Instance.hellManager.GetFirstPlotInRow(indexNumber);
    }

    public static void GameOver(Alignment winner)
    {
        Instance.gameOver = true;

        if (winner == Alignment.Heaven) Instance.heavenWins.Invoke();
        else Instance.hellWins.Invoke();

        UIManager.ShowAlignmentWinner(winner);
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
