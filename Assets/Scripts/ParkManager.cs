using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParkManager : MonoBehaviour
{
    [Tooltip("Reference to a GameObject with a Grid component.")]
    [SerializeField] private Grid grid;
    [SerializeField] private Plot plotPrefab;
    [SerializeField] private int rows = 7;
    [SerializeField] private int columns = 3;
    [SerializeField] private int plotBaseCost = 10;
    [SerializeField] private int plotUpgradeLevelCostMultiplier = 10;

    protected int soulPoints;
    protected int totalCollectedPoints;
    protected Plot[,] plots;

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
                Plot newPlot = Instantiate(plotPrefab, grid.transform);
                newPlot.transform.localPosition = grid.CellToLocal(new Vector3Int(c, 0, r));
                newPlot.Initialize(this, r);
                plots[r, c] = newPlot;
            }
        }
    }

    protected virtual void Update()
    {
        
    }

    public virtual void AddSoulPoints(int pointsToAdd)
    {
        soulPoints += pointsToAdd;
        totalCollectedPoints += pointsToAdd;
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
            soulPoints -= cost;
            return true;
        }
        return false;
    }

    public virtual int GetUpgradeCost(int currentUpgradeLevel, int indexNumber)
    {
        // TODO: Factor in per "Attribute" costs / discounts (e.g. if we have "Charity" cost reduction?)

        // e.g. (buy) 10 -> (lvl1 -> lvl2) 10 -> (lvl2 -> lvl3) 22 -> (lvl3 -> lvl4) 36 -> (lvl4 -> lvl5) 52
        return currentUpgradeLevel == 0 ? plotBaseCost : currentUpgradeLevel * (plotUpgradeLevelCostMultiplier + currentUpgradeLevel - 1);
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
}
