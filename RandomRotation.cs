using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    // Start is called before the first frame update
    void Start()
    {
        m_EulerAngleVelocity = new Vector3(Random.Range(4, 50), Random.Range(10,100), 0);

        m_Rigidbody = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
