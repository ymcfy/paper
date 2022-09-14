using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{

    private int countint=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Switch()
    {
        if (countint==0)
        {
            gameObject.SetActive(true);
            countint++;
        }
        else if(countint==1)
        {
            gameObject.SetActive(false);
            countint = 0;
        }
    }
        
}
