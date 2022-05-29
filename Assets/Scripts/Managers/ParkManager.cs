using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParkManager : MonoBehaviour
{
    [Tooltip("Reference to a GameObject with a Grid component.")]
    [SerializeField] private Grid grid;
    [SerializeField] private Plot[] plotPrefabs;
    [SerializeField] private int rows = 7;
    [SerializeField] private int columns = 3;
    [SerializeField] private int plotBaseCost = 10;
    [SerializeField] private int plotUpgradeCostMultiplier = 10;
    [SerializeField] private float tickRateInSeconds = 1f;

    protected float lastTickTime;
    protected int soulPoints;
    protected int totalCollectedPoints;
    protected Plot[,] plots;

    public event Action<int> OnSoulPointsChanged;
    public event Action<int> OnTotalPointsChanged;

    public abstract Alignment Alignment { get; }
    public int SoulPoints { get => soulPoints; }
    public int TotalCollectedPoints { get => totalCollectedPoints; }
    

    protected virtual void Awake()
    {
        if (grid == null)
            Debug.LogError($"{name} requires the grid object reference to be set.");
    }

    protected virtual void Start()
    {
        plots = new Plot[rows, columns];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Plot newPlot = Instantiate(plotPrefabs[r], grid.transform);
                newPlot.transform.localPosition = grid.CellToLocal(new Vector3Int(c, 0, r));
                newPlot.Initialize(this, r);
                plots[r, c] = newPlot;
            }
        }
    }

    protected virtual void Update()
    {
        if (Time.time - lastTickTime >= tickRateInSeconds)
        {
            lastTickTime = Time.time;
            foreach (Plot plot in plots)
            {
                plot.Tick();
            }
        }
    }

    public virtual void AddSoulPoints(int pointsToAdd)
    {
        SetSoulPoints(soulPoints + pointsToAdd);
        SetTotalPoints(totalCollectedPoints + pointsToAdd);
    }

    /// <summary>
    /// Tries to pay soul points
    /// </summary>
    /// <param name="cost"></param>
    /// <returns>True if successfully deducted soul points</returns>
    public virtual bool SpendSoulPoints(int cost)
    {
        if (soulPoints >= cost)
        {
            SetSoulPoints(soulPoints - cost);
            return true;
        }
        return false;
    }

    public virtual int GetUpgradeCost(int currentUpgradeLevel, int indexNumber)
    {
        // TODO: Factor in per "Attribute" costs / discounts (e.g. if we have "Charity" cost reduction?)

        // e.g. (buy) 10 -> (lvl1 -> lvl2) 10 -> (lvl2 -> lvl3) 22 -> (lvl3 -> lvl4) 36 -> (lvl4 -> lvl5) 52
        return currentUpgradeLevel == 0 ? plotBaseCost : currentUpgradeLevel * (plotUpgradeCostMultiplier + currentUpgradeLevel - 1);
    }

    public virtual int GetUpgradeCost(Plot plot)
    {
        if (plot == null)
            return 0;
        return GetUpgradeCost(plot.UpgradeLevel, plot.IndexNumber);
    }
    
    /// <summary>
    /// Calculates the total score on a given row / index.
    /// </summary>
    /// <param name="indexNumber">The row/index number</param>
    /// <returns>The total score</returns>
    public int GetIndexScore(int indexNumber)
    {
        int sum = 0;
        for (int i = 0; i < columns; i++)
        {
            sum += plots[indexNumber, i].GetAlignmentPoints();
        }
        return sum;
    }

    public virtual Plot GetFirstPlotInRow(int indexNumber)
    {
        return plots[indexNumber, 0];
    }

    private void OnDrawGizmos()
    {
        // Visualize grid layout
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Gizmos.DrawWireCube(grid.CellToWorld(new Vector3Int(c, 0, r)), new Vector3(1f, 0, 1f));
            }
        }
    }

    public void SetSoulPoints(int soulPoints, bool invoke = true)
    {
        this.soulPoints = soulPoints;
        if (invoke)
            OnSoulPointsChanged?.Invoke(soulPoints);
    }

    public void SetTotalPoints(int totalPoints, bool invoke = true)
    {
        totalCollectedPoints = totalPoints;
        if (invoke)
            OnTotalPointsChanged?.Invoke(totalCollectedPoints);
    }
}
