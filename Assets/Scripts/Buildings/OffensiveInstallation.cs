using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveInstallation : Building
{
    [Header("Fire Settings")]
    [Header("--= OffensiveInstallation Attributes =--")]
    public int I_firePower = 1;
    public int I_firePowerL2 = 2;
    public int I_firePowerL3 = 3;

    public float F_fireRate = 1;
    public float F_fireRateL2 = 1.25f;
    public float F_fireRateL3 = 1.5f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = Type.OffensiveInstallation;
    }

    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(I_firePower);
    }

    public override void EvolveStatsCurrentBuilding(int level)
    {
        I_firePower = (int)this.GetType().GetField("I_firePowerL" + level).GetValue(this);
        F_fireRate = (int)this.GetType().GetField("I_fireRateL" + level).GetValue(this);
        I_maxLife = (int)this.GetType().GetField("I_maxLifeL" + level).GetValue(this);
        I_regenPoint = (int)this.GetType().GetField("I_regenPointL" + level).GetValue(this);
    }
}
