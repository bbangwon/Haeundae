using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

    public static bool GAMEOVER = false;
    public static bool NEXTSTATE = false;

    public int level = 0;

    public Transform levelScale;

    public CCreateBuildingManager createBuildingManager;
    public UIManager uiManager;

    public CCat cat;

    public CMountain mountain;

    public Transform civilStartPoints;
    public GameObject civil;

    public ScrollManager[] scrollManagers;

    public CBuildingMove _effectBuilding;

    public CBackSpriteChage backSpriteChage;

    public int mountainStartNumber = 1;
    public int mountainEndNumber = 2;
    public int nextStateSpriteNumber = 3;
    public int nextMountainStartNumber = 4;

    public int stateUp = 0;

    bool mountainStart = false;
    bool mountainEnd = false;


    // Use this for initialization
    void Start () {
		GAMEOVER = false;
        NEXTSTATE = false;
        StartCoroutine(createCivils());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GameOver()
    {

        foreach (ScrollManager s in scrollManagers)
            s.scrollSpeed = 0.0f;

        foreach (Civil c in GameObject.FindObjectsOfType<Civil>())
            c.speed = -1.0f;


        createBuildingManager.GameOver();
        uiManager.GameOverPopUp();
        mountain.MountainMoveStop();
        cat.Die();
        GAMEOVER = true;
        // GameEnd 팝업창
    }

    public void GameReStart()
    {
        Time.timeScale = 1.0f;
        if (GameObject.Find("IntroPoint") != null)
            Destroy(GameObject.Find("IntroPoint"));

        GameManager.GAMEOVER = false;
        SceneManager.LoadScene("PrePlay");
    }
    public void GameExit()
    {
        Application.Quit();
        //Exit
    }
    public void GameHome()
    {
        SceneManager.LoadScene("Title");
    }
    public void LevelUp()
    {
        level++;
        if (_effectBuilding != null)
            _effectBuilding.ShowEffect();

        if(level % 5 ==0)
        {
            ScaleLevelUp();
            stateUp++;
            Debug.Log(NEXTSTATE);
        }
        if(!mountainStart&&mountainStartNumber == stateUp)
        {
            mountainStart = true;
            mountain.MountainMoveStart();
        }
        
        if(!mountainEnd&&mountainEndNumber == stateUp)
        {
            mountainEnd = true;
            mountain.MountainMoveNext();
            mountainStart = true;

            backSpriteChage.NextBackSprite();

        }
        if(nextStateSpriteNumber == stateUp)
        {
            NEXTSTATE = true;
            mountainStart = false;
        }
        if (!mountainStart && nextMountainStartNumber == stateUp)
        {
            mountainStart = true;
            mountain.MountainMoveStart();
        }

    }

    public void ScaleLevelUp()
    {
        if(levelScale.localScale.x > 0.5f)
        {
            StartCoroutine(LevelCoroutine());
        }
    }

    IEnumerator LevelCoroutine()
    {
        int upup = 10;
        float upupSpeed = createBuildingManager.buildingSpeedSeting * 0.06f;
        //float upupBuildingCreateUp = createBuildingManager.
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            levelScale.localScale = new Vector3(levelScale.localScale.x - 0.01f,
                levelScale.localScale.x - 0.01f, 1);
            createBuildingManager.SpeedChage(upupSpeed);
            uiManager.scoreTiem -= 0.01f;
            upup--;
            if(upup <1)
            {
                break;
            }
        }
        Debug.Log(uiManager.scoreTiem);
    }

    IEnumerator createCivils()
    {
         while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 3.0f));

            if (GAMEOVER == true)
                break;

            HashSet<int> hi = new HashSet<int>();

            hi.Add(Random.Range(0, civilStartPoints.childCount));
 
            foreach(int n in hi)
            {
                GameObject go = Instantiate(civil, civilStartPoints.GetChild(n).position, Quaternion.Euler(0f, 180f, 0f)) as GameObject;
                go.transform.parent = civilStartPoints;
                go.transform.localScale = Vector3.one;
                go.GetComponent<Civil>().speed = CCreateBuildingManager.buildingSpeed - CCreateBuildingManager.buildingSpeed * 0.1f;
            }


        }
    }

   
}
