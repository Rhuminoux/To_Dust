using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public enum BuildingType
    {
        Road = 0,
        RessourceTank = 1,
        RessourcesProducer = 2,
        OffensiveInstallation = 3
    }
    public enum LifeStatus 
    { 
        GoodShape = 1, 
        BadShape = 2, 
        Dead = 3 
    };

    [Header("--= Building Attributes =--")]
    public BuildingType ActualBuildingType;
    public GameObject GO_ressourcesManager;

    [Header("Health Settings")]
    public int I_currentLife = 100;
    public int I_maxLife = 360;
    public int I_regenPoint = 1;
    public LifeStatus ActualLifeStatus
    {
        get => I_currentLife >= (I_maxLife / 2) ? LifeStatus.GoodShape :
               I_currentLife > 0 ? LifeStatus.BadShape :
               LifeStatus.Dead;
    }

    [Header("Cost Settings")]
    public int I_creationCost = 100;

    [Header("Level Settings")]
    public int I_level = 1;
    public int I_upgradeCostL2 = 50;
    public int I_upgradeCostL3 = 100;
    public int I_maxLifeL2 = 150;
    public int I_maxLifeL3 = 150;
    public int I_regenPointL2 = 2;
    public int I_regenPointL3 = 3;
    public int I_maxLevel = 3;

    [Header("Size Settings")]
    public int I_sizeX = 1;
    public int I_sizeZ = 1;


    protected void Start()
    {
        GO_ressourcesManager = GameObject.FindWithTag("RessourcesManager");
        TimeDayNightManager.TimePassed += Regen_TimePassed;
    }

    public void TakeDamage(int I_Damage)
    {
        I_currentLife -= I_Damage;
        
        float f_ratio = I_currentLife / I_maxLife;
        float f_colorValue = 255 * f_ratio; // Risque d'être tout noir, à tester.
        this.GetComponent<SpriteRenderer>().color = new Color(f_colorValue, f_colorValue, f_colorValue); 
        
        if(ActualLifeStatus == LifeStatus.Dead)
        {
            Destroy(this.GetComponent<GameObject>());
        }
    }

    public bool UprageCurrentBuilding()
    {
        bool b_isUpgraded = false;
        if (I_level < I_maxLevel)
        {
            I_level++;
            int i_upgradeCost = (int)this.GetType().GetField("I_upgradeCost" + I_level).GetValue(this);
            if (GO_ressourcesManager.GetComponent<RessourcesManager>().RemoveToStock(i_upgradeCost))
            {
                EvolveStatsCurrentBuilding(I_level);
                b_isUpgraded = true;
            }
        }
        return b_isUpgraded;
    }
    public abstract void EvolveStatsCurrentBuilding(int level);

    public void Regen_TimePassed(EventArgs e)
    {
        if(I_currentLife < I_maxLife)
        {
            I_currentLife += I_regenPoint;
            I_currentLife = I_currentLife > I_maxLife ? I_maxLife : I_currentLife;
        }

        if (ActualLifeStatus == LifeStatus.GoodShape)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
    }


}
