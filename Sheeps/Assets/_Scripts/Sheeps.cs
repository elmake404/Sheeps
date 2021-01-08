using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheeps : MonoBehaviour
{
    private Vector3 _direction;

    [SerializeField]
    private float _speedRuning,_speedRotation;
    private bool _isSgepherd;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (_isSgepherd)
        {
            RotationSpheeps(_direction);
            transform.Translate(Vector3.forward * _speedRuning);
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
}
