using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheeps : MonoBehaviour
{
    [SerializeField]
    private CommunicationOfSheep _communication; public CommunicationOfSheep Communication
    { get { return _communication; } }

    [SerializeField]
    private Transform _direcrionSheep; public Transform DirecrionSheep
    { get { return _direcrionSheep; } }
    [SerializeField]
    private SkinnedMeshRenderer[] _mesh;
    [SerializeField]
    private Material _activeMaterial, _decontaminationMaterial;
    [SerializeField]
    private SheepPen _sheepPen;

    [SerializeField]
    private Rigidbody _rbMain;
    private Vector3 _direction, _directionJamp;

    [SerializeField]
    private float _speedRuning, _speedRotation, _minDistens, _brakingSpeed, _jumpForse;
    private float _speedMove;
    private bool _isShepherd, _isJump, _isFly ;

    private bool _isDirectionSet
    { get { return _communication.GroupInstance != null ? _communication.GroupInstance.IsDirectionSet : false; } }

    public float DistanceFinish
    { get; private set; }
    //выглфдит не очень 
    [HideInInspector]
    public bool IsInHerd;
    public bool IsActivation;

    public float MinDistend
    { get { return _minDistens; } }
    private void Awake()
    {
        _sheepPen.Initialization(_rbMain,_speedRuning);
        _sheepPen.enabled = false;
    }
    void Start()
    {
        _isFly = true;
        if (!IsActivation)
        {
            for (int i = 0; i < _mesh.Length; i++)
            {
                _mesh[i].material = _decontaminationMaterial;
            }
        }

    }

    void FixedUpdate()
    {
        ActiveSheep();
        Polishing();
        DistanceFinish = (transform.position - CameraControl.Instance.GetPosFinish()).sqrMagnitude;

        if (!Player.IsMove && _isShepherd)
        {
            _direction = transform.position;
            _communication.TurningOffGroupMovement();
            _isShepherd = false;
        }

        if (!_isJump)
        {
            if (_isShepherd && IsActivation)
            {
                #region Camera
                if (_communication.GroupInstance != null)
                    CameraControl.Instance.SetTargetGroup(_communication.GroupInstance);
                else
                    CameraControl.Instance.SetTarget(transform.position);
                #endregion

                _communication.SetDirectionGroup(transform.rotation);

                RotationOffTarget(_direction);
                _speedMove = _speedRuning;
            }
            else if (_communication.GroupInstance != null)
            {
                if (_isDirectionSet && IsInHerd)
                {
                    RotationForTarget(_communication.GroupInstance.DirectionGroup);
                    _speedMove = _speedRuning;
                }
                else
                {
                    if (_direcrionSheep == null)
                    {
                        _direcrionSheep = _communication.GetNearestSheep();
                    }
                    else
                    {
                        float sqrMagnitude = (_direcrionSheep.position - transform.position).sqrMagnitude;

                        if ((sqrMagnitude > _minDistens) && this != Herd.Instance.FindingTheLeading(_communication.GroupInstance))
                        {
                            RotationToTheTarget(_direcrionSheep.position);
                            transform.Translate(Vector3.forward * (_speedRuning - _speedMove));

                            _direcrionSheep = _communication.GetNearestSheep();
                        }
                        else
                        {
                            _rbMain.velocity = Vector3.zero;
                            _rbMain.angularVelocity = Vector3.zero;
                            IsInHerd = true;
                        }
                    }
                }
            }
            else
            {
                _rbMain.velocity = Vector3.zero;
                _rbMain.angularVelocity = Vector3.zero;
            }
            Movement();
        }
        else
        {
            transform.forward = Vector3.MoveTowards(transform.forward, _directionJamp, 0.5f);
            transform.Translate(Vector3.forward * _speedRuning);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            _communication.LeavinGroup();
            Destroy(gameObject);
            CanvasManager.IsLoseGame = Herd.Instance.LoseCheck();
        }
        if (other.tag == "Finish")
        {
            SheepInThePen();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shepherd")
        {
            _direction = other.transform.position;
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
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Earth")
        {
            if (_isJump && _isFly)
            {
                _directionJamp = transform.forward;
                _isJump = false;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Earth")
        {
            _isJump = true;
        }

    }
    private void SheepInThePen()
    {
        for (int i = 0; i < _mesh.Length; i++)
        {
            _mesh[i].material = _activeMaterial;
        }
        _communication.LeavinGroupFinish();
        _sheepPen.enabled = true;
        enabled = false;
    }
    private void Movement()
    {
        if (!_isShepherd && !_isDirectionSet)
        {
            _speedMove = Mathf.Lerp(_speedMove, 0, _brakingSpeed);
        }

        transform.Translate(Vector3.forward * _speedMove);

    }
    private void ActiveSheep()
    {
        if (IsActivation && _mesh[0].material != _activeMaterial)
        {
            for (int i = 0; i < _mesh.Length; i++)
            {
                _mesh[i].material = _activeMaterial;
            }
        }
        if (!IsActivation && _mesh[0].material != _decontaminationMaterial)
        {
            for (int i = 0; i < _mesh.Length; i++)
            {
                _mesh[i].material = _decontaminationMaterial;
            }
        }
    }
    private void RotationOffTarget(Vector3 PosShepherd)
    {
        PosShepherd.y = transform.position.y;
        Vector3 direction = (transform.position - PosShepherd).normalized;
        transform.forward = Vector3.Slerp(transform.forward, direction, _speedRotation);
    }
    private void Polishing()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
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
    private IEnumerator Delay()
    {
        _isFly = false;
        yield return new WaitForSeconds(0.1f);
        _isFly = true;
    }
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
        _isShepherd = false;
        IsInHerd = false;
    }
    public void JumpSheeps(float jampForce, Vector3 rotation)
    {
        if (!_isJump)
        {
            _directionJamp = rotation;
            _rbMain.AddForce(Vector3.up * jampForce);
            _isJump = true;
            StartCoroutine(Delay());
        }
    }
}
