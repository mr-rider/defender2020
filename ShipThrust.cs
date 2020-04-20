using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrust : MonoBehaviour
{
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Перемещает корабль вперед с постоянной скоростью
    // Update is called once per frame
    void Update()
    {
        var offset = Vector3.forward * Time.deltaTime * speed;
        this.transform.Translate(offset);
    }
}
