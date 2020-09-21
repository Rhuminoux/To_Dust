using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumGame;

public class EnemyManager : MonoBehaviour
{
    [Header("--= EnemyManager Attributes =--")]
    public GameObject PrefabEnemyTile;
    [Header("Board Settings")]
    public GameObject GO_tileManager;
    public Tile[,] GetBoard { get => GO_tileManager.GetComponent<TileManager>().board; }
    public int GetSizeX { get => GO_tileManager.GetComponent<TileManager>().size_x; }
    public int GetSizeY { get => GO_tileManager.GetComponent<TileManager>().size_y; }
    private void Awake()
    {
        GO_tileManager = GameObject.FindWithTag("TileManager");
    }

    void Start()
    {
        List<Tile> list_tile;
        list_tile = GetAllEmptyTiles();
        Tile random_empty_tile = GetRandomTile(list_tile);
        CreateEnemyOnEmptyTile(random_empty_tile);

    }

    private List<Tile> GetAllEmptyTiles()
    {
        List<Tile> list_tile = new List<Tile>();
        for (int i = 0; i < GetSizeX; ++i)
        {
            for (int j = 0; j < GetSizeY; ++j)
            {
                if (GetBoard[i, j] != null && GetBoard[i, j].typeEnvironement == TypeEnvironement.Empty)
                {
                    list_tile.Add(GetBoard[i, j]);
                }
            }
        }
        return list_tile;
    }
    private Tile GetRandomTile(List<Tile> list_tile)
    {
        int random_index = Random.Range(0, list_tile.Count - 1);
        return list_tile[random_index];
    }

    private void CreateEnemyOnEmptyTile(Tile emptyTile)
    {
        PrefabEnemyTile.GetComponent<Tile>().SetPosition((int)emptyTile.I_x, (int)emptyTile.I_y);
        GameObject go_enemy = GameObject.Instantiate(PrefabEnemyTile, new Vector3(emptyTile.I_x, emptyTile.I_y, -0.1f), new Quaternion(0, 0, 0, 0) /*, coral_tiles_p.transform*/);
        GetBoard[emptyTile.I_x, emptyTile.I_y] = go_enemy.GetComponent<Tile>();
    }

}
