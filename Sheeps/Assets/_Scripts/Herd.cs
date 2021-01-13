using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herd : MonoBehaviour
{
    [SerializeField]
    private List<Sheeps> _sheepsList;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    [ContextMenu("HerdSearch")]
    private void HerdSearch()
    {
        _sheepsList = new List<Sheeps>();
        _sheepsList.AddRange(FindObjectsOfType<Sheeps>());
    }
}
