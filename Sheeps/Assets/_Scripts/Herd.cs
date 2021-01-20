﻿using System.Collections;
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
                for (int j = 0; j < _groupsSheep[i].SheepsHerd.Count; j++)
                {
                    Debug.Log(_groupsSheep[i].SheepsHerd[j].name);
                }
            }
        }
    }
    public void NewGroup(Sheeps _sheeps)
    {
        var NewGroup = new Group(_groupsSheep.Count, _sheeps);
        _groupsSheep.Add(NewGroup);
        _sheeps.Communication.GroupInitialization(NewGroup);
        _sheeps.IsInHerd = true;
    }
    public void AddGroup(int index, Sheeps sheeps)
    {
        if (!_groupsSheep[index].SheepsHerd.Contains(sheeps))
        {
            _groupsSheep[index].SheepsHerd.Add(sheeps);
            sheeps.Communication.GroupInitialization(_groupsSheep[index]);
        }
    }
    public void UnificationOfGroups(int indexAddedGroup, int indexWillAddedGroup)
    {
        _groupsSheep[indexWillAddedGroup].SheepsHerd.AddRange(_groupsSheep[indexAddedGroup].SheepsHerd);

        for (int i = 0; i < _groupsSheep[indexAddedGroup].SheepsHerd.Count; i++)
        {
            _groupsSheep[indexAddedGroup].SheepsHerd[i].MovingToAnotherGroup(_groupsSheep[indexWillAddedGroup]);
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
    public void GroupCheck(Sheeps sheep)
    {
        List<Sheeps> CheckList = new List<Sheeps>();

        CheckList.Add(sheep);
        CheckList.AddRange(sheep.Communication.GetShepsNeighbors());

        int i = 1;

        while (true)
        {
            if (i>= CheckList.Count)
            {
                break;
            }
            List<Sheeps> Sheeps = new List<Sheeps>();
            Sheeps.AddRange(CheckList[i].Communication.GetShepsNeighbors());
            for (int j = 0; j < Sheeps.Count; j++)
            {
                if (!CheckList.Contains(Sheeps[j]))
                {
                    CheckList.Add(Sheeps[j]);
                }
            }
            i++;
        }

        List<Sheeps> SheepsGroup = sheep.Communication.GroupInstance.SheepsHerd;
        List<Sheeps> Fugitives = new List<Sheeps>();

        for (int t = 0; t < SheepsGroup.Count; t++)
        {
            if (!CheckList.Contains(SheepsGroup[t]))
            {
                Fugitives.Add(SheepsGroup[t]);
            }
        }

        if (CheckList.Count>=Fugitives.Count)
        {
            for (int t = 0; t < Fugitives.Count; t++)
            {
                Fugitives[t].Communication.LeavinGroup();
            }
        }
        else
        {
            for (int t = 0; t < CheckList.Count; t++)
            {
                CheckList[t].Communication.LeavinGroup();
            }
        }
        //Debug.Log(Fugitives.Count);
    }
}
