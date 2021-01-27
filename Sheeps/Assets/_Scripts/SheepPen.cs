using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepPen : MonoBehaviour
{
    private Rigidbody _rbMain;
    private float _speed;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _rbMain.velocity = Vector3.Slerp(_rbMain.velocity, Vector3.zero, 0.01f);
        _rbMain.angularVelocity = Vector3.zero;
        transform.Translate(Vector3.forward * _speed);
        _speed = Mathf.Lerp(_speed, 0, 0.03f);
    }
    public void Initialization(Rigidbody rigidbody,float Speed)
    {
        _rbMain = rigidbody;
        _speed = Speed;
    }

}
