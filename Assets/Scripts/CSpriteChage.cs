using UnityEngine;
using System.Collections;

public class CSpriteChage : MonoBehaviour {

    public SpriteRenderer _spriteRenderer;
    public Sprite _nextStateCity;

    public void SpriteChage()
    {
        if(GameManager.NEXTSTATE)
        _spriteRenderer.sprite = _nextStateCity;
    }
}
