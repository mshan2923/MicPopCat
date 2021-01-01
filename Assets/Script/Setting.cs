using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    MicScript mic;
    public GameObject ImageObj;
    public GameObject SettingMenu;
    
    [Space(10)]
    public Text InputStrong;
    public InputField StartVaule;
    public Button ReverseButton;
    void Start()
    {
        mic = GetComponent<MicScript>();
        StartVaule.text = mic.OverVaule.ToString("0.#");
        StartVaule.onValueChanged.AddListener(EditStartVaule);

        ReverseButton.onClick.AddListener(ReverseEvent);
    }

    // Update is called once per frame
    void Update()
    {
        InputStrong.text = mic.loudness.ToString("0.#");
        if(SettingMenu.activeSelf == false && Input.GetMouseButtonDown(0))
        {
            SettingMenu.SetActive(true);
        }
    }


    void EditStartVaule(string vaule)
    {
        mic.OverVaule = float.Parse(vaule);
    }
    void ReverseEvent()
    {
        var S = ImageObj.transform.localScale;
        ImageObj.transform.localScale = new Vector3(S.x * -1, S.y, S.z);
    }
}
