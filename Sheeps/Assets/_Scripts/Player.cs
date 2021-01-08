using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _shepherd;
    private Camera _cam;
    private Vector3 _oldPosShepherd, _startPosMouse;

    private void Awake()
    {
        _cam = Camera.main;
    }
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            _oldPosShepherd = _shepherd.position;
            _startPosMouse = (_cam.transform.position - ((ray.direction) *
        ((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y)));
        }
        else if (Input.GetMouseButton(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            _shepherd.position = _oldPosShepherd + (_cam.transform.position - ((ray.direction) *
        ((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y))) - _startPosMouse;
        }

    }
    //private void FixedUpdate()
    //{
    //    _shepherd.position = _startPosMouse;
    //}
}
