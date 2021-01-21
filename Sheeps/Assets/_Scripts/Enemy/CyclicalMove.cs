﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclicalMove : MonoBehaviour
{
    [SerializeField]
    private Transform _targer;
    private Vector3 _point1, _point2;

    [SerializeField]
    private float _speed, _speedRotation;

    private void Start()
    {
        _point1 = transform.position;
        _point2 = _targer.position;
    }

    private void FixedUpdate()
    {
        if(RotationObj())
        transform.position = Vector3.MoveTowards(transform.position, NextPos(), _speed);
        
    }
    private Vector3 NextPos()
    {
        if ((_targer.position - transform.position).magnitude <= 0.1f)
        {
            _targer.position = GetNevTargetPos();
        }

        return _targer.position;
    }
    private Vector3 GetNevTargetPos()
    {
        if ((_point1 - transform.position).magnitude > (_point2 - transform.position).magnitude)
        {
            return _point1;
        }
        else
        {
            return _point2;
        }
    }
    private bool RotationObj()
    {
        Quaternion Rotation = Quaternion.LookRotation(NextPos() - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, _speedRotation);

        return Rotation == transform.rotation;
    }
}
