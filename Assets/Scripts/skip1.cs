using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skip1 : MonoBehaviour
{
    public void changjingmanyou()
    {
        SceneManager.LoadScene("1");
    }
    public void changjingguanli()
    {
        SceneManager.LoadScene("2");
    }
    public void shujuguanli()
    {
        SceneManager.LoadScene("3");
    }
    public void kechengpeixun()
    {
        SceneManager.LoadScene("4");
    }
    public void tuichu()
    {
        Application.Quit();
    }
}

