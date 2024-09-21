using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DashStruct
{
    public float _activationTime;
    public float _dashSpeed;
    public DashStruct(float activationTime, float dashSpeed)
    {
        _activationTime = activationTime;
        _dashSpeed = dashSpeed;
    }
}
