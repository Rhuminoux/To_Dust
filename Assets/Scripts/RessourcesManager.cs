using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManager : MonoBehaviour
{
    public int I_currentStock;
    public int I_maxStock = 1000;

    public bool RemoveToStock(int ressourcePoints)
    {
        bool b_autoriseRemove = true;
        if(I_currentStock - ressourcePoints < 0)
        {
            b_autoriseRemove = false;
        }
        return b_autoriseRemove;
    }

    public void AddToStock(int ressourcePoints)
    {
        if(I_currentStock < this.I_maxStock)
        {
            I_currentStock += ressourcePoints;
            I_currentStock = I_currentStock > I_maxStock ? I_maxStock : I_currentStock;
        }

    }

    public void AddLimitStock(int stockRessource)
    {
        I_maxStock += stockRessource;
    }
}
