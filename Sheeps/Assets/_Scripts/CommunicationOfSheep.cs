using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationOfSheep : MonoBehaviour
{
    [SerializeField]
    private Sheeps _sheepsMain;
    private Group _group; public Group GroupInstance { get { return _group; } }
    //
    private List<Sheeps> _sheepsList = new List<Sheeps>();
    void Start()
    {
    }

    void FixedUpdate()
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sheeps")
        {

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

        if (_sheepsList.Count > 0)
        {
            float magnitude = float.PositiveInfinity;

            for (int i = 0; i < _sheepsList.Count; i++)
            {
                float magnitudeSheeps = (_sheepsMain.transform.position - _sheepsList[i].transform.position).sqrMagnitude;

                if (magnitude > magnitudeSheeps
                    && magnitudeSheeps > _sheepsMain.MinDistend)
                {
                    magnitude = (_sheepsMain.transform.position - _sheepsList[i].transform.position).sqrMagnitude;
                    Sheep = _sheepsList[i].transform;
                }
            }
        }

        return Sheep;
    }
    public void GroupInitialization(Group group)
    {
        _group = group;
    }
}
