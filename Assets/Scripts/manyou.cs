using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;
/// <summary>
/// 挂接在main camera上
/// </summary>
public class manyou : MonoBehaviour
{
    public Material[] mat1;
    public Material[] mat2;
    public GameObject a;
    public GameObject b;
    public GameObject Ground;
    public GameObject Road;
    public GameObject Road1;
    /// <summary>
    /// 道路上的箭头，这个在地图透明时也应该取消,代号EAQDYHQJZ04
    /// </summary>
    public GameObject RoadArrow;
    /// <summary>
    /// 道路旁的细长竿子，这个在地图透明时也应该取消，代号EOQDYHQXP05
    /// </summary>
    public GameObject SlenderRod;
    /// <summary>
    /// 道路旁的矮粗石墩，这个在地图透明时也应该取消，代号EOQDYHQXP23
    /// </summary>
    public GameObject LowRoughStonePier;
    /// <summary>
    /// 测量工具UI条
    /// </summary>
    public GameObject measureUI;
    /// <summary>
    /// 漫游模块主UI条
    /// </summary>
    public GameObject roamUI;
    /// <summary>
    /// 是否透明 用于判断是调用透明函数还是取消透明函数
    /// true为透明，false为不透明
    /// </summary>
    private bool isLucency = false;
    /// <summary>
    /// 打开测量工具UI条
    /// </summary>
    public void measure()
    {
        measureUI.SetActive(true);
        roamUI.SetActive(false);
    }
    /// <summary>
    /// 通过isLucency变量控制是否取消透明，初始为不透明
    /// </summary>
    public void Lucency()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        if (isLucency == true)
        {
            //此时为透明，需要取消透明
            quxiaotouming();
            isLucency = false;
        }
        else
        {
            //此时为不透明，需要透明
            dimiantouming();
            isLucency = true;
        }
    }
    // Use this for initialization
    void Start()
    {
    }
    public void zidongmanyou()
    {
        StartCoroutine(shijiao());
    }
    public void home()
    {
        transform.DOMove(new Vector3(45.2488f, 10.66472f, 37.14768f), 0.1f);
        transform.DORotate(new Vector3(3f, -91f, 0f), 0.1f);
    }
    /// <summary>
    /// 地图透明
    /// </summary>
    public void dimiantouming()
    {
        //this.transform.position = new Vector3(7.579256f, 12, -70.16784f);
        //this.transform. rotation = Quaternion.Euler(22.8f, 88.3f, 0);
        //MeshRenderer[] mesh = a.GetComponentsInChildren<MeshRenderer>();
        //foreach (var item in mesh)
        //{
        //    setMaterialRenderingMode(item.material, RenderingMode.Fade);
        //}
        //MeshRenderer[] mesh1 = b.GetComponentsInChildren<MeshRenderer>();
        //foreach (var item1 in mesh)
        //{
        //    setMaterialRenderingMode(item1.material, RenderingMode.Fade);
        //}
        Ground.SetActive(false);
        Road.SetActive(false);
        Road1.SetActive(false);
        RoadArrow.SetActive(false);
        SlenderRod.SetActive(false);
        LowRoughStonePier.SetActive(false);
    }
    /// <summary>
    /// 取消地面透明
    /// </summary>
    public void quxiaotouming()
    {
        //this.transform.position = new Vector3(40, 12, -111);
        //this.transform.rotation = Quaternion.Euler(0,0,0);
        //MeshRenderer[] mesh = a.GetComponentsInChildren<MeshRenderer>();
        //foreach (var item in mesh)
        //{
        //    setMaterialRenderingMode(item.material, RenderingMode.Opaque);
        //}
        //MeshRenderer[] mesh1 = b.GetComponentsInChildren<MeshRenderer>();
        //foreach (var item1 in mesh)
        //{
        //    setMaterialRenderingMode(item1.material, RenderingMode.Opaque);
        //}
        //EAQDYHQJZ03  EAQDYHQJZ02
        Ground.SetActive(true);
        Road.SetActive(true);
        Road1.SetActive(true);
        RoadArrow.SetActive(true);
        SlenderRod.SetActive(true);
        LowRoughStonePier.SetActive(true);
    }
    private IEnumerator shijiao()
    {
        transform.DOMove(new Vector3(1f, 0f, 13f), 3f);
        transform.DORotate(new Vector3(8f, -90f, 0f), 3f);
        yield return new WaitForSeconds(3f);
        transform.DOMove(new Vector3(2f, 0f, 60f), 8f);
        transform.DORotate(new Vector3(10f, -90f, 0f), 8f);
        yield return new WaitForSeconds(8f);
        transform.DOMove(new Vector3(-18f, 36f, 40f), 2f);
        transform.DORotate(new Vector3(34f, -110f, 0f), 2f);
        yield return new WaitForSeconds(2f);
        transform.DOMove(new Vector3(-31f, 0f, 42f), 2f);
        transform.DORotate(new Vector3(3f, -120f, 0f), 2f);
        yield return new WaitForSeconds(2f);
        transform.DOMove(new Vector3(-35f, -1f, -10f), 7f);
        transform.DORotate(new Vector3(-10f, -170f, 0f), 7f);
        yield return new WaitForSeconds(8f);
        transform.DOMove(new Vector3(-55f, -1f, -25f), 3f);
        transform.DORotate(new Vector3(-0f, -10f, 0f), 3f);
        yield return new WaitForSeconds(3f);
        transform.DOMove(new Vector3(-52f, 0f, 6f), 5f);
        transform.DORotate(new Vector3(24f, -13f, 0f), 5f);
        yield return new WaitForSeconds(5f);
        transform.DOMove(new Vector3(-60f, 0f, 13f), 4f);
        transform.DORotate(new Vector3(24f, -13f, 0f), 4f);
        yield return new WaitForSeconds(4f);
        transform.DOMove(new Vector3(-60f, 0f, 48f), 4f);
        transform.DORotate(new Vector3(24f, -13f, 0f), 4f);
        yield return new WaitForSeconds(4f);
        transform.DOMove(new Vector3(-75f, 0f, 55f), 3f);
        transform.DORotate(new Vector3(24f, -138f, 0f), 3f);
        yield return new WaitForSeconds(3f);
        transform.DOMove(new Vector3(-109f, 6f, 15f), 3f);
        transform.DORotate(new Vector3(24f, -138f, 0f), 3f);
        yield return new WaitForSeconds(3f);
        transform.DOMove(new Vector3(-130f, 0, 31f), 4f);
        transform.DORotate(new Vector3(24f, -138f, 0f), 4f);
        yield return new WaitForSeconds(4f);
        transform.DOMove(new Vector3(-265f, 1f, -73f), 18f);
        transform.DORotate(new Vector3(10f, -90f, 0f), 18f);
        yield return new WaitForSeconds(18f);
        transform.DOMove(new Vector3(-300f, 1f, -42f), 4f);
        transform.DORotate(new Vector3(20f, 55f, 0f), 4f);
        yield return new WaitForSeconds(4f);
        transform.DOMove(new Vector3(-236f, 0f, 0f), 12f);
        transform.DORotate(new Vector3(20f, 55f, 0f), 12f);
        yield return new WaitForSeconds(12f);
        transform.DOMove(new Vector3(-166f, 203f, -7f), 4f);
        transform.DORotate(new Vector3(90f, 180f, 0f), 4f);
        yield return new WaitForSeconds(4f);
    }
    // Update is called once per frame
    void Update()
    {
    }
    public enum RenderingMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }
    private void setMaterialRenderingMode(Material material, RenderingMode renderingMode)
    {
        switch (renderingMode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}
