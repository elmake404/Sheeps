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

    public List<Sheeps> SheepsHerd;
    public Quaternion DirectionGroup;
    public Sheeps Leader;

    public int Index;
    public bool IsDirectionSet;
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
            Debug.Log(_groupsSheep.Count);
            for (int i = 0; i < _groupsSheep.Count; i++)
            {
               Debug.Log( _groupsSheep[i].SheepsHerd.Count);
            }
        }
    }
    public void NewGroup(Sheeps _sheeps)
    {
        var NewGroup = new Group(_groupsSheep.Count, _sheeps);
        _groupsSheep.Add(NewGroup);
        _sheeps.Communication.GroupInitialization(NewGroup);
    }
    public void AddGroup(int index, Sheeps sheeps)
    {
        _groupsSheep[index].SheepsHerd.Add(sheeps);
        sheeps.Communication.GroupInitialization(_groupsSheep[index]);
    }
    public void UnificationOfGroups(int indexAddedGroup, int indexWillAddedGroup)
    {
        _groupsSheep[indexWillAddedGroup].SheepsHerd.AddRange(_groupsSheep[indexAddedGroup].SheepsHerd);

        for (int i = 0; i < _groupsSheep[indexAddedGroup].SheepsHerd.Count; i++)
        {
            _groupsSheep[indexAddedGroup].SheepsHerd[i].Communication.GroupInitialization(_groupsSheep[indexWillAddedGroup]);
        }
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
