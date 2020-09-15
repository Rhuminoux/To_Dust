using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject tile_manager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(tile_manager.GetComponent<TileManager>().size_x / 2, tile_manager.GetComponent<TileManager>().size_y / 2, -10);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
