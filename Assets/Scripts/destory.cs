using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class destory : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(startTime());   //运行一开始就进行协程
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.GetComponent<MeshCollider>());
    }
    public int TotalTime = 3;//总时间



    public IEnumerator startTime()
    {

        while (TotalTime >= 0)
        {

            //Debug.Log(TotalTime);//打印出每一秒剩余的时间

            yield return new WaitForSeconds(1);//由于开始倒计时，需要经过一秒才开始减去1秒，
                                               //所以要先用yield return new WaitForSeconds(1);然后再进行TotalTime--;运算
            TotalTime--;


            if (TotalTime <= 0)
            {                //如果倒计时剩余总时间为0时，就跳转场景

                this.gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}