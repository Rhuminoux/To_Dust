using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using static EnumGame;

public abstract class Building : MonoBehaviour
{

    [Header("--= Building Attributes =--")]
    public TypeEnvironement ActualBuildingType;
    public GameObject GO_ressourcesManager;

    [Header("Board Settings")]
    public GameObject GO_tileManager;
    public Tile[,] GetBoard { get => GO_tileManager.GetComponent<TileManager>().board; }
    public int GetSizeX { get => GO_tileManager.GetComponent<TileManager>().size_x; }
    public int GetSizeY { get => GO_tileManager.GetComponent<TileManager>().size_y; }

    [Header("Health Settings")]
    public int I_currentLife = 100;
    public int I_maxLife = 360;
    public int I_regenPoint = 1;
    public LifeStatus ActualLifeStatus
    {
        get => I_currentLife == I_maxLife ? LifeStatus.GoodShape :
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

    public void TakeDamage(int i_damage)
    {
        Debug.Log("vie actuel " + I_currentLife);
        I_currentLife -= i_damage;
        Debug.Log("vie après dégat " + I_currentLife);

        Debug.Log("I_currentLife : " + I_currentLife);
        Debug.Log("I_maxLife : " + I_maxLife);
        float f_ratio = (float)I_currentLife / (float)I_maxLife;
        Debug.Log("f_ratio : " + f_ratio);
        float f_colorValue = 255 * f_ratio; // Risque d'être tout noir, à tester.
        Debug.Log("ColorValue : " + f_colorValue);

        this.GetComponent<SpriteRenderer>().color = new Color(f_colorValue, f_colorValue, f_colorValue);

        if (ActualLifeStatus == LifeStatus.Dead)
        {
            Destroy(this.gameObject);
        }
    }

    public int CountNumberOfBuildingAround()
    {
        int x = this.GetComponent<Tile>().I_x;
        int y = this.GetComponent<Tile>().I_y;
        int countBuildings = 0;

        if (x != 0 && GetBoard[x - 1, y] != null && 
            GetBoard[x - 1, y].typeEnvironement != TypeEnvironement.Empty && GetBoard[x - 1, y].typeEnvironement != TypeEnvironement.Road)
        {
            countBuildings++;
        }
        if (x != GetSizeX - 1 && GetBoard[x + 1, y] != null && 
            GetBoard[x + 1, y].typeEnvironement != TypeEnvironement.Empty && GetBoard[x + 1, y].typeEnvironement != TypeEnvironement.Road)
        {
            countBuildings++;
        }
        if (y != GetSizeY - 1 && GetBoard[x, y + 1] != null && 
            GetBoard[x, y + 1].typeEnvironement != TypeEnvironement.Empty && GetBoard[x, y + 1].typeEnvironement != TypeEnvironement.Road)
        {
            countBuildings++;
        }
        if (y != 0 && GetBoard[x, y - 1] != null && 
            GetBoard[x, y - 1].typeEnvironement != TypeEnvironement.Empty && GetBoard[x, y - 1].typeEnvironement != TypeEnvironement.Road)
        {
            countBuildings++;
        }

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
    }

    protected void OnDestroy()
    {
        GetBoard[this.GetComponent<Tile>().I_x, this.GetComponent<Tile>().I_y].typeEnvironement = TypeEnvironement.Empty;
        TimeDayNightManager.TimePassed -= Regen_TimePassed;
    }
}
