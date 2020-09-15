using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceTank : Building
{
    [Header("--= RessourceTank Attributes =--")]
    [Header("Tank Settings")]
    public int I_ressouceTank = 100;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = BuildingType.RessourceTank;
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddLimitStock(I_ressouceTank);
    }
}
