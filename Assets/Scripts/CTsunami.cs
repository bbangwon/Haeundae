using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CTsunami : MonoBehaviour {

    public GameManager gameManager;

    public GameObject _effect;
    public Transform _effectPos;

    public Transform _use;
    public Transform _pool;

    public int effectPoolNumber;

    List<CEffect> effectPool;

    void Start()
    {
        effectPool = new List<CEffect>();
        for (int i = 0; i < effectPoolNumber; i++)
        {
            GameObject effectGO = Instantiate(_effect, Vector3.zero, Quaternion.identity) as GameObject;
            CEffect temp = effectGO.GetComponent<CEffect>();
            temp.Init(effectPool, _pool);
            effectPool.Add(temp);
        }
    }


    // 건물무너짐 처리
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Building"|| coll.tag == "BuildingItem")
        {
            CBuildingMove buildingMove = 
            coll.GetComponentInParent<CBuildingMove>();
            buildingMove.Out();
            Debug.Log("이펙트 풀 갯수 : " + effectPool.Count);
            if(effectPool.Count > 0)
            {
                effectPool[0].EffectStart();
                effectPool[0].transform.position = _use.position;
                effectPool[0].transform.SetParent(_use);
                Debug.Log("이펙트 생성 좌표 " + effectPool[0].transform.position);
                effectPool.Remove(effectPool[0]);
            }

            if(coll.tag == "BuildingItem")
            {
                Debug.Log("터치 해야됨");
                gameManager._effectBuilding = buildingMove;
                InputManager.isTouch = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "BuildingItem")
        {
            if(InputManager.isTouch)
            {
                Debug.Log("게임 오버");
                gameManager.GameOver();
            }
        }
    }
    
}
