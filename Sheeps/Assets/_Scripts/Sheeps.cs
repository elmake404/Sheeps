using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheeps : MonoBehaviour
{

    [SerializeField]
    private CommunicationOfSheep _communication; public CommunicationOfSheep Communication { get { return _communication; } }

    [SerializeField]
    private Transform _direcrionSheep; public Transform DirecrionSheep { get { return _direcrionSheep; } }
    [SerializeField]
    private Rigidbody _rbMain;
    private Vector3 _direction;
    //private Quaternion _directionHerd;

    [SerializeField]
    private float _speedRuning, _speedRotation, _minDistens;
    [SerializeField]
    private bool _isShepherd/*, _isCo1mmunicationHerd*/;

    //выглфдит не очень 
    [HideInInspector]
    public bool IsInHerd;
    public float MinDistend { get { return _minDistens; } }
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (!Player.IsMove && _isShepherd)
        {
            _direction = transform.position;
            _communication.TurningOffGroupMovement();
            _isShepherd = false;
        }

        if (_isShepherd)
        {
            RotationOffTarget(_direction);
            transform.Translate(Vector3.forward * _speedRuning);
        }
        else if (_communication.GroupInstance != null)
        {
            if (_communication.GroupInstance.IsDirectionSet && IsInHerd)
            {
                RotationForTarget(_communication.GroupInstance.DirectionGroup);
                transform.Translate(Vector3.forward * _speedRuning);
            }
            else
            {
                if (_direcrionSheep == null)
                {
                    _direcrionSheep = _communication.GetNearestSheep();
                }
                else
                {
                    if ((_direcrionSheep.position - transform.position).sqrMagnitude > _minDistens)
                    {
                        RotationToTheTarget(_direcrionSheep.position);
                        transform.Translate(Vector3.forward * _speedRuning);
                        _direcrionSheep = _communication.GetNearestSheep();
                    }
                    else
                    {
                        IsInHerd = true;
                    }
                }
                _rbMain.velocity = Vector3.zero;
                _rbMain.angularVelocity = Vector3.zero;
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shepherd")
        {
            _direction = other.transform.position;
            _communication.SetDirectionGroup(transform.rotation);
            if (_communication.GroupInstance != null)
                CameraControl.Instance.SetTarget(_communication.GroupInstance);
            _isShepherd = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shepherd")
        {
            _direction = transform.position;
            _communication.TurningOffGroupMovement();
            _isShepherd = false;
        }
    }
    private void RotationOffTarget(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;
        Vector3 direction = (transform.position - PosShepherd).normalized;
        transform.forward = Vector3.Slerp(transform.forward, direction, _speedRotation);
    }
    private void RotationForTarget(Quaternion rotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speedRotation);
    }
    private void RotationToTheTarget(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;

        Quaternion Rotation = Quaternion.LookRotation(PosShepherd - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, _speedRotation);
    }
    //public void SetDirectionHers(Quaternion direction)
    //{
    //    _isCommunicationHerd = true;
    //    _directionHerd = direction;
    //}
    //public void TurningOffHerdMovement()
    //{
    //    _isCommunicationHerd = false;
    //    _directionHerd = transform.rotation;
    //}
    public void MovingToAnotherGroup(Group NewGrpop)
    {
        Communication.GroupInitialization(NewGrpop);
        IsInHerd = false;
        _direcrionSheep = null;
    }
    public bool CheckForAnException(bool isDuet, Sheeps Sheep)
    {
        if (isDuet)
        {
            if (Sheep != this)
                return true;
            else
                return false;
        }
        else
        {
            if (Sheep != this && Sheep.DirecrionSheep != transform)
                return true;
            else
                return false;
        }
    }
    public void LeavinGroup()
    {
        _direcrionSheep = null;
        //_directionHerd = transform.rotation;
        //_isCommunicationHerd = false;
        _isShepherd = false;
        IsInHerd = false;
    }
}
