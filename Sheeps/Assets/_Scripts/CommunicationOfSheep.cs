using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationOfSheep : MonoBehaviour
{
    [SerializeField]
    private Collider _colliderMain;
    [SerializeField]
    private Sheeps _sheepsMain;
    private List<Sheeps> _sheepsList = new List<Sheeps>();
    void Start()
    {
        _colliderMain.enabled = false;
    }

    void FixedUpdate()
    {
        if (_colliderMain.enabled && _sheepsList.Count > 0)
        {
            for (int i = 0; i < _sheepsList.Count; i++)
            {
                _sheepsList[i].Herd(_sheepsMain.transform.forward);
            }
        }
    }
    public void Activation()
    {
        _colliderMain.enabled = true;
    }
    public void Deactivation()
    {
        _colliderMain.enabled = false;
        for (int i = 0; i < _sheepsList.Count; i++)
        {
            _sheepsList[i].OffHerd();
        }
        _sheepsList.Clear();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sheeps")
        {
            var sheeps = other.GetComponentInParent<Sheeps>();
            if (_sheepsMain != sheeps && !_sheepsList.Contains(sheeps))
            {
                _sheepsList.Add(sheeps);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sheeps")
        {
            var sheeps = other.GetComponentInParent<Sheeps>();

            if (_sheepsList.Contains(sheeps))
            {
                _sheepsList.Remove(sheeps);
                sheeps.OffHerd();
            }
        }
    }
}
