using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public int size_x = 100;
    public int size_y = 50;
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
}
