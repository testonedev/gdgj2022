using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private GameObject highlightIndicator;
    [SerializeField] private GameObject selectedIndicator;

    private int indexNumber;
    private int upgradeLevel;
    private int pointsPerTick;
    private int pointsPerInteraction;
    private Alignment alignment;
    private ParkManager parkManager;

    public int IndexNumber { get => indexNumber; }
    public int UpgradeLevel { get => upgradeLevel; }
    public int PointsPerTick { get => pointsPerTick; }
    public int PointsPerInteraction { get => pointsPerInteraction; }
    public Alignment Alignment { get => alignment; }
    public ParkManager ParkManager { get => parkManager; }

    public void Initialize(ParkManager parkManager, int indexNumber)
    {
        this.parkManager = parkManager;
        alignment = parkManager.Alignment;
        this.indexNumber = indexNumber;
    }

    public virtual void Tick()
    {
        parkManager.AddSoulPoints(pointsPerTick);
    }

    // NOTE: Not sure if we need this still
    public virtual int GetAlignmentPoints()
    {
        return pointsPerInteraction;
    }

    public bool UpgradePlot()
    {
        if (parkManager.SpendSoulPoints(parkManager.GetUpgradeCost(upgradeLevel, indexNumber)))
        {
            upgradeLevel++;
            pointsPerInteraction += upgradeLevel * 2;
            pointsPerTick += upgradeLevel;

            return true;
        }
        return false;
    }

    public virtual void Interact(Soul soulThatInteracted)
    {
        parkManager.AddSoulPoints(pointsPerInteraction);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightIndicator.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightIndicator.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.ShowPlotWindow(this);
    }
}
