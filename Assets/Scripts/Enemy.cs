using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum LifeStatus
    {
        GoodShape = 1,
        BadShape = 2,
        Dead = 3
    };

    [Header("Life Settings")]
    [Header("--= Enemy Attributes =--")]
    public int I_currentLife = 15;
    public int I_maxLife = 15;

    public LifeStatus ActualLifeStatus
    {
        get => I_currentLife >= (I_maxLife / 2) ? LifeStatus.GoodShape :
               I_currentLife > 0 ? LifeStatus.BadShape :
               LifeStatus.Dead;
    }

    [Header("Board Settings")]
    public GameObject GO_tileManager;
    public Tile[,] getBoard { get => GO_tileManager.GetComponent<TileManager>().board; }
    public int getSizeX { get => GO_tileManager.GetComponent<TileManager>().size_x; }
    public int getSizeY { get => GO_tileManager.GetComponent<TileManager>().size_y; }

    [Header("Power Settings")]
    public int I_firePower = 1;
    public float F_fireRate = 1;

    [Header("Size Settings")]
    public int I_sizeX = 1;
    public int I_sizeZ = 1;

    void Start()
    {
        GO_tileManager = GameObject.FindWithTag("TileManager");
    }

    int CheckEnvironementAround(int x, int y)
    {
        int result = 0;

        if (x != 0 && getBoard[x - 1, y] != null)
        {
            if(getBoard[x - 1, y].type != null)
            {
                // getBoard[x - 1, y].GetComponent<Building>().I_creationCost = 500;
            }

            result += 8;
        }
        if (x != getSizeX - 1 && getBoard[x + 1, y] != null)
        {
            result += 2;
        }
        if (y != getSizeY - 1 && getBoard[x, y + 1] != null)
        {
            result += 1;
        }
        if (y != 0 && getBoard[x, y - 1] != null)
        {
            result += 4;
        }
        return result;
    }

    public void Attack(GameObject building)
    {
        building.GetComponent<Building>().TakeDamage(I_firePower);
    }

    public void TakeDamage(int I_Damage)
    {
        I_currentLife -= I_Damage;

        float f_ratio = I_currentLife / I_maxLife;
        float f_colorValue = 255 * f_ratio; // Risque d'être tout noir, à tester.
        this.GetComponent<SpriteRenderer>().color = new Color(f_colorValue, f_colorValue, f_colorValue);

        if (ActualLifeStatus == LifeStatus.Dead)
        {
            Destroy(this.GetComponent<GameObject>());
        }
    }
}
