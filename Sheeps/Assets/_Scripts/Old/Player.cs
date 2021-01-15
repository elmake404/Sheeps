using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _shepherd;
    private Camera _cam;
    private Vector3 _posShepherd, _startPosMouse, _currentMosePos;

    [SerializeField]
    private float _speedMowe, _speedRot, _speedRotMin;
    //[SerializeField]
    private bool _IsMove = false;

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
            _startPosMouse = (_cam.transform.position - ((ray.direction) *
         ((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y)));

            _shepherd.position = _startPosMouse;
        }
        else if (Input.GetMouseButton(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            _currentMosePos = (_cam.transform.position - ((ray.direction) *
            ((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y)));

            //if ((_currentMosePos - _shepherd.position).magnitude >= 0.5f)
            //{
                _IsMove = true;
                _posShepherd = _currentMosePos;
            //}
            //else
            //{
            //    _IsMove = false;
            //}
        }
        else
        {
            _IsMove = false;
        }

        #region Old
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        //    _oldPosShepherd = _shepherd.position;
        //    _startPosMouse = (_cam.transform.position - ((ray.direction) *
        //((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y)));
        //}
        //else if (Input.GetMouseButton(0))
        //{
        //    vector = _shepherd.position;
        //    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        //    Vector3 NewPos = _oldPosShepherd + (_cam.transform.position - ((ray.direction) *
        // ((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y))) - _startPosMouse;

        //    _shepherd.position = NewPos;
        //    if((_shepherd.position - vector)!=Vector3.zero)
        //    _shepherd.forward=(_shepherd.position - vector).normalized;
        //}
        #endregion
    }
    private void FixedUpdate()
    {
        if (_IsMove)
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
