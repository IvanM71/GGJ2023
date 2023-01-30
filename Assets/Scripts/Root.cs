using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root
{
    public RootStages stage;
    public bool[] _growDirections; // 0 - up, 1 - down, 2 - left, 3 - right
    public Root(RootStages stage)
    {
        this.stage = stage;
        _growDirections = new bool[4] { false, false, false, false };
    }
}
