using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    // Целевой объект для следования
    public Transform target;

    // Высота камеры над целевым объектом
    public float height = 5.0f;

    // Расстояние до целевого объекта без учета высоты
    public float distance = 10.0f;

    // Насколько замедляются изменения в повороте и высоте
    public float rotationDamping = 2;
    public float heightDamping = 3;

    // Вызывается для каждого кадра
    private void LateUpdate()
    {
        //выйти, если цель не определена
        if (!target)
            return;

        // Вычислить желаемые местоположение и ориентацию
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;

        // Выяснить текущее местоположение и ориентацию
        var currentRotationAngle = transform.eulerAngles.y;
//Debug.Log("Текущий угол вращения" + currentRotationAngle);
        var currentHeight = transform.position.y;

        // Продолжить выполнять замедленный поворот вокруг оси y
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Продолжить постепенно корректировать высоты над целью
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        //Преобразовать угол в поворот
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Установить местоположение камеры в плоскости x-z
        // на расстоянии в "distance" метрах от цели
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Установить местоположение камеры, используя новую высоту
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Наконец, сориентировать объектив камеры в сторону,
        // куда направляется целевой объект
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDamping * Time.deltaTime);
    }
    
   
}
