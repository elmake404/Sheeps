using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group
{
    public Group(int index, Sheeps sheep)
    {
        Index = index;
        SheepsHerd = new List<Sheeps>();
        SheepsHerd.Add(sheep);
    }
    public int Index;
    public List<Sheeps> SheepsHerd;
}
public class Herd : MonoBehaviour
{
    public static Herd Instance;

    private List<Group> _groupsSheep = new List<Group>();
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(_sheeps.Count);
        }
    }
    public void NewGroup(Sheeps _sheeps)
    {
        //ему надо записать индекс
        var NewGroup = new Group(_groupsSheep.Count - 1, _sheeps);
        _groupsSheep.Add(NewGroup);
        _sheeps.Communication.GroupInitialization(NewGroup);
    }
    public void AddGroup(int index, Sheeps sheeps)
    {
        //ему надо записать индекс
        _groupsSheep[index].SheepsHerd.Add(sheeps);
        sheeps.Communication.GroupInitialization(_groupsSheep[index]);
    }
    public void UnificationOfGroups(int indexAddedGroup, int indexWillAddedGroup)
    {
        _groupsSheep[indexAddedGroup].SheepsHerd.AddRange(_groupsSheep[indexWillAddedGroup].SheepsHerd);
        _groupsSheep.RemoveAt(indexAddedGroup);
        for (int i = 0; i < _groupsSheep.Count; i++)
        {
            _groupsSheep[i].Index = i;
        }
    }
    public void RemoveGroup(int index, Sheeps sheeps)
    {
        _groupsSheep[index].SheepsHerd.Remove(sheeps);
        if (_groupsSheep[index].SheepsHerd.Count <= 0)
        {
            _groupsSheep.RemoveAt(index);

            for (int i = 0; i < _groupsSheep.Count; i++)
            {
                _groupsSheep[i].Index = i;
            }

        }
    }
}
