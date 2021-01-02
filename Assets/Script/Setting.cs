using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    MicScript mic;
    PictureAnimation PictureAnim;

    public GameObject ImageObj;
    public GameObject SettingMenu;
    
    [Space(10)]
    public Text InputStrong;
    public InputField StartVaule;
    public Button ReverseButton;

    public InputField SpeedInput;
    public Scrollbar SpeedScrollbar;

    [Space(10)]
    public float MinSpeed = 0.01f;
    public float MaxSpeed = 1;

    void Start()
    {
        mic = GetComponent<MicScript>();
        PictureAnim = GetComponent<PictureAnimation>();

        StartVaule.text = mic.OverVaule.ToString("0.#");
        StartVaule.onEndEdit.AddListener(EditStartVaule);

        ReverseButton.onClick.AddListener(ReverseEvent);

        SpeedInput.onEndEdit.AddListener(SpeedInputEvent);
        SpeedScrollbar.onValueChanged.AddListener(SpeedScrollbarEvent);


        SpeedInput.text = PictureAnim.delay.ToString();
        SpeedScrollbar.value = PictureAnim.delay;
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
        mic.Save(float.Parse(vaule));
    }
    void ReverseEvent()
    {
        var S = ImageObj.transform.localScale;
        ImageObj.transform.localScale = new Vector3(S.x * -1, S.y, S.z);
    }
    void SpeedInputEvent(string vaule)
    {
        float Lvaule = float.Parse(vaule);

        if(Lvaule >= MinSpeed && Lvaule <= MaxSpeed)
        {

        }else if(Lvaule < MinSpeed)
        {
            Lvaule = MinSpeed;
            SpeedInput.text = Lvaule.ToString();
        }else if(Lvaule > MaxSpeed)
        {
            Lvaule = MaxSpeed;
            SpeedInput.text = Lvaule.ToString();
        }

        PictureAnim.delay = Lvaule;
        PictureAnim.Save(Lvaule);
        SpeedScrollbar.value = Lvaule;
    }
    void SpeedScrollbarEvent(float vaule)
    {
        float Lvaule = vaule;
        if(Lvaule < MinSpeed)
        {
            Lvaule = MinSpeed;
        }

        PictureAnim.delay = Lvaule;
        PictureAnim.Save(Lvaule);
        SpeedInput.text = Lvaule.ToString("0.##");
    }
}
