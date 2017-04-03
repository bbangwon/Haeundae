using UnityEngine;
using System.Collections;

public class ScrollManager : MonoBehaviour {

    public enum Direction
    {
        LEFT,
        RIGHT
    }

    public enum Pos
    {
        Front,
        Back
    }

    public float ChangePoint;
    public Transform[] scrollObjectsNoLoop;
    public Transform[] scrollObjects;
    public Transform lastScrollObject;
    public float scrollSpeed;
    public Direction scrollDirection;
    public Pos _pos;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(_pos == Pos.Front)
        {
            scrollSpeed = CCreateBuildingManager.buildingSpeed + CCreateBuildingManager.buildingSpeed * 0.2f;
        }
        else
        {
            scrollSpeed = CCreateBuildingManager.buildingSpeed * 0.5f;
        }
        foreach(Transform transfn in scrollObjectsNoLoop)
        {
            if (scrollDirection == Direction.RIGHT)
            {
                if(transfn.gameObject.activeSelf == true)
                {
                    if (transfn.position.x >= ChangePoint)
                    {
                        transfn.gameObject.SetActive(false);
                    }
                    else
                        transfn.transform.Translate(Vector2.right * scrollSpeed * Time.deltaTime);
                }

            }
            else if (scrollDirection == Direction.LEFT)
            {
                if (transfn.gameObject.activeSelf == true)
                {
                    if (transfn.position.x <= ChangePoint)
                    {
                        transfn.gameObject.SetActive(false);
                    }
                    else
                        transfn.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
                }
            }
        }
        foreach (Transform transf in scrollObjects)
        {
            if(scrollDirection == Direction.RIGHT)
            {
                if (transf.position.x >= ChangePoint)
                {
                    transf.position = lastScrollObject.FindChild("ConnectPoint").transform.position;
                    lastScrollObject = transf;
                    transf.GetComponent<CSpriteChage>().SpriteChage();
                }

                transf.transform.Translate(Vector2.right * scrollSpeed * Time.deltaTime);
            }
            else if(scrollDirection == Direction.LEFT)
            {
                if (transf.position.x <= ChangePoint)
                {
                    transf.position = lastScrollObject.FindChild("ConnectPoint").transform.position;
                    lastScrollObject = transf;
                }

                transf.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
            }

        }
	}

   
}
