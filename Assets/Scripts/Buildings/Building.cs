using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public enum Type
    {
        Empty = -1,
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
    public Type ActualBuildingType;
    public GameObject GO_ressourcesManager;

    [Header("Board Settings")]
    public GameObject GO_tileManager;
    public Tile[,] getBoard { get => GO_tileManager.GetComponent<TileManager>().board; }
    public int getSizeX { get => GO_tileManager.GetComponent<TileManager>().size_x; }
    public int getSizeY { get => GO_tileManager.GetComponent<TileManager>().size_y; }

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

    private void Awake()
    {
        GO_ressourcesManager = GameObject.FindWithTag("RessourcesManager");
        GO_tileManager = GameObject.FindWithTag("TileManager");
    }

    protected void Start()
    {  
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

    public int CountNumberOfBuildingAround()
    {
        int x = this.GetComponent<Tile>().I_x;
        int y = this.GetComponent<Tile>().I_y;
        int countBuildings = 0;

        if (x != 0 && getBoard[x - 1, y] != null && 
            getBoard[x - 1, y].B_type != Type.Empty && getBoard[x - 1, y].B_type != Type.Road)
        {
            countBuildings++;
        }
        if (x != getSizeX - 1 && getBoard[x + 1, y] != null && 
            getBoard[x + 1, y].B_type != Type.Empty && getBoard[x + 1, y].B_type != Type.Road)
        {
            countBuildings++;
        }
        if (y != getSizeY - 1 && getBoard[x, y + 1] != null && 
            getBoard[x, y + 1].B_type != Type.Empty && getBoard[x, y + 1].B_type != Type.Road)
        {
            countBuildings++;
        }
        if (y != 0 && getBoard[x, y - 1] != null && 
            getBoard[x, y - 1].B_type != Type.Empty && getBoard[x, y - 1].B_type != Type.Road)
        {
            countBuildings++;
        }
        Debug.Log("cb : " + countBuildings);
        return countBuildings;
    }

    public bool UprageCurrentBuilding()
    {
        bool b_isUpgraded = false;
        if (I_level < I_maxLevel)
        {
            I_level++;
            int i_upgradeCost = (int)this.GetType().GetField("I_upgradeCostL" + I_level).GetValue(this);
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
