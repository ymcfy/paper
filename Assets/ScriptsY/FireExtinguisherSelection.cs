using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisherSelection : MonoBehaviour {

    public GameObject LengQueShui;
    public GameObject PaoMo;
    public GameObject HunHe;
    public GameObject LengQueShui_choose;
    public GameObject PaoMo__choose;
    public GameObject HunHe__choose;
    public GameObject game_check;
    public GameObject miehuoqi_ca;
    // Use this for initialization
    void Start () {
        LengQueShui.SetActive(false);
        PaoMo.SetActive(false);
        HunHe.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //miehuoqi_ca.GetComponent<mouse>().gameCheck = game_check;
	}

    public void LengQueShuiSelection() {
        LengQueShui.SetActive(true);
        PaoMo.SetActive(false);
        HunHe.SetActive(false);
        //game_check = LengQueShui_choose;
        LengQueShui_choose.AddComponent<SpectrumController>();
        LengQueShui_choose.AddComponent<HighlightableObject>();
        if (PaoMo__choose.GetComponent<SpectrumController>() != null)
        {
            Destroy(PaoMo__choose.GetComponent<SpectrumController>());
            Destroy(PaoMo__choose.GetComponent<HighlightableObject>());
        }
        if (HunHe__choose.GetComponent<SpectrumController>() != null)
        {
            Destroy(HunHe__choose.GetComponent<SpectrumController>());
            Destroy(HunHe__choose.GetComponent<HighlightableObject>());
        }
    }
    public void PaoMoSelection()
    {
        PaoMo.SetActive(true);
        LengQueShui.SetActive(false);
        HunHe.SetActive(false);
        //game_check = PaoMo__choose;
        PaoMo__choose.AddComponent<SpectrumController>();
        PaoMo__choose.AddComponent<HighlightableObject>();
        if (LengQueShui_choose.GetComponent<SpectrumController>() != null)
        {
            Destroy(LengQueShui_choose.GetComponent<SpectrumController>());
            Destroy(LengQueShui_choose.GetComponent<HighlightableObject>());
        }
        if (HunHe__choose.GetComponent<SpectrumController>() != null)
        {
            Destroy(HunHe__choose.GetComponent<SpectrumController>());
            Destroy(HunHe__choose.GetComponent<HighlightableObject>());
        }
    }
    public void HunHeSelection()
    {
        HunHe.SetActive(true);
        PaoMo.SetActive(false);
        LengQueShui.SetActive(false);
        //game_check = HunHe__choose;
        HunHe__choose.AddComponent<SpectrumController>();
        HunHe__choose.AddComponent<HighlightableObject>();
        if (LengQueShui_choose.GetComponent<SpectrumController>() != null)
        {
            Destroy(LengQueShui_choose.GetComponent<SpectrumController>());
            Destroy(LengQueShui_choose.GetComponent<HighlightableObject>());
        }
        if (PaoMo__choose.GetComponent<SpectrumController>() != null)
        {
            Destroy(PaoMo__choose.GetComponent<SpectrumController>());
            Destroy(PaoMo__choose.GetComponent<HighlightableObject>());
        }
    }

}
