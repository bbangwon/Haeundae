using UnityEngine;
using System.Collections;

public class IntroPoint : MonoBehaviour {

    public int point;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
}
