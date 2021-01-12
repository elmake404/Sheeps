using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _shepherd;
    private Camera _cam;
    private Vector3 _oldPosShepherd, _startPosMouse, _currentMosePos;

    [SerializeField]
    private float _speedMowe, _speedRot,_speedRotMin;

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

            _shepherd.position = _startPosMouse;
        }
        else if (Input.GetMouseButton(0))
        {
            //vector = _shepherd.position;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            //   Vector3 NewPos = _oldPosShepherd + (_cam.transform.position - ((ray.direction) *
            //((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y))) - _startPosMouse;

            _currentMosePos = (_cam.transform.position - ((ray.direction) *
((_cam.transform.position.y - _shepherd.position.y) / ray.direction.y)));

            //_shepherd.position = NewPos;
            //if ((_shepherd.position - vector) != Vector3.zero)
            //    _shepherd.forward = (_shepherd.position - vector).normalized;
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
        if ((_shepherd.position - _currentMosePos) != Vector3.zero)
            RotationPlayer(_currentMosePos);

           // _shepherd.forward = (_currentMosePos - _shepherd.position).normalized;

        _shepherd.position = Vector3.MoveTowards(_shepherd.position, _currentMosePos,_speedMowe);
    }
    private void RotationPlayer(Vector3 target)
    {
        //Debug.Log((target - _shepherd.position).magnitude);
        Quaternion Rotation = Quaternion.LookRotation(target - _shepherd.position);
        Debug.Log(Mathf.Abs((Rotation.eulerAngles - _shepherd.rotation.eulerAngles).y));

        //if (Mathf.Abs((Rotation.eulerAngles - _shepherd.rotation.eulerAngles).y)>20)
        //{
            _shepherd.rotation = Quaternion.Slerp(_shepherd.rotation, Rotation, _speedRot);
        //}
        //else
        //{
        //    _shepherd.rotation = Quaternion.Slerp(_shepherd.rotation, Rotation, _speedRotMin);
        //}
    }
}
