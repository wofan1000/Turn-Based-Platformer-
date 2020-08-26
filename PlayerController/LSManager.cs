using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{

    public LSPlayer theplayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadLvl()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        LSUIController.instance.FadeToBlack();

        yield return new WaitForSeconds(1f / LSUIController.instance.fadeSpeed);

        SceneManager.LoadScene(theplayer.currentPoint.lvlToLoad);
    }
}
