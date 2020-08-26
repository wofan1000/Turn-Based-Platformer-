using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{
    public static LSUIController instance;

    public Image FadeScreen;
    public float fadeSpeed;

    public bool fadeToBlack;
    public bool fadeFromBlack;

    public GameObject lvlInfoPanal;

    public Text lvlName;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        fadeFromBlack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b,
                Mathf.MoveTowards(FadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (FadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b,
                Mathf.MoveTowards(FadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (FadeScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }


    public void FadeToBlack()
    {
        fadeToBlack = true;
        fadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        fadeFromBlack = true;
        fadeToBlack = false;
    }

    public void showInfo(MapPoint lvlInfo)
    {
        lvlName.text = lvlInfo.lvlName;
        lvlInfoPanal.SetActive(true);

    }

    public void HideInfo()
    {
        lvlInfoPanal.SetActive(false);
    }

}
