using UnityEngine;
using System.Collections;

public class CMonsterEffect : MonoBehaviour {

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

	public void MonsterEffectStart()
    {
        _animator.SetTrigger("Start");
    }
}
