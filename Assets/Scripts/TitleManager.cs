using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleManager : Singleton<TitleManager> {

    void Start()
    {
        Time.timeScale = 1.0f;
        if (GameObject.Find("IntroPoint") != null)
            Destroy(GameObject.Find("IntroPoint"));

        GameManager.GAMEOVER = false;
    }

    public void OnGameStart()
    {
        SceneManager.LoadScene("PrePlay");
    }
}
