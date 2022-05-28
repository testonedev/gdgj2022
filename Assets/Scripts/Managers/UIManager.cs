using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvas canvasPrefab;

    private Canvas canvas;
    private PlotWindowUI plotWindow;
    private WinPanelUI hellWins;
    private WinPanelUI heavenWins;

    //runtime
    private WinPanelUI[] allWinPanels;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        canvas = Instantiate(canvasPrefab);
        plotWindow = canvas.GetComponentInChildren<PlotWindowUI>();

        //requires panel game objects to be enabled after instantiation to find them
        allWinPanels = canvas.GetComponentsInChildren<WinPanelUI>();
        foreach (WinPanelUI item in allWinPanels)
        {
            if (item.alignment == Alignment.Heaven) heavenWins = item;
            else if(item.alignment == Alignment.Hell) hellWins = item;

            item.gameObject.SetActive(false);
        }
    }

    public void ShowPlotWindow(Plot plot)
    {
        plotWindow.SetPlot(plot);
    }

    public static void ShowAlignmentWinner(Alignment winner)
    {
        if(winner == Alignment.Heaven) Instance.heavenWins.gameObject.SetActive(true);
        else if (winner == Alignment.Hell) Instance.hellWins.gameObject.SetActive(true);
    }
}
