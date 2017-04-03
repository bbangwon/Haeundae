using UnityEngine;
using System.Collections;


public class CTouchAni : MonoBehaviour {

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

	public void TouchAnim()
    {
        _animator.SetTrigger("Touch");
    }
}
