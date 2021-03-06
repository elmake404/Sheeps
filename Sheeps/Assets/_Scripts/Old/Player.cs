﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool IsMove;

    [SerializeField]
    private Transform _shepherd;
    private Camera _cam;
    private Vector3 _posShepherd, _startPosMouse, _currentMosePos;

    [SerializeField]
    private float _speedMowe, _speedRot, _speedRotMin;
    //[SerializeField]

    private void Awake()
    {
        _cam = Camera.main;
    }
    void Start()
    {
        IsMove = false;
        _shepherd.gameObject.SetActive(false);
    }

    void Update()
    {
        if (CanvasManager.IsStartGeme && CanvasManager.IsGameFlow)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                _startPosMouse = (_cam.transform.position - ((ray.direction) *
             ((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y)));

                _shepherd.position = _startPosMouse;
                _shepherd.gameObject.SetActive(true);

            }
            else if (Input.GetMouseButton(0))
            {
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

                _currentMosePos = (_cam.transform.position - ((ray.direction) *
                ((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y)));

                IsMove = true;
                _posShepherd = _currentMosePos;
            }
            else
            {
                _shepherd.gameObject.SetActive(false);

                IsMove = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (IsMove)
        {
            if ((_shepherd.position - _currentMosePos) != Vector3.zero)
                RotationPlayer(_posShepherd);

            _shepherd.position = Vector3.MoveTowards(_shepherd.position, _posShepherd, _speedMowe);
        }
    }
    private void RotationPlayer(Vector3 target)
    {
        Quaternion Rotation = Quaternion.LookRotation(target - _shepherd.position);
        _shepherd.rotation = Quaternion.Slerp(_shepherd.rotation, Rotation, _speedRot);
    }
}
