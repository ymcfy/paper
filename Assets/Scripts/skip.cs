using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System;
public class skip : MonoBehaviour
{
    public GameObject ChangJingManYou;
    public GameObject KeChengPeiXun;
    public GameObject RawImage;
    #region  最小化代码
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();
    const int SW_SHOWMINIMIZED = 2; //{最小化, 激活} 
    const int SW_SHOWMAXIMIZED = 3;//最大化 
    const int SW_SHOWRESTORE = 1;//还原 
    public void Min()
    {
        ShowWindow(GetActiveWindow(), SW_SHOWMINIMIZED);
    }
    #endregion
    public void Skip()
    {
        //SceneManager.LoadScene("0");
        ChangJingManYou.SetActive(true);
        KeChengPeiXun.SetActive(true);
        RawImage.SetActive(false);
    }
    public void skip4()
    {
        SceneManager.LoadScene("1");
    }
    public void skip3()
    {
        SceneManager.LoadScene("3");
    }
    public void SkipToModel2()
    {
        SceneManager.LoadScene("2");
    }
    /// <summary>
    /// 退出系统
    /// </summary>
    public void exitSystem()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
