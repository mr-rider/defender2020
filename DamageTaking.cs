﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaking : MonoBehaviour
{
    // Число очков прочности данного объекта
    public int hitPoints = 10;

    // При разрушении создать этот объект в текущей позиции
    public GameObject destructionPrefab;

    // Завершить игру при разрушении данного объетка?
    public bool gameOverOnDestroyed = false;

    // Вызывается другими объектами (например, астероидами и шарами плазмы) для нанесения повреждений
    public void TakeDamage(int amount)
    {
        // Сообщить о попадании в текущий объект
        Debug.Log(gameObject.name + " damaged!");

        // Вычесть amount из числа очков прочности
        hitPoints -= amount;

        // Очки исчерпаны?
        if(hitPoints <=0)
        {
            // Зафиксировать этот факт
            Debug.Log(gameObject.name + " destroyed!");

            // Удалить себя из игры
            Destroy(gameObject);

            // Задан шаблон для создания объекта в точку разрушения?
            if (destructionPrefab != null)
            {
                // Создать объект в текущей позиции с текущей ориентацией.
                Instantiate(destructionPrefab, transform.position, transform.rotation);
            }

            // Если требуется завершить игру, вызвать метод GameOver класса GameManager.
            if (gameOverOnDestroyed == true)
            {
                GameManager.instance.GameOver();
            }
        }
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
