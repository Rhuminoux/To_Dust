using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Building.Type B_type = Building.Type.Empty;
    public int I_x;
    public int I_y;

    public void SetPosition(int x, int y)
    {
        I_x = x;
        I_y = y;
    }
}
