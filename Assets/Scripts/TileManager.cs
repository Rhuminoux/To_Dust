using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public int size_x = 20;
    public int size_y = 10;
    public List<Sprite> tiles_sprite;
    public List<GameObject> background_sprites;
    public GameObject background_tiles_p;
    public GameObject coral_tiles_p;
    public Tile[,] board;
    public GameObject base_tile;
    // Start is called before the first frame update
    void Start()
    {
        board = new Tile[size_x, size_y];
        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateMap()
    {
        GameObject newTile;

        for (int i = 0; i < size_x; ++i)
        {
            for (int j = 0; j < size_y; ++j)
            {
                if (j == 0)
                {
                    GameObject.Instantiate(background_sprites[0], new Vector3(i, j, 0), new Quaternion(0, 0, 0, 0), background_tiles_p.transform);
                }
                else
                {
                    GameObject.Instantiate(background_sprites[1], new Vector3(i, j, 0), new Quaternion(0, 0, 0, 0), background_tiles_p.transform);
                }
                newTile = GameObject.Instantiate(base_tile, new Vector3(i, j, 0), new Quaternion(0, 0, 0, 0), coral_tiles_p.transform);
                board[i, j] = newTile.GetComponent<Tile>(); 
            }
        }
        newTile = GameObject.Instantiate(base_tile, new Vector3(size_x / 2, 1, 0), new Quaternion(0, 0, 0, 0), coral_tiles_p.transform);
        newTile.GetComponent<SpriteRenderer>().sprite = tiles_sprite[4];
        newTile.GetComponent<Tile>().taken = true;
        board[size_x / 2, size_y - 2] = newTile.GetComponent<Tile>();
    }

    public Sprite GetTileSprite(int x, int y)
    {
        return tiles_sprite[CheckAround(x, y)];
    }

    int CheckAround(int x, int y)
    {
        int result = 0;

        if (x != 0 && board[x - 1, y].taken != true)
            result += 8;
        if (x != size_x - 1 && board[x + 1, y].taken != true)
            result += 2;
        if (y != 0 && board[x, y + 1].taken != true)
            result += 1;
        if (y != size_y - 1 && board[x, y - 1].taken != true)
            result += 4;
        return result;
    }
}
