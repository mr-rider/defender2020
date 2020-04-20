﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSteering : MonoBehaviour
{
    // Скорость поворота корабля
    public float turnRate = 6.0f;

    // Сила выравнивания корабля
    public float levelDamping = 1.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Создать новый поворот, умножив вектор направления джойстика
        // на turnRate, и ограничить величиной 90% от половины круга.

        // Сначала получить ввод пользователя.
        var steeringInput = InputManager.instance.steering.delta;

        // Теперь создать вектор для вычисления поворота.
        var rotation = new Vector2();

        rotation.y = steeringInput.x;
        rotation.x = steeringInput.y;

        // Умножить на turnRate, чтобы получить величину поворта.
        rotation *= turnRate;

        // Преобразовать в радианы, умножив на 90% половины круга
        rotation.x = Mathf.Clamp(rotation.x, -Mathf.PI * 0.9f, Mathf.PI * 0.9f);

        // И преобразовать радианы в кватерион поворота!
        var newOrientation = Quaternion.Euler(rotation);

        // Объдинить поворот с текущей ориентацией
        transform.rotation *= newOrientation;

        // Далее попытаемся минимизировать поворот
        // Сначала определить, какой была бы ориентация в отсутствии вращения относительно оси Z
        var levelAngles = transform.eulerAngles;
        levelAngles.z = 0.0f;
        var levelOrientation = Quaternion.Euler(levelAngles);

        // Объеденить текущую ориентацию с небольшой величиной
        // этой ориентации "без вращения" ; когда это происходит на протяжении 
        // нескольких кадров, объект медленно выравнивается над поверхностью
        transform.rotation = Quaternion.Slerp(transform.rotation, levelOrientation, levelDamping * Time.deltaTime);

    }
}
