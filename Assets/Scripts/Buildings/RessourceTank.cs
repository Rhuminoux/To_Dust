using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceTank : Building
{
    [Header("Tank Settings")]
    [Header("--= RessourceTank Attributes =--")]
    public int I_ressouceTank = 100;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = BuildingType.RessourceTank;
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddLimitStock(I_ressouceTank);
    }
}
