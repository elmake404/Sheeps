using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationOfSheep : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private Sheeps _sheepsMain;
    private Group _group; public Group GroupInstance { get { return _group; } }
    [SerializeField]
    private List<Sheeps> _sheepsList = new List<Sheeps>();
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(enumerator());
        }
    }
    void FixedUpdate()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheeps")
        {
            Sheeps sheeps = other.GetComponentInParent<Sheeps>();
            if (_sheepsMain != sheeps && sheeps != null)
            {
                if (!_sheepsList.Contains(sheeps))
                {
                    _sheepsList.Add(sheeps);
                }

                #region AddGroup
                if (sheeps.Communication.GroupInstance == null && _sheepsMain.Communication.GroupInstance == null)
                {
                    Herd.Instance.NewGroup(_sheepsMain);
                    Herd.Instance.AddGroup(_group.Index, sheeps);
                }
                else if (sheeps.Communication.GroupInstance != null && _sheepsMain.Communication.GroupInstance == null)
                {
                    Herd.Instance.AddGroup(sheeps.Communication.GroupInstance.Index, _sheepsMain);
                }
                else if (sheeps.Communication.GroupInstance == null && _sheepsMain.Communication.GroupInstance != null)
                {
                    Herd.Instance.AddGroup(_group.Index, sheeps);
                }
                else if (sheeps.Communication.GroupInstance != null && _sheepsMain.Communication.GroupInstance != null
                    && sheeps.Communication.GroupInstance != _sheepsMain.Communication.GroupInstance)
                {
                    Herd.Instance.UnificationOfGroups(sheeps.Communication.GroupInstance.Index, _sheepsMain.Communication.GroupInstance.Index);
                }
                #endregion
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sheeps")
        {
            Sheeps sheeps = other.GetComponentInParent<Sheeps>();
            if (sheeps != null)
            {
                if (_sheepsList.Contains(sheeps))
                {
                    _sheepsList.Remove(sheeps);
                    if(_group!=null)
                    Herd.Instance.GroupCheck(_sheepsMain);
                }
            }
        }
    }
    private IEnumerator RebootCollider()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(0.02f);
        _collider.enabled = true;
    }
    public Transform GetNearestSheep()
    {
        Transform Sheep = null;

        if (_group != null)
        {
            bool isDuet = _group.SheepsHerd.Count <= 2;

            float magnitude = float.PositiveInfinity;

            for (int i = 0; i < _group.SheepsHerd.Count; i++)
            {
                if (_group.SheepsHerd[i].IsInHerd && _sheepsMain.CheckForAnException(isDuet, _group.SheepsHerd[i]))
                {
                    float magnitudeSheeps = (_sheepsMain.transform.position - _group.SheepsHerd[i].transform.position).sqrMagnitude;

                    if (magnitude > magnitudeSheeps)
                    {
                        magnitude = (_sheepsMain.transform.position - _group.SheepsHerd[i].transform.position).sqrMagnitude;
                        Sheep = _group.SheepsHerd[i].transform;
                    }
                }
            }
        }

        return Sheep;
    }
    public List<Sheeps> GetShepsNeighbors()
    {
        return _sheepsList;
    }
    public void GroupInitialization(Group group)
    {
        _group = group;
    }
    public void SetDirectionGroup(Quaternion Direction)
    {
        if (_group != null)
        {
            if (_group.Leader == null || _group.Leader == _sheepsMain)
            {
                _group.Leader = _sheepsMain;
                _group.DirectionGroup = Direction;
                _group.IsDirectionSet = true;
            }
        }
    }
    public void TurningOffGroupMovement()
    {
        if (_group != null)
        {
            if (_group.Leader == _sheepsMain)
            {
                _group.DirectionGroup = Quaternion.identity;
                _group.IsDirectionSet = false;
                _group.Leader = null;
            }
        }
    }
    public void LeavinGroup()
    {
        if (_group!=null)
        {
            Herd.Instance.RemoveGroup(_group.Index, _sheepsMain);
            _group = null;
        }
        _sheepsMain.LeavinGroup();
        StartCoroutine(RebootCollider());
    }
}
