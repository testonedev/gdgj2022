using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlotWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI plotNameText;
    [SerializeField] private TextMeshProUGUI upgradeLevelText;
    [SerializeField] private TextMeshProUGUI pointsPerTickText;
    [SerializeField] private TextMeshProUGUI pointsPerInteractionText;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private RectTransform upgradePanel;
    [SerializeField] private Button upgradeButton;

    private RectTransform rectTransform;
    private Plot currentPlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(TryUpgradeCurrentPlot);
    }

    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(TryUpgradeCurrentPlot);
    }

    private void TryUpgradeCurrentPlot()
    {
        if (currentPlot == null)
            return;

        if (currentPlot.UpgradePlot())
        {
            UpdateUIElements();
        }
    }

    public void SetPlot(Plot plot)
    {
        if (currentPlot == plot && currentPlot != null)
        {
            // Simply update window position
            ShowWindow();
            UpdateWindowPosition();
            return;
        }

        currentPlot = plot;

        if (currentPlot == null)
        {
            HideWindow();
            return;
        }

        ShowWindow();
        UpdateUIElements();
        UpdateWindowPosition();
    }

    public void UpdateWindowPosition()
    {
        // TODO: Convert this to a point on the canvas as it's in screen space
        Vector2 origin = InputManager.PointerPosition;
        rectTransform.anchoredPosition = origin;
    }

    public void ShowWindow()
    {
        gameObject.SetActive(true);
    }

    public void HideWindow()
    {
        gameObject.SetActive(false);
    }

    public void UpdateUIElements()
    {
        if (currentPlot == null)
            return;

        upgradeLevelText.text = $"{currentPlot.UpgradeLevel}";
        pointsPerTickText.text = $"P/Tick: {currentPlot.PointsPerTick}";
        pointsPerInteractionText.text = $"P/Interaction: {currentPlot.PointsPerInteraction}";

        if (currentPlot.Alignment == Alignment.Heaven)
        {
            upgradePanel.gameObject.SetActive(true);
            plotNameText.text = $"{Consts.HeavenAttributes[currentPlot.IndexNumber]} Plot";
            upgradeCostText.text = $"Upgrade Cost: {GameManager.Instance.HeavenManager.GetUpgradeCost(currentPlot)}";
        }
        else
        {
            upgradePanel.gameObject.SetActive(false);
            plotNameText.text = $"{Consts.HellAttributes[currentPlot.IndexNumber]} Plot";
        }
    }
}
