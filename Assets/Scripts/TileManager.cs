using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public int size_x = 20;
    public int size_y = 10;
    public List<Sprite> tiles_sprite;
    public Tile[,] board;
    // Start is called before the first frame update
    void Start()
    {
        board = new Tile[size_x, size_y];
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (y != 0 && board[x, y - 1].taken != true)
            result += 1;
        if (y != size_y - 1 && board[x, y + 1].taken != true)
            result += 4;
        return result;
    }
}
