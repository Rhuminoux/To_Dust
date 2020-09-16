using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TileManager tile_manager;
    public GameObject build_menu;
    public GameObject tile_shadow;

    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !tile_shadow.activeSelf)
        {
            build_menu.SetActive(true);
            build_menu.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(1))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            if (hit.collider != null && hit.collider.tag.StartsWith("BMenu"))
            {
                tile_shadow.GetComponent<SpriteRenderer>().sprite = hit.collider.gameObject.GetComponent<BuildMenu>().sprite;
                tile_shadow.GetComponent<TileShadow>().type = hit.collider.gameObject.GetComponent<BuildMenu>().type;
                tile_shadow.SetActive(true);
            }
            else
                tile_shadow.SetActive(false);
            build_menu.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (tile_shadow.activeSelf)
            {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
                if (hit.collider != null && hit.collider.tag.StartsWith("Background"))
                {
                    tile_manager.PlaceTile(hit.collider.transform.position.x, hit.collider.transform.position.y, tile_shadow.GetComponent<TileShadow>().type);
                    tile_shadow.SetActive(false);
                }
            }
        }
            
    }
}
