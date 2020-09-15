using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string S_name;
    public enum LifeStatus { GoodShape = 1, BadShape = 2, Dead = 3};
    public LifeStatus ActualLifeStatus { get => I_currentLife >= (I_maxLife / 2) ? LifeStatus.GoodShape :
                                                I_currentLife > 0 ? LifeStatus.BadShape :
                                                LifeStatus.Dead;}

    public GameObject GO_ressourcesManager;
    public int I_currentLife = 100;
    public int I_maxLife = 360;
    public int I_regenPoint = 1;

    public int I_ressource = 1;
    public int I_creationCost = 100;

    public int I_sizeX = 1;
    public int I_sizeZ = 1;

    void Start()
    {
        TimeDayNightManager.TimePassed += AddRessourcesToStock_TimePassed;
        TimeDayNightManager.TimePassed += Regen_TimePassed;
    }

    public void TakeDamage(int I_Damage)
    {
        I_currentLife -= I_Damage;
        if(ActualLifeStatus == LifeStatus.BadShape)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(200, 200, 200); 
        }
        else if(ActualLifeStatus == LifeStatus.Dead)
        {
            Destroy(this.GetComponent<GameObject>());
        }
    }

    public void AddRessourcesToStock_TimePassed(EventArgs e)
    {
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddToStock(I_ressource);
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
