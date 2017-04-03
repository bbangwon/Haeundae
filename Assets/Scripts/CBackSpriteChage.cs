using UnityEngine;
using System.Collections;

public class CBackSpriteChage : MonoBehaviour {

    public GameObject _moon;
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void NextBackSprite()
    {
        _animator.SetTrigger("Next");
        _moon.SetActive(false);
    }
    public void PrevBackSprite()
    {
        _animator.SetTrigger("Prev");
        _moon.SetActive(true);
    }
	
}
