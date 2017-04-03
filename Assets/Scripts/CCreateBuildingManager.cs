using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class CCreateBuildingManager : MonoBehaviour {

    // 건물 오브젝트 뭉치
    public GameObject[] _buildings;
    // 아이템 건물 오브젝트
    public GameObject _itemBuilding;
   // 생성 좌표
    public Transform _createPos;
    public Transform _createBuildingItemPos;
    // 아이템 생성 타임
    public float _itemCreateTiem;

    public Transform _use;
    public Transform _pool;
    public Transform _back;

    public static Dictionary<string, List<CBuildingMove>> _objectPool;

    public int _poolNumber;

    public static float buildingSpeed;

    public float buildingSpeedSeting = 4;

    public Text textSpeed;

    // Use this for initialization
    void Start()
    {
        buildingSpeed = buildingSpeedSeting;
        textSpeed.text = buildingSpeed.ToString();
        _objectPool = new Dictionary<string, List<CBuildingMove>>();
        // 오브젝트 생성
        for (int i = 0; i < _buildings.Length; i++)
        {
            CreateOjbectPool(_buildings[i], _buildings[i].name, _poolNumber, false);
        }
        CreateOjbectPool(_itemBuilding, _itemBuilding.name, _poolNumber/2, true);

        StartCoroutine(CreateBuidling());   // 건물 생성 코루틴
        StartCoroutine(CreateItemCreate()); // 아이템 건물 생성 코루틴
    }
    // 오브젝트 풀 생성
    public void CreateOjbectPool(GameObject objectP, string objectName, int num, bool isItem)
    {
        if(!_objectPool.ContainsKey(objectName))
        {
            _objectPool.Add(objectName, new List<CBuildingMove>());
        }

        for (int i = 0; i < num; i++)
        {
            GameObject gameobjectP = Instantiate(objectP, Vector2.zero, Quaternion.identity) 
                as GameObject;
            gameobjectP.name = objectName;
            CBuildingMove buildingMove = gameobjectP.GetComponent<CBuildingMove>();
            if(isItem)
            {
                buildingMove.InitItem(_pool);
            }
            else
            {
                buildingMove.Init(_pool);
            }

            _objectPool[objectName].Add(buildingMove);
        }
    }

    
    // 아이템 건물 생성 코루틴
    IEnumerator CreateItemCreate()
    {
        while(true)
        {
            yield return new WaitForSeconds(_itemCreateTiem);

            if(_objectPool[_itemBuilding.name].Count < 1)
            {
                Debug.Log("아이템없음");
            }
            else
            {
                Debug.Log("아이템출현");
                CBuildingMove buildingMovePool = _objectPool[_itemBuilding.name][0];
                _objectPool[_itemBuilding.name].Remove(buildingMovePool);
                buildingMovePool.MoveStart(_createBuildingItemPos, _use);

            }


        }
    }
    // 건물 생성 코루틴
    IEnumerator CreateBuidling()
    {
        while(true)
        {
            //buildingSpeed = _buildingSpeed;
            yield return new WaitForSeconds(1f / buildingSpeed);
            //Debug.Log("건물 생성 속도" + 1f / _buildingSpeed);
            int buildingNum = Random.Range(0, _buildings.Length);
            if (_objectPool[_buildings[buildingNum].name].Count < 1)
            {
                Debug.Log("건물 없음");
                while(true)
                {
                    buildingNum++;
                    if(buildingNum >= _buildings.Length)
                    {
                        buildingNum = 0;
                    }
                    if(_objectPool[_buildings[buildingNum].name].Count > 0)
                    {
                        break;
                    }
                }
            }
            else
            {
                CBuildingMove buildingMovePool = _objectPool[_buildings[buildingNum].name][0];
                _objectPool[_buildings[buildingNum].name].Remove(buildingMovePool);
                buildingMovePool.MoveStart(_createBuildingItemPos, _back);

            }

         
        }
    }
    
    public void GameOver()
    {
        Debug.Log("멈춰");
        StopAllCoroutines();
        buildingSpeed = 0.0f;
    }

    public void SpeedChage(float upSpeed)
    {

        buildingSpeedSeting += upSpeed;
        buildingSpeed = buildingSpeedSeting;
        textSpeed.text = buildingSpeed.ToString();
    }

}
