using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carcontrol : MonoBehaviour
{
    public Transform m_RigidBody;
    float speed = 6f;

    private void FixedUpdate()
    {
        Move();
        Turn();
    }
    private void Start()
    {
        m_RigidBody.GetComponent<Rigidbody>();
        m_RigidBody.rotation = Quaternion.Euler(0, -90f, 0);
    }
    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        // 注意这里的forward
        Vector3 movement = transform.forward * v * speed * Time.deltaTime * -1;
        m_RigidBody.position = (m_RigidBody.position + movement);
    }

    private void Turn()
    {
        float h = Input.GetAxis("Horizontal");
        float turn = h * 50f;
        //Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        Quaternion turn1 = Quaternion.Euler(m_RigidBody.rotation.x, m_RigidBody.rotation.y + turn, m_RigidBody.rotation.z);
        m_RigidBody.rotation = (turn1);
    }
}