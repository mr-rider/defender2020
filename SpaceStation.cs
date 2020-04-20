using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IndicatorManager.instance.AddIndicator(gameObject, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
