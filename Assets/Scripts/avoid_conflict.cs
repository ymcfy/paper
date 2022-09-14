using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avoid_conflict : MonoBehaviour
{
    private bool t = true;
    void Start()
    {

    }
    void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<Volume>().enabled == true)
        {
            this.GetComponent<CameraControl>().enabled = false;
        }
        else
        {
            //this.GetComponent<CameraControl>().enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.O))
        {

            if (t == true)
            {
                this.GetComponent<CameraControl>().enabled = false;
                t = false;
            }
            else
            {
                this.GetComponent<CameraControl>().enabled = true;
                t = true;
            }
        }
    }
}
