using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureAnimation : MonoBehaviour
{
    public GameObject image;
    public Sprite[] images;
    public float delay = 0.1f;
    int index = 0;
    Coroutine CoroutineLoop;
    SaveLoad<floatData> saveLoad = new SaveLoad<floatData>();

    private void Awake()
    {
        floatData temp = new floatData();
        bool v = saveLoad.Load("Delay", Application.dataPath, out temp);
        if(temp != null)
        if (temp.Data != 0)
        {
            delay = temp.Data;
        }
    }
    void Start()
    {
        GetComponent<MicScript>().SetUp(speaked);
    }

    void speaked(bool b)
    {
        if(b)
        {
            CoroutineLoop = StartCoroutine(Loop());
        }else
        {
            StopCoroutine(CoroutineLoop);
            image.GetComponent<Image>().sprite = images[0];
        }
    }

    IEnumerator Loop()
    {
        yield return new WaitForSeconds(delay);
        image.GetComponent<Image>().sprite = images[index];

        index++;
        if(index + 1 > images.Length)
        {
            index = 0;
        }

        CoroutineLoop = StartCoroutine(Loop());
    }
    public void Save(float delay)
    {
        var v = new floatData(delay);
        saveLoad.Save(v, "Delay", Application.dataPath);
    }
}
