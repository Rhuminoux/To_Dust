using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Building
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = BuildingType.Road;
    }

    public override void EvolveStatsCurrentBuilding(int level)
    {
        I_maxLife = (int)this.GetType().GetField("I_maxLifeL" + level).GetValue(this);
        I_regenPoint = (int)this.GetType().GetField("I_regenPointL" + level).GetValue(this);
    }
}
