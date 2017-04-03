using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CEffect : MonoBehaviour {

    Animator _animator;
    List<CEffect> _effectPool;
    Transform _pool;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
	
    public void Init(List<CEffect> effectPool, Transform pool)
    {
        _effectPool = effectPool;
        _pool = pool;
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void EffectStart()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger("Start");
    }

    public void Push()
    {
        transform.SetParent(_pool);
        _effectPool.Add(this);
        gameObject.SetActive(false);
    }

}
