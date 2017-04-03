using UnityEngine;
using System.Collections;

public class CBuildingMove : MonoBehaviour {

    Animator _animator;     // 애니메이터
    bool _down = false;     // 아래로 이동여부
    bool _isItem = false;   // 아이템 건물여부

    Transform _pool;
    Transform _createPos;
    Transform _effectPos;

    public Sprite _nextSprite;
    public SpriteRenderer _spriteRenderer;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
        Move();
    }

    public void Init(Transform pool)
    {
        _pool = pool;
        transform.SetParent(pool);

        transform.position = Vector2.zero;
        gameObject.SetActive(false);
        //_speed = speed;
    }
    public void InitItem(Transform pool)
    {
        _pool = pool;
        transform.SetParent(pool);
        _isItem = true;

        transform.position = Vector2.zero;
        gameObject.SetActive(false);
        //_speed = speed;
    }
    // 처음 출현 처리
    public void MoveStart(Transform createPos, Transform use)
    {

        if(GameManager.NEXTSTATE && !_isItem)
        {
            _spriteRenderer.sprite = _nextSprite;
        }
        _createPos = createPos;
        transform.SetParent(use);
        transform.position = createPos.position;
        if(!_isItem)
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    public void Move()
    {
        if (GameManager.GAMEOVER) return;
        // 오른쪽 이동
        transform.Translate(Vector3.left * -CCreateBuildingManager.buildingSpeed * Time.deltaTime);
        // 밑으로 이동
        if(_down)
        {
            transform.Translate(Vector3.down* 2 * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, _createPos.position.y, transform.position.z);
        }
        // 애니메이션 원위치
        if(transform.position.x > 10)
        {
            Turn();
        }
        // Pool 넣기
        if (transform.position.x > 15)
        {
            Push();
        }

    }
    
    public void Out()
    {
        //Debug.Log("무너짐");
        if(!_isItem)
        {
            _animator.SetTrigger("Out");
            _down = true;

        }
    }
    // 애니메이션 원위치
    public void Turn()
    {
        _down = false;
        _animator.SetTrigger("Turn");
        transform.position = new Vector3(transform.position.x, -2.2f , 0);
        transform.SetParent(_pool);
        transform.localScale = Vector3.one;
    }
    // pool 넣기
    public void Push()
    {
        transform.position = Vector3.zero;
        CCreateBuildingManager._objectPool[gameObject.name].Add(this);
        gameObject.SetActive(false);
    }

    public void NextSprite()
    {
        _spriteRenderer.sprite = _nextSprite;
    }

    // 이펙트 효과
    public void ShowEffect()
    {
        //if(_effectAni != null)
        //_effectAni.SetTrigger("Show");
    }

}
