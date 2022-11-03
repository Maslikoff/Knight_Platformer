using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("�����")]
    [SerializeField] private Transform Player;

    /// <summary>
    /// ���������� �� ������
    /// </summary>
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
