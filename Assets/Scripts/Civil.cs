using UnityEngine;
using System.Collections;

public class Civil : MonoBehaviour {
    public float speed = 2.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x > 4.5f)
            Destroy(gameObject);
	}
}
