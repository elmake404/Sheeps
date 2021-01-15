using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationOfSheep : MonoBehaviour
{
    [SerializeField]
    private Sheeps _sheepsMain;
    private Group _group; public Group GroupInstance { get { return _group; } }
    //
    //private List<Sheeps> _sheepsList = new List<Sheeps>();
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (_group!=null)
        {
            if (_group.IsDirectionSet)
            {
                _sheepsMain.SetDirectionHers(_group.DirectionGroup);
            }
            else
            {
                _sheepsMain.TurningOffHerdMovement();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheeps")
        {
            Sheeps sheeps = other.GetComponentInParent<Sheeps>();
            if (_sheepsMain != sheeps)
            {
                if (sheeps.Communication.GroupInstance == null && _sheepsMain.Communication.GroupInstance == null)
                {
                    Herd.Instance.NewGroup(_sheepsMain);
                    Herd.Instance.AddGroup(_group.Index, sheeps);
                }
                else if (sheeps.Communication.GroupInstance != null && _sheepsMain.Communication.GroupInstance == null)
                {
                    Herd.Instance.AddGroup(sheeps.Communication.GroupInstance.Index, sheeps);
                }
                else if (sheeps.Communication.GroupInstance == null && _sheepsMain.Communication.GroupInstance != null)
                {
                    Herd.Instance.AddGroup(_group.Index, sheeps);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sheeps")
        {

        }
    }
    public Transform GetNearestSheep()
    {
        Transform Sheep = null;

        if (_group != null)
        {
            float magnitude = float.PositiveInfinity;

            for (int i = 0; i < _group.SheepsHerd.Count; i++)
            {
                float magnitudeSheeps = (_sheepsMain.transform.position - _group.SheepsHerd[i].transform.position).sqrMagnitude;

                if (magnitude > magnitudeSheeps && _sheepsMain != _group.SheepsHerd[i])
                {
                    magnitude = (_sheepsMain.transform.position - _group.SheepsHerd[i].transform.position).sqrMagnitude;
                    Sheep = _group.SheepsHerd[i].transform;
                }
            }
        }

        return Sheep;
    }
    public void GroupInitialization(Group group)
    {
        _group = group;
    }
    public void SetDirectionGroup(Quaternion Direction)
    {
        if (_group != null)
        {
            if (_group.Leader==null||_group.Leader == _sheepsMain)
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
}
