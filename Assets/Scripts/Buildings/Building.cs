using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public string S_name;
    public enum BuildingType { Road = 0, RessourceTank = 1, RessourcesProducer = 2, OffensiveInstallation = 3 }
    public BuildingType ActualBuildingType;
    public enum LifeStatus { GoodShape = 1, BadShape = 2, Dead = 3};
    public LifeStatus ActualLifeStatus { get => I_currentLife >= (I_maxLife / 2) ? LifeStatus.GoodShape :
                                                I_currentLife > 0 ? LifeStatus.BadShape :
                                                LifeStatus.Dead;}

    public GameObject GO_ressourcesManager;
    public int I_currentLife = 100;
    public int I_maxLife = 360;
    public int I_regenPoint = 1;

    public int I_creationCost = 100;

    public int I_sizeX = 1;
    public int I_sizeZ = 1;

    public int I_Level = 1;
    public int I_maxLevel = 3;

    protected void Start()
    {
        TimeDayNightManager.TimePassed += Regen_TimePassed;
    }

    public void TakeDamage(int I_Damage)
    {
        I_currentLife -= I_Damage;
        if(ActualLifeStatus == LifeStatus.BadShape)
        {
            float f_ratio = I_currentLife / I_maxLife;

            float f_colorValue = 255 * f_ratio; // Risque d'être tout noir, à tester.

            this.GetComponent<SpriteRenderer>().color = new Color(f_colorValue, f_colorValue, f_colorValue); 
        }
        else if(ActualLifeStatus == LifeStatus.Dead)
        {
            Destroy(this.GetComponent<GameObject>());
        }
    }

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
