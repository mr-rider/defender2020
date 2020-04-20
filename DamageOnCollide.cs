using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    // Объем повреждений, наносимых объекту.
    public int damage = 1;
    

    // Объем повреждений, наносимых себе при попадании в какой-то объект.
    public int damageToSelf = 5;

    void HitObject(GameObject theObject)
    {
        // Нанести повреждение объекту, в который попал данный объект, если возможно.
        var theirDamage = theObject.GetComponentInParent<DamageTaking>();
        if (theirDamage)
        {
            var targetTag = theObject.tag;
            Debug.Log(" Попали в цель" + targetTag);
            theirDamage.TakeDamage(damage);
            if (targetTag == "Asteroid")
            {
                 GameObject.Find("Game Manager").GetComponent<GameManager>().asteroidCount +=1;
               

                
                Debug.Log(" Количестов сбитых астероидов " + GameObject.Find("Game Manager").GetComponent<GameManager>().asteroidCount);
            }
        }

        // Нанести повреждение себе, если возможно
        var ourDamage = this.GetComponentInParent<DamageTaking>();
        if (ourDamage)
        {
            ourDamage.TakeDamage(damageToSelf);
        }
    }

    // Объект вошел в область действия данного триггера?
    private void OnTriggerEnter(Collider collider)
    {
        HitObject(collider.gameObject);
    }

    // Другой объект столкнулся с текущим объектом?
    private void OnCollisionEnter(Collision collision)
    {
        HitObject(collision.gameObject);
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
