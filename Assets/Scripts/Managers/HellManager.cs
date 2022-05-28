using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellManager : ParkManager
{
    public override Alignment Alignment { get => Alignment.Hell; }
    public float upgradeInterval = 3f;

    //runtime
    private float nextTime;

    protected override void Start()
    {
        base.Start();

        nextTime = Time.time + upgradeInterval;
    }

    protected override void Update()
    {
        base.Update();

        if (nextTime <= Time.time) TryUpgrading();
    }

    private void TryUpgrading()
    {
        //tries randomly to upgrade a plot
        plots[Random.Range(0, 6), 0].UpgradePlot();

        nextTime = Time.time + upgradeInterval;
    }
}
