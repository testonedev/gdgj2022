using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject highlightIndicator;
    [SerializeField] private GameObject selectedIndicator;

    private int indexNumber;
    private int upgradeLevel;
    private Alignment alignment;
    private ParkManager parkManager;

    public int IndexNumber { get => indexNumber; }
    public int UpgradeLevel { get => upgradeLevel; }
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

    }

    public virtual int GetAlignmentPoints()
    {
        //throw new System.NotImplementedException();

        return 3;
    }

    public bool UpgradePlot()
    {
        if (parkManager.SpendSoulPoints(parkManager.GetUpgradeCost(upgradeLevel, indexNumber)))
        {
            // TODO: Actually apply upgrades (e.g. increase passive income / interact gains)

            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightIndicator.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightIndicator.SetActive(false);
    }
}
