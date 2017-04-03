using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

    public Sprite handImage1;
    public Sprite handImage2;
    public Sprite handImage3;

    public GameObject finger;
    bool bSwiped = false;
    bool bUp = false;

    public GameObject background;

    public GameObject bar;

    public IntroPoint introPoint;

    public enum fingerState
    {
        RequestSwiped,
        RequestUp
    }

    public fingerState fingerStat = IntroManager.fingerState.RequestSwiped;


    // Use this for initialization
    void Start () {
        finger.SetActive(true);
        finger.GetComponent<Animator>().SetTrigger("Swiped");
        StartCoroutine("IntroPlay");

    }
	
	// Update is called once per frame
	void Update () {
	}

    public void swiped()
    {       
         bSwiped = true;
        
    }

    public void up()
    {
       // if (fingerStat == fingerState.RequestUp)
            bUp = true;
    }

    IEnumerator IntroPlay()
    {
        // 1. 스와이프 요구

        while (true)
        {
            if (fingerStat == fingerState.RequestSwiped)
            {
                if (bSwiped == true)
                {
                    finger.SetActive(false);
                    finger.transform.position = new Vector3(0f, -2.8f, 0f);
                    break;
                }
                yield return null;
            }
        }

        // 연출
        background.GetComponent<SpriteRenderer>().sprite = handImage2;
        yield return new WaitForSeconds(0.2f);
        background.GetComponent<SpriteRenderer>().sprite = handImage3;
        yield return new WaitForSeconds(0.2f);

        fingerStat = fingerState.RequestUp;
        finger.SetActive(true);
        finger.GetComponent<Animator>().SetTrigger("Up");
        StartCoroutine("GageBarMove");  



        while (true)
        {
            if (fingerStat == fingerState.RequestUp)
            {
                if(bUp == true)
                {
                    finger.SetActive(false);
                    break;
                }
            }
            yield return null;
        }

        background.GetComponent<SpriteRenderer>().sprite = handImage2;
        yield return new WaitForSeconds(0.1f);
        background.GetComponent<SpriteRenderer>().sprite = handImage1;
        yield return new WaitForSeconds(0.1f);

        yield return null;
    }

    IEnumerator GageBarMove()
    {
        //bar 이동
        float speed = 20.0f;

        while(true)
        {
            if (bar.transform.position.x >= 4.2f)
            {
                speed = -20.0f;
            }
            else if (bar.transform.position.x <= -4.2f)
            {
                speed = 20.0f;
            }

            bar.transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (bUp == true)
                break;

            yield return null;
        }

        introPoint.point = (int)((-Mathf.Abs(bar.transform.position.x) + 4.2f) / 4.2f * 100.0f);
        SceneManager.LoadScene("inGame");
        

     yield return null;


    }
}
