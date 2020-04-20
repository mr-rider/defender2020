using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    //Показывает предупреждающую рамку, когда игрок улетает слишком далеко от центра
    public float warningRadius = 400.0f;

    // Расстояние от центра, удаление на которое вызывает завершение игры
    public float destroyRadius = 450.0f;

    public void OnDrawGizmosSelected()
    {
        // Желтым цветом показывать сферу предупреждения
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, warningRadius);

        // ...а красным - сферу уничтожения
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, destroyRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
