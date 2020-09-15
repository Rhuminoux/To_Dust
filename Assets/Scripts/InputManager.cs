using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TileManager tile_manager;
    public GameObject build_menu;

    RaycastHit2D hit;
    GameObject newTile;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            if (hit.collider != null && hit.collider.tag.StartsWith("Background"))
            {
                tile_manager.PlaceTile(hit.collider.transform.position.x, hit.collider.transform.position.y);
            }
        }*/

        if (Input.GetMouseButtonDown(1))
        {
            build_menu.SetActive(true);
            build_menu.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(1))
            build_menu.SetActive(false);
            
    }
}
