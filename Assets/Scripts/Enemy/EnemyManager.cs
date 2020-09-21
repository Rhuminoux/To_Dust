using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Board Settings")]
    public GameObject GO_tileManager;
    public Tile[,] getBoard { get => GO_tileManager.GetComponent<TileManager>().board; }
    public int getSizeX { get => GO_tileManager.GetComponent<TileManager>().size_x; }
    public int getSizeY { get => GO_tileManager.GetComponent<TileManager>().size_y; }

    private void Awake()
    {
        GO_tileManager = GameObject.FindWithTag("TileManager");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
