using UnityEngine;
using System.Collections;

public class Tsunami : MonoBehaviour {


    float inc = 0;
    bool isClick = false;
    bool IAnimation = false;
    public CCat cat;
    int point = 0;
    // Use this for initialization
    void Start () {
        
        if (GameObject.Find("IntroPoint"))
            point = GameObject.Find("IntroPoint").GetComponent<IntroPoint>().point;

        if (point < 50)
        { 
            GetComponent<Animator>().SetTrigger("smallTsunami");
        }
        else
        { 
            GetComponent<Animator>().SetTrigger("Tsunami");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.GAMEOVER) return;

        if (Input.GetMouseButtonDown(0) && !IAnimation)
        {
            ScaleUp();
        }

        //water_w_pos.y += sin(water_w_pos.x + delta_time);
    }

    void ScaleUp()
    {
        
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0.9f, 0.9f, 0.9f), "easetype", "easeOutCubic", "oncomplete", "ScaleDown","speed",3));
        IAnimation = true;

    }

    void ScaleDown()
    {
        cat.Jump();   
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0.75f, 0.75f, 0.75f), "easetype", "easeInCubic", "oncomplete", "ScaleComplate", "speed", 2));
    }

    void ScaleComplate()
    {
        IAnimation = false;
    }

    //IEnumerator ScaleTsunami
}
