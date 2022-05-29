using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Plot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private GameObject highlightIndicator;
    [SerializeField] private GameObject selectedIndicator;
    [SerializeField] private Transform graphics;
    [SerializeField] private Vector3 newGraphicsOffset = new Vector3(3f, 0f, 0f);
    [SerializeField] private TextMeshProUGUI upgradeText;

    private int indexNumber;
    private int upgradeLevel;
    private int pointsPerTick;
    private int pointsPerInteraction;
    private Alignment alignment;
    private ParkManager parkManager;
    private bool selected;

    public UltEvent OnHighlighted;
    public UltEvent OnSelected;
    public UltEvent OnUpgrade;

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

    public virtual int GetAlignmentPoints()
    {
        return pointsPerInteraction;
    }

    public bool UpgradePlot()
    {
        if (upgradeLevel == GameManager.Instance.MaxUpgradeLevel)
            return false;
        if (parkManager.SpendSoulPoints(parkManager.GetUpgradeCost(upgradeLevel, indexNumber)))
        {
            upgradeLevel++;
            pointsPerInteraction += upgradeLevel * 2;
            pointsPerTick += upgradeLevel;

            upgradeText.text = upgradeLevel.ToString();

            AddNewGraphics();

            OnUpgrade?.Invoke();

            return true;
        }
        return false;
    }

    // NOTE: This isn't being used
    //public virtual void Interact(Soul soulThatInteracted)
    //{
    //    parkManager.AddSoulPoints(pointsPerInteraction);
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightIndicator.SetActive(true);
        OnHighlighted?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightIndicator.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.SelectPlot(this);
    }

    public void Select()
    {
        selectedIndicator.SetActive(true);
        OnSelected?.Invoke();
    }

    public void Deselect()
    {
        selectedIndicator.SetActive(false);
    }

    public void AddNewGraphics()
    {
        Transform newGraphics = Instantiate(graphics, graphics.position + (newGraphicsOffset * upgradeLevel), Quaternion.identity, transform);
    }
}
