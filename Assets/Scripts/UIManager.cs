using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] number;

    public Image[] inGameNumber;
    public Image[] gameOverNumber;

    public GameObject gameOverPopUp;
    public GameObject inGameUi;

    public float scoreTiem = 0.6f;

    int score = 0;

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        StartCoroutine(ScoreCoroutine());
	}
	IEnumerator ScoreCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(scoreTiem);
            score ++;
            score += (int)(gameManager.stateUp * 10f);
            Score(inGameNumber);
        }
    }

    public void Score(Image[] numchage)
    {
        int tempscore = score;
        int numPostion = 0;
        while(numPostion < inGameNumber.Length)
        {
            int numChoice = tempscore % 10;
            tempscore = (int)(tempscore * 0.1f);
            numchage[numPostion].sprite = number[numChoice];
            if (tempscore <= 0)
                break;

            numPostion++;
        }
    }

    public void GameOverPopUp()
    {
        StopAllCoroutines();
        gameOverPopUp.SetActive(true);
        inGameUi.SetActive(false);
        Score(gameOverNumber);
    }
}
