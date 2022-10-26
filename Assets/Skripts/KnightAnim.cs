using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnim : MonoBehaviour
{
    private Animator _animator;

    public bool isRunning { private get; set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("isRunning", isRunning);
    }
}
