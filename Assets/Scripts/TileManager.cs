using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumGame;

public class TileManager : MonoBehaviour
{
    public int size_x = 20;
    public int size_y = 10;
    public List<Sprite> road_sprite;
    public List<Sprite> building_sprites;

    public List<GameObject> background_sprites;
    public GameObject water_tiles_p;
    public GameObject ground_tiles_p;

    public GameObject coral_tiles_p;
    public Tile[,] board;
    public GameObject base_tile;

    public GameObject GO_ressourcesManager;
    public List<GameObject> List_tilesBuilding;

    private GameObject newTile;
    // Start is called before the first frame update
    void Start()
    {
        GO_ressourcesManager = GameObject.FindWithTag("RessourcesManager");
        board = new Tile[size_x, size_y];
        CreateMap();
    }

    private void CreateMap()
    {
        for (int i = 0; i < size_x; ++i)
        {
            for (int j = 0; j < size_y; ++j)
            {
                if (j == 0)
                {
                    GameObject.Instantiate(background_sprites[0], new Vector3(i, j, 0), new Quaternion(0, 0, 0, 0), ground_tiles_p.transform);
                }
                else
                {
                    GameObject.Instantiate(background_sprites[1], new Vector3(i, j, 0), new Quaternion(0, 0, 0, 0), water_tiles_p.transform);
                }
            }
        }
        newTile = GameObject.Instantiate(base_tile, new Vector3(size_x / 2, 1, -0.1f), new Quaternion(0, 0, 0, 0), coral_tiles_p.transform);
        newTile.GetComponent<SpriteRenderer>().sprite = road_sprite[4];
        newTile.GetComponent<Tile>().SetPosition(size_x / 2, 1);
        board[size_x / 2, 1] = newTile.GetComponent<Tile>();
    }

    public void PlaceTile(float x, float y, TypeEnvironement type)
    {
        GameObject go_tileBuilding = List_tilesBuilding[(int)type];

        if (GO_ressourcesManager.GetComponent<RessourcesManager>().RemoveToStock(go_tileBuilding.GetComponent<Building>().I_creationCost))
        {
            if (go_tileBuilding.GetComponent<Building>().ActualBuildingType == TypeEnvironement.Road)
            {
                if (GetTileSprite((int)x, (int)y))
                {
                    newTile = GameObject.Instantiate(go_tileBuilding, new Vector3(x, y, -0.1f), new Quaternion(0, 0, 0, 0), coral_tiles_p.transform);
                    newTile.GetComponent<Tile>().SetPosition((int)x, (int)y);
                    board[(int)x, (int)y] = newTile.GetComponent<Tile>();

                    UpdateTiles();
                }
            }
            else
            {
                int around = CheckAround((int)x, (int)y);
                if (around == 1 || around == 2 || around == 4 || around == 8)
                {
                    newTile = GameObject.Instantiate(go_tileBuilding, new Vector3(x, y, -0.1f), new Quaternion(0, 0, 0, 0), coral_tiles_p.transform);
                    newTile.GetComponent<Tile>().SetPosition((int)x, (int)y);
                    board[(int)x, (int)y] = newTile.GetComponent<Tile>();

                    UpdateTiles();
                }
            }
        }

    }

    private void UpdateTiles()
    {
        for (int i = 0; i < size_x; ++i)
            for (int j = 0; j < size_y; ++j)
                if (board[i, j] != null && board[i, j].B_type == 0)
                    board[i, j].gameObject.GetComponent<SpriteRenderer>().sprite = GetTileSprite(i, j);
    }

    Sprite GetTileSprite(int x, int y)
    {
        return road_sprite[CheckAround(x, y)];
    }

    int CheckAround(int x, int y)
    {
        int result = 0;

        if (x != 0 && board[x - 1, y] != null)
            result += 8;
        if (x != size_x - 1 && board[x + 1, y] != null)
            result += 2;
        if (y != size_y - 1 && board[x, y + 1] != null)
            result += 1;
        if (y != 0 && board[x, y - 1] != null)
            result += 4;
        return result;
    }
}
