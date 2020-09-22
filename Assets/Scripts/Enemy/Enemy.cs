using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumGame;

public class Enemy : MonoBehaviour
{
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
    public Tile[,] GetBoard { get => GO_tileManager.GetComponent<TileManager>().board; }
    public int GetSizeX { get => GO_tileManager.GetComponent<TileManager>().size_x; }
    public int GetSizeY { get => GO_tileManager.GetComponent<TileManager>().size_y; }

    [Header("Power Settings")]
    public int I_firePower = 5;
    public float F_fireRate = 1;

    [Header("Size Settings")]
    public int I_sizeX = 1;
    public int I_sizeZ = 1;

    private void Awake()
    {
        GO_tileManager = GameObject.FindWithTag("TileManager");
    }

    void Start()
    {
        GO_tileManager = GameObject.FindWithTag("TileManager");
        TimeDayNightManager.TimePassed += EnemyBehaviour_TimePassed;
    }

    public void EnemyBehaviour_TimePassed(EventArgs e)
    {
        Tile tile = GetTileWithBuilding();
        if (tile == null)
        {
            tile = FindPromximaTile();
            MoveToTileDirection(tile);
        }
        else
        {
            Attack(tile.GetComponent<Building>());
        }
    }

    public void MoveToTileDirection(Tile tile)
    {
        int i_currentX = this.GetComponent<Tile>().I_x;
        int i_currentY = this.GetComponent<Tile>().I_y;

        int i_diffX = System.Math.Abs(i_currentX - tile.I_x);
        int i_diffY = System.Math.Abs(i_currentY - tile.I_y) * 2;

        if ((i_diffY == 0 && i_diffX > i_diffY)  ||  (i_diffX != 0 && i_diffX < i_diffY))
        {
            this.GetComponent<Tile>().I_x = GetNextValueFromToStep1(i_currentX, tile.I_x);
        }
        else
        {
            this.GetComponent<Tile>().I_y = GetNextValueFromToStep1(i_currentY, tile.I_y);
        }
        GetBoard[i_currentX, i_currentY].typeEnvironement = TypeEnvironement.Empty;
        GetBoard[this.GetComponent<Tile>().I_x, this.GetComponent<Tile>().I_y] = this.GetComponent<Tile>();

        Vector3 v3_newPosition = new Vector3((float)this.GetComponent<Tile>().I_x, (float)this.GetComponent<Tile>().I_y, 0);
        this.gameObject.transform.position = v3_newPosition; 
    }

    public Tile FindPromximaTile()
    {
        Tile tile = null;

        tile = FindBuildingInX(this.GetComponent<Tile>().I_y);

        if (tile == null)
        {
            int i_yToBottom = this.GetComponent<Tile>().I_y - 1;
            int i_yToTop = this.GetComponent<Tile>().I_y + 1;

            while (tile == null)
            {
                if (i_yToBottom > 0)
                {
                    tile = FindBuildingInX(i_yToBottom);
                    i_yToBottom--;
                }

                if (tile == null && i_yToTop < 10)
                {
                    tile = FindBuildingInX(i_yToTop);
                    i_yToTop++;
                }
            }
        }
        return tile;
    }

    public Tile FindBuildingInX(int i_y)
    {
        Tile tile = null;
        int i_myX = this.GetComponent<Tile>().I_x;

        int i_borderX = i_myX >= (int)GetSizeX / 2 ? GetSizeX : 0;
        
        if(i_borderX == GetSizeX)
        {

            int i_x = GetSizeX;
            while(tile == null && i_x > 0)
            {
                i_x--;
                if (i_x != 0 && i_y != 0 && GetBoard[i_x, i_y] != null 
                    && GetBoard[i_x, i_y].typeEnvironement !=TypeEnvironement.Empty && GetBoard[i_x, i_y].typeEnvironement != TypeEnvironement.Enemy)
                    tile = GetBoard[i_x, i_y];
            }
        }
        else
        {
            int i_x = 0;
            while (tile == null && i_x > GetSizeX)
            {
                i_x++;
                if (i_x != 0 && i_y != 0 && GetBoard[i_x, i_y] != null 
                    && GetBoard[i_x, i_y].typeEnvironement != TypeEnvironement.Empty && GetBoard[i_x, i_y].typeEnvironement != TypeEnvironement.Enemy)
                    tile = GetBoard[i_x, i_y];
            }
        }

        return tile;
    }

    public Tile GetTileWithBuilding()
    {
        int i_x = this.GetComponent<Tile>().I_x;
        int i_y = this.GetComponent<Tile>().I_y;
        
        Tile tile = null;

        if (i_x != 0 && GetBoard[i_x - 1, i_y] != null 
            && GetBoard[i_x - 1, i_y].typeEnvironement != TypeEnvironement.Empty && GetBoard[i_x - 1, i_y].typeEnvironement != TypeEnvironement.Enemy)
        {
            tile = GetBoard[i_x - 1, i_y];
        }
        else if (i_x != GetSizeX - 1 && GetBoard[i_x + 1, i_y] != null
            && GetBoard[i_x + 1, i_y].typeEnvironement != TypeEnvironement.Empty && GetBoard[i_x + 1, i_y].typeEnvironement != TypeEnvironement.Enemy)
        {
            tile = GetBoard[i_x + 1, i_y];
        }
        else if (i_y != GetSizeY - 1 && GetBoard[i_x, i_y + 1] != null
            && GetBoard[i_x, i_y + 1].typeEnvironement != TypeEnvironement.Empty && GetBoard[i_x, i_y + 1].typeEnvironement != TypeEnvironement.Enemy)
        {
            tile = GetBoard[i_x, i_y + 1];
        }
        else if (i_y != 0 && GetBoard[i_x, i_y - 1] != null
            && GetBoard[i_x, i_y - 1].typeEnvironement != TypeEnvironement.Empty && GetBoard[i_x, i_y - 1].typeEnvironement != TypeEnvironement.Enemy)
        {
            tile = GetBoard[i_x, i_y - 1];
        }
        return tile;
    }

    public int GetNextValueFromToStep1(int i_from, int i_to)
    {
        if(i_from > i_to)
        {
            i_from--;
        }
        else if(i_from < i_to)
        {
            i_from++;
        }
        return i_from;
    }
    public void Attack(Building building)
    {
        building.TakeDamage(I_firePower);
    }

    public void TakeDamage(int i_damage)
    {
        I_currentLife -= i_damage;

        float f_ratio = (float)I_currentLife / (float)I_maxLife;
        float f_colorValue = 255 * f_ratio; // Risque d'être tout noir, à tester.
        this.GetComponent<SpriteRenderer>().color = new Color(f_colorValue, f_colorValue, f_colorValue);

        if (ActualLifeStatus == LifeStatus.Dead)
        {
            GetBoard[this.GetComponent<Tile>().I_x, this.GetComponent<Tile>().I_y].typeEnvironement = TypeEnvironement.Empty;
            Destroy(this.GetComponent<GameObject>());
        }
    }
}
