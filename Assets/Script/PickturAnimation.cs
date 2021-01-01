using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickturAnimation : MonoBehaviour
{
    public GameObject image;
    public Sprite[] images;
    public float delay = 0.1f;
    int index = 0;
    Coroutine CoroutineLoop;

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
}
