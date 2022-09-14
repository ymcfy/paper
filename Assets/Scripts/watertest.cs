using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watertest : MonoBehaviour
{
    private ParticleSystem par;
    void Start()
    {
        par = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            par.Play();

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            par.Stop();
        }
    }
}