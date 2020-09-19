using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumGame;

public class Tile : MonoBehaviour
{
    public TypeEnvironement B_type = TypeEnvironement.Empty;
    public int I_x;
    public int I_y;

    public void SetPosition(int x, int y)
    {
        I_x = x;
        I_y = y;
    }
}
