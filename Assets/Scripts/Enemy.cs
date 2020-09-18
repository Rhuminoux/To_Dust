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

    private void Awake()
    {
        GO_tileManager = GameObject.FindWithTag("TileManager");
    }

    void Start()
    {
        GO_tileManager = GameObject.FindWithTag("TileManager");

        Tile tile = FindPromximaTile();
        
        Debug.Log(tile.B_type + " x : "  + tile.I_x + " y : " + tile.I_y);
    }

    public Tile FindPromximaTile()
    {
        Tile tile = null;

        for(int y = this.GetComponent<Tile>().I_y; y > 0; y--)
        {
            tile = FindBuildingInX(y);
        }

        return tile;
    }

    public Tile FindBuildingInX(int y)
    {
        Tile tile = null;
        int myX = this.GetComponent<Tile>().I_x;

        int borderX = myX >= (int)getSizeX / 2 ? getSizeX : 0;
        
        if(borderX == getSizeX)
        {

            int x = getSizeX;
            while(tile == null && x > 0)
            {
                x--;
                Debug.Log("x : " + x + "y : " + y);
                if (x != 0 && y != 0 && getBoard[x, y] != null && getBoard[x, y].B_type != Building.Type.Empty)
                    tile = getBoard[x, y];
            }
        }
        else
        {
            int x = 0;
            while (tile == null && x > getSizeX)
            {
                x++;
                if (x != 0 && y != 0 && getBoard[x, y] != null && getBoard[x, y].B_type != Building.Type.Empty)
                    tile = getBoard[x, y];
            }
        }

        return tile;
    }

    public Tile GetTileWithBuilding()
    {
        int x = this.GetComponent<Tile>().I_x;
        int y = this.GetComponent<Tile>().I_y;
        
        Tile tile = null;

        if (x != 0 && getBoard[x - 1, y] != null && getBoard[x - 1, y].B_type != Building.Type.Empty)
        {
            tile = getBoard[x - 1, y];
        }
        else if (x != getSizeX - 1 && getBoard[x + 1, y] != null && getBoard[x + 1, y].B_type != Building.Type.Empty)
        {
            tile = getBoard[x + 1, y];
        }
        else if (y != getSizeY - 1 && getBoard[x, y + 1] != null && getBoard[x, y + 1].B_type != Building.Type.Empty)
        {
            tile = getBoard[x, y + 1];
        }
        else if (y != 0 && getBoard[x, y - 1] != null && getBoard[x, y - 1].B_type != Building.Type.Empty)
        {
            tile = getBoard[x, y - 1];
        }
        return tile;
    }

    public void Attack(Building building)
    {
        building.TakeDamage(I_firePower);
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
