using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheeps : MonoBehaviour
{
    private Vector3 _direction, _directionHerd;
    [SerializeField]
    private CommunicationOfSheep _communication;

    [SerializeField]
    private float _speedRuning,_speedRotation;
    private bool _isSgepherd, _isCommunicationHerd;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (_isSgepherd)
        {
            RotationSpheeps(_direction);
            transform.Translate(Vector3.forward * _speedRuning);
            _communication.Activation();
        }
        else if (_isCommunicationHerd)
        {
            RotationSpheepsHerd(_directionHerd);
            transform.Translate(Vector3.forward * _speedRuning);
            _communication.Activation();
        }
        else
        {
            _communication.Deactivation();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shepherd")
        {
           _direction = other.transform.position;
            _isSgepherd = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shepherd")
        {
            _direction = transform.position;
            _isSgepherd = false;
        }
    }
    private void RotationSpheeps(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;
        Vector3 direction = (transform.position - PosShepherd).normalized;
        transform.forward = Vector3.Slerp(transform.forward,direction,_speedRotation);
    }
    private void RotationSpheepsHerd(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;
        transform.forward = Vector3.Slerp(transform.forward, PosShepherd, _speedRotation);
    }
    public void Herd(Vector3 Direction)
    {
        _directionHerd = Direction;
        _isCommunicationHerd = true;
    }
    public void OffHerd()
    {
        _directionHerd = transform.forward;
        _isCommunicationHerd = false;
    }
}
