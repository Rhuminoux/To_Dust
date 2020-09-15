using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManager : MonoBehaviour
{
    public static int I_STOCK;
    public int stock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stock = I_STOCK;
    }

    public static void AddToStock(int ressourcePoints)
    {
        I_STOCK += ressourcePoints;
    }
}
