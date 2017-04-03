using UnityEngine;
using System.Collections;

public class CCat : MonoBehaviour {

    Animator _animator;
    Rigidbody2D _rigidbody2D;
    public BoxCollider2D _EndColl;
    public AudioClip[] jumpAudio;

    public CMonsterEffect monsterEffect;

    bool bJump = true;
	
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (bJump == false)
            return;

        bJump = false;
        _animator.SetBool("Jump", true);
        _rigidbody2D.AddForce(Vector2.up * 1000f);
        // 효과음
        AudioSource.PlayClipAtPoint(jumpAudio[Random.Range(0,jumpAudio.Length)], Vector3.zero);
        monsterEffect.MonsterEffectStart();
    }

    public void JumpEnd()
    {
        _animator.SetBool("Jump", false);
    }

    public void Die()
    {
        _EndColl.isTrigger = true;
        _animator.SetTrigger("Die");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        JumpEnd();
        bJump = true;
    }
}
