using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceProducer : Building
{
    [Header("Ressource Settings")]
    [Header("--= RessourceProducer Attributes =--")]
    public int I_ressource = 1;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = BuildingType.RessourcesProducer;
        TimeDayNightManager.TimePassed += AddRessourcesToStock_TimePassed;
    }


    public void AddRessourcesToStock_TimePassed(EventArgs e)
    {
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddToStock(I_ressource);
    }
}
