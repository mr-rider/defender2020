using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    // Джойстик, используем для упарвления кораблем.
    public VirtualJoystick steering;

    // Задержка между выстрелами в секундах
    public float fireRate = 0.2f;

    // Текущий сценарий ShipWeapons управления стрельбой
    private ShipWeapons currentWeapons;
    

    // Содержит true, если в данный момент ведется огонь.
    private bool isFiring = true;

    // Вызывается сценарием ShipWeapons для обновления переменной currentWeapons.
    public void SetWeapons(ShipWeapons weapons)
    {
        Debug.Log("сценарий weapons" + currentWeapons);
        this.currentWeapons = weapons;
    }

    // Аналогично вызывается для сброса переменной currentWeapons.
    public void RemoveWeapons(ShipWeapons weapons)
    {
        Debug.Log("метод RemoveWeapons");
        // Если currentWeapons ссылается на данный объект 'weapons', присвоить ей null.
       if (this.currentWeapons == weapons)
        {
            Debug.Log("currentWeapons ссылается на данный объект 'weapons', присвоить ей null. ");
           this.currentWeapons = null;
        }
    }

    // Вызывается, когда пользователь касается кнопки Fire.
    public void StartFiring()
    {
        // Запустить сопрограмму ведения огня
        Debug.Log("Запустить сопрограмму ведения огня");
       StartCoroutine(FireWeapons());
        
    }

    IEnumerator FireWeapons()
    {
        Debug.Log("Ienumerator");
        // Установить признак ведения огня
        isFiring = true;

        // Продолжать итерации, пока isFiring равна true
        while (isFiring)
        {
            Debug.Log(" while isfiring");
            // Если сценарии управления оружием зарегистрирован, сообщить ему о необходимости произветси выстрел!
            if (this.currentWeapons != null)
            {
                currentWeapons.Fire();
            }

            // Ждать fireRate секунд перед следующим выстрелом
            yield return new WaitForSeconds(fireRate);
        }
    }

    // Вызывается, когда пользователь убирает палец с кнопки Fire
    public void StopFiring()
    {
        // Присвоить false, чтобы завершить цикл в FireWeapons
        isFiring = false;
    }
}