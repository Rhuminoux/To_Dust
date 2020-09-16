using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceTank : Building
{
    [Header("Tank Settings")]
    [Header("--= RessourceTank Attributes =--")]
    public int I_ressourceTank = 100;
    public int I_ressourceTankV2 = 150;
    public int I_ressourceTankV3 = 200;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = BuildingType.RessourceTank;
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddLimitStock(I_ressourceTank);
    }

    public override void EvolveStatsCurrentBuilding(int level)
    {
        int i_newRessourceTank = (int)this.GetType().GetField("I_ressourceTankV" + level).GetValue(this);
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddLimitStock(i_newRessourceTank - I_ressourceTank);
        I_ressourceTank = i_newRessourceTank;
        I_maxLife = (int)this.GetType().GetField("I_maxLifeL" + level).GetValue(this);
        I_regenPoint = (int)this.GetType().GetField("I_regenPointL" + level).GetValue(this);
    }

    public void OnDestroy()
    {
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddLimitStock(- I_ressourceTank);
    }
}
