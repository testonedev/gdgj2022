using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private PlotWindow plotWindow;

    public void ShowPlotWindow(Plot plot)
    {
        plotWindow.SetPlot(plot);
    }
}
