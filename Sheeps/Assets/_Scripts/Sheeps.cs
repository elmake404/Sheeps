using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheeps : MonoBehaviour
{
    //[SerializeField]
    //private Herd _herd;
    //[SerializeField]
    private Transform _direcrionSheep;
    private Vector3 _direction/* _directionForward,*/ ;
    private  Quaternion _directionHerd;
    [SerializeField] 
    private CommunicationOfSheep _communication; public CommunicationOfSheep Communication { get { return _communication; } }

    [SerializeField]
    private float _speedRuning, _speedRotation, _minDistens;
    private bool _isSgepherd, /*_isSgepherdForwar,*/ _isCommunicationHerd;
    public float MinDistend { get { return _minDistens; } }
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (_isSgepherd)
        {
            RotationOffTarget(_direction);
            transform.Translate(Vector3.forward * _speedRuning);
        }
        else if (_isCommunicationHerd)
        {
            RotationForTarget(_directionHerd);
            transform.Translate(Vector3.forward * _speedRuning);
        }
        else
        {
            if (_direcrionSheep==null)
            {
                _direcrionSheep = _communication.GetNearestSheep();
            }
            else if ((_direcrionSheep.position -transform.position).sqrMagnitude>_minDistens)
            {
                _direcrionSheep = _communication.GetNearestSheep();
                RotationToTheTarget(_direcrionSheep.position);
                transform.Translate(Vector3.forward * _speedRuning);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shepherd")
        {
            _direction = other.transform.position;
            _communication.SetDirectionGroup(transform.rotation);
            _isSgepherd = true;
        }
        //if (other.tag == "ShepherdForward")
        //{
        //    _isSgepherdForwar = true;
        //    _directionForward = other.transform.parent.forward;
        //}

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shepherd")
        {
            _direction = transform.position;
            _communication.TurningOffGroupMovement();
            _isSgepherd = false;
        }

        //if (other.tag == "ShepherdForward")
        //{
        //    _directionForward = transform.forward;

        //    _isSgepherdForwar = false;
        //}
    }
    private void RotationOffTarget(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;
        Vector3 direction = (transform.position - PosShepherd).normalized;
        transform.forward = Vector3.Slerp(transform.forward, direction, _speedRotation);
    }
    private void RotationForTarget(Quaternion rotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,_speedRotation);
    }
    private void RotationToTheTarget(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;

        Quaternion Rotation = Quaternion.LookRotation(PosShepherd - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, _speedRotation);
    }
    public void SetDirectionHers(Quaternion direction)
    {
        _isCommunicationHerd = true;
        _directionHerd = direction;
    }
    public void TurningOffHerdMovement()
    {
        _isCommunicationHerd = false;
        _directionHerd = transform.rotation;
    }
}
