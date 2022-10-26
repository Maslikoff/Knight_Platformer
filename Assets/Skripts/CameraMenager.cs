using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenager : MonoBehaviour
{
    [SerializeField] private Transform Knight;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - Knight.position;
    }

    private void FixedUpdate()
    {
        transform.position = _offset + Knight.position;
    }
}
