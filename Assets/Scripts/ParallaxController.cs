using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] layers;
    [SerializeField] private float[] coff;

    private int _layersCount;

    private void Start()
    {
        _layersCount = layers.Length;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _layersCount; i++)
        {
            layers[i].position = transform.position * coff[i];
        }
    }
}
