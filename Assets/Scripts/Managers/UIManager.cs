using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvas canvasPrefab;

    private PlotWindowUI plotWindow;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        plotWindow = Instantiate(canvasPrefab).GetComponentInChildren<PlotWindowUI>();
    }

    public void ShowPlotWindow(Plot plot)
    {
        plotWindow.SetPlot(plot);
    }
}
