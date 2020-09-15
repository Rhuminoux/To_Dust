﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveInstallation : Building
{
    public int I_firePower;
    public float F_fireRate;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = BuildingType.OffensiveInstallation;
    }

}