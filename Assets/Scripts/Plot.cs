using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{   
    public Afterlife alignment;
    public int indexNumber;
    private ParkManager parkManager;
    private int upgradeLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual int GetAlignmentPoints()
    {
        //throw new System.NotImplementedException();

        return 3;
    }

    public bool UpgradePlot()
    {
        return true;
    }
}
