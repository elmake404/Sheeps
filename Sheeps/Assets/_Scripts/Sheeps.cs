using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheeps : MonoBehaviour
{
    //[SerializeField]
    //private Herd _herd;
    //[SerializeField]
    private Transform _direcrionSheep;
    private Vector3 _direction, _directionForward, _directionHerd;
    [SerializeField]
    private CommunicationOfSheep _communication; public CommunicationOfSheep Communication { get { return _communication; } }

    [SerializeField]
    private float _speedRuning, _speedRotation, _minDistens;
    private bool _isSgepherd, _isSgepherdForwar/*, _isCommunicationHerd*/;
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
        else if (_isSgepherdForwar)
        {
            RotationForTarget(_directionForward);
            transform.Translate(Vector3.forward * _speedRuning);
        }
        else
        {

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shepherd")
        {
            _direction = other.transform.position;
            _isSgepherd = true;
        }
        if (other.tag == "ShepherdForward")
        {
            _isSgepherdForwar = true;
            _directionForward = other.transform.parent.forward;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shepherd")
        {
            _direction = transform.position;
            _isSgepherd = false;
        }

        if (other.tag == "ShepherdForward")
        {
            _directionForward = transform.forward;

            _isSgepherdForwar = false;
        }
    }
    private void RotationOffTarget(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;
        Vector3 direction = (transform.position - PosShepherd).normalized;
        transform.forward = Vector3.Slerp(transform.forward, direction, _speedRotation);
    }
    private void RotationForTarget(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;
        transform.forward = Vector3.Slerp(transform.forward, PosShepherd, _speedRotation);
    }
    private void RotationToTheTarget(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;

        Quaternion Rotation = Quaternion.LookRotation(PosShepherd - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, _speedRotation);
    }
}
