using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapons : MonoBehaviour
{
    // Шаблон для создания снарядов
    public GameObject shotPrefab;

    public void Awake()
    {
        // когда данный объект запускается, сообщить диспетчеру ввода,
        // чтобы использовать его как текущий сценарий управления оружием
        InputManager.instance.SetWeapons(this);
        Debug.Log("Сообщить диспетчеру ввода");

    }

    // Вызывется при удалении объекта
    public void OnDestroy()
    {
        //Ничего не делать, если вызывается не в режиме игры
        if(Application.isPlaying != true)
{
            Debug.Log("isPlaying != true");
            InputManager.instance.RemoveWeapons(this);
        }
    }

    // Список пушек для стрельбы
    public Transform[] firePoints;
    

    // Индекс в firePoints, указывающий на следующую пушку
    private int firePointIndex;

    // Вызывается диспетчером ввода InputManager.
    public void Fire()
    {
        Debug.Log("Сценарий Fire");
        // Если пушки отсутвуют, выйти
        if (firePoints.Length == 0)
        {
            Debug.Log("Пушкек нет");
            return;
        }

        // Определить следующую пушку для выстрела
        var firePointToUse = firePoints[firePointIndex];

        // Создать новый снаряд с ориентацией, соответсвующей пушке
        Instantiate(shotPrefab, firePointToUse.position, firePointToUse.rotation);

        // Если пушка имеет компонент источника звука, воспроизветси звуковой эффект
        var audio = firePointToUse.GetComponent<AudioSource>();
        Debug.Log(audio);
        if (audio)
        {
            Debug.Log(" Play blaster sound");
            audio.Play();
        }

        // Перейти к следующей пушке
        firePointIndex++;

        // Если произошел выход за границы массива, вернуться к его началу
        if (firePointIndex >= firePoints.Length)
            firePointIndex = 0;
    }

    
}
