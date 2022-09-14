using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluentTex1 : MonoBehaviour {
    public Material m;
    public float moveSpeed = 5f;
    public float dir;
   public float time=0;

    void Start()
    {
        this.GetComponent<Renderer>().material = m;
        m.SetTextureScale("_MainTex", new Vector2(0, 0.2f));
        m.SetTextureOffset("_MainTex", new Vector2(0, 0));

    }


    void Update()
    {
        this.GetComponent<Renderer>().material = m;
        if (m)
        {
            time += Time.deltaTime;
                dir = time * moveSpeed;
                m.SetTextureOffset("_MainTex", new Vector2(0, -dir));
            
        }
    }
}
