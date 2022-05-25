using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravestone : MonoBehaviour
{
    public Vector2 minMaxScale = new Vector2(0.8f, 1.2f);
    private Vector3 defaultScale;

    private Vector3 newScale;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;

        newScale = new Vector3(Random.Range(minMaxScale.x, minMaxScale.y) * defaultScale.x,
            Random.Range(minMaxScale.x, minMaxScale.y) * defaultScale.y,
            Random.Range(minMaxScale.x, minMaxScale.y) * defaultScale.z);

        transform.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
