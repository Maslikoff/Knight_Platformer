using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private SliderJoint2D _sliderJoint2D;

    void Start()
    {
        _sliderJoint2D = GetComponent<SliderJoint2D>();
    }

    void Update()
    {

    }
}
