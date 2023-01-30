using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root
{
    public RootStages stage;
    public bool[] _growDirections = new bool[4]; // 0 - up, 1 - down, 2 - left, 3 - right
    public Root(RootStages stage, bool[] growDirections)
    {
        this.stage = stage;
        _growDirections = growDirections;
    }
}
