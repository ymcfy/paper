using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class random : MonoBehaviour
{
    public GameObject tank;
    int timer = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    void huo()
    {
        for (timer = 0; timer <= 1; timer++)
        {
            Instantiate(tank, new Vector3(Random.Range(-12f, 17f), 0, Random.Range(-6f, 3.3f)), Quaternion.identity);
        }
    }
}
