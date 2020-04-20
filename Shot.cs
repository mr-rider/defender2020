using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Перемещает объект вперед с постоянной скоростью и уничтожает его спустя заданный промежуток времени.

public class Shot : MonoBehaviour
{
    // Скорость движения снаряда
    public float speed = 100.0f;

    // Время в секундах, через которое следует уничтожить снаряд
    public float life = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Уничтожить через 'life' секунд
        Destroy(gameObject, life);
    }

    // Update is called once per frame
    void Update()
    {
        // перемещать вперед с постоянной скоростью
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }
}
