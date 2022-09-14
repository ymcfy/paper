using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class round : MonoBehaviour
{
    private float turn;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        {
            turn += (10 * Time.deltaTime);

            this.transform.rotation = Quaternion.Euler(0, turn, 0);
            //(围绕物体位置坐标，旋转轴，旋转角度  )
        }

    }
}
