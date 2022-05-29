using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenManager : ParkManager
{
    public override Alignment Alignment { get => Alignment.Heaven; }

    public void SelectPlot(int indexNumber)
    {
        UIManager.Instance.SelectPlot(GetFirstPlotInRow(indexNumber));
    }
}
