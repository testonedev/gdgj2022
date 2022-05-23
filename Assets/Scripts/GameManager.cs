using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Afterlife { Heaven, Hell }

public class GameManager : MonoBehaviour
{
    
    
    private HellManager hellManager;
    private HeavenManager heavenManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoulWentToAfterlife(Afterlife afterlifePlace, int alignmentPoints)
    {
        throw new System.NotImplementedException();
    }

    public static Plot GetTargetPlot(int indexNumber)
    {
        return null;
    }
}
