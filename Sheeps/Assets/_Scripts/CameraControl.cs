using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;

    [SerializeField]
    private Transform _anchorPoint,_finishPos;
    private Vector3 _velocity = Vector3.zero, _target;
    [SerializeField]
    private Vector3 _offSet = new Vector3(0, 0, 10);

    [SerializeField]
    private float _smoothTime = 0.3F, _offSetX = 1f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _target = transform.position -_offSet;
        _target.x = _anchorPoint.position.x;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target + _offSet, ref _velocity, _smoothTime);
    }
    public void SetTargetGroup(Group group)
    {
        float magnitude = float.PositiveInfinity;
        float ZPos = (transform.position - _offSet).z;
        float XPos = (transform.position - _offSet).x;

        for (int i = 0; i < group.SheepsHerd.Count; i++)
        {
            if (group.SheepsHerd[i].IsInHerd)
            {
                float magnitudeSheeps = group.SheepsHerd[i].DistanceFinish;

                if (magnitude > magnitudeSheeps)
                {
                    magnitude = (_finishPos.position - group.SheepsHerd[i].transform.position).sqrMagnitude;
                    ZPos = group.SheepsHerd[i].transform.position.z;
                    XPos = group.SheepsHerd[i].transform.position.x;
                }
            }
        }
        Debug.Log(XPos - _anchorPoint.position.x);
        if (Mathf.Abs(XPos - _anchorPoint.position.x) > _offSetX)
        {
            float factor = XPos - _anchorPoint.position.x > 0 ? 1 : -1;
            XPos = _anchorPoint.position.x +(_offSetX * factor);
        }
        _target.z = ZPos;
        _target.x = XPos;
    }
    public void SetTarget(Vector3 target)
    {
        float XPos = target.x;

        if (Mathf.Abs(XPos - _anchorPoint.position.x) > _offSetX)
        {
            float factor = XPos - _anchorPoint.position.x > 0 ? 1 : -1;
            XPos = _anchorPoint.position.x + (_offSetX * factor);
        }
        _target.z = target.x;
        _target.x = XPos;
    }
    public Vector3 GetPosFinish()
    {
        return _finishPos.position;
    }
}
