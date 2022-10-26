using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float Speed = 3f;
    [SerializeField] private float JampForse = 3f;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private KnightAnim _anim;

    private Vector2 _moveVector;
    private bool _isRunning;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<KnightAnim>();
    }

    private void FixedUpdate()
    {
       RunAndAnim();
       MoveJamp();
    }

    private void RunAndAnim()
    {
        _moveVector.x = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(_moveVector.x * Speed, _rigidbody.velocity.y);
        _isRunning = _moveVector.x != 0 ? true : false;

        if (_isRunning)
            _spriteRenderer.flipX = _moveVector.x < 0 ? true : false;

        _anim.isRunning = _isRunning;
    }

    private void MoveJamp()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_rigidbody.velocity.y)<0.05f)
            _rigidbody.AddForce(new Vector2(0, JampForse), ForceMode2D.Impulse);
    }
}
