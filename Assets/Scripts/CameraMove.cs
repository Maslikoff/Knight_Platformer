using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform Player;

    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - Player.position;
    }

    void Update()
    {
        transform.position = _offset + Player.position;
    }
}
