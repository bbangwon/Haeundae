using UnityEngine;
using System.Collections;

public class CMountain : MonoBehaviour {

    Animator _animator;

    public SpriteRenderer mountainSpriteRenedererFirst;
    public SpriteRenderer[] mountainSpriteRenederer;
    public SpriteRenderer mountainSpriteRenedererLast;

    public Sprite[] nextMoutain; 



    int Next = 0;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MountainMoveStart()
    {
        Next++;
        if(Next == 2)
        {
            mountainSpriteRenedererFirst.sprite = nextMoutain[0];
            for (int i = 0; i < mountainSpriteRenederer.Length; i++)
            {
                mountainSpriteRenederer[i].sprite = nextMoutain[1];
            }
            mountainSpriteRenedererLast.sprite = nextMoutain[2];
        }
        _animator.SetTrigger("MountainStart");
    }

    public void MountainMoveNext()
    {
        _animator.SetTrigger("MountainEnd");
    }
    public void MountainMoveStop()
    {
        _animator.speed = 0;
    }
    
}
