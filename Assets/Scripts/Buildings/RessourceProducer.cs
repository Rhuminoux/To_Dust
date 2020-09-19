using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumGame;

public class RessourceProducer : Building
{
    [Header("Ressource Settings")]
    [Header("--= RessourceProducer Attributes =--")]
    public int I_ressource = 1;
    public int I_ressourceL2 = 2;
    public int I_ressourceL3 = 3;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = TypeEnvironement.RessourcesProducer;
        TimeDayNightManager.TimePassed += AddRessourcesToStock_TimePassed;
    }

    public void AddRessourcesToStock_TimePassed(EventArgs e)
    {
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddToStock(I_ressource);
    }

    public override void EvolveStatsCurrentBuilding(int level)
    {
        I_ressource = (int)this.GetType().GetField("I_ressourceL" + level).GetValue(this);
        I_maxLife = (int)this.GetType().GetField("I_maxLifeL" + level).GetValue(this);
        I_regenPoint = (int)this.GetType().GetField("I_regenPointL" + level).GetValue(this);
    }
}
