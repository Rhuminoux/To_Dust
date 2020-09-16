using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RessourcesManager : MonoBehaviour
{
    public GameObject GO_TMP_ressource;
    public int I_currentStock = 100;
    public int I_maxStock = 1000;

    void FixedUpdate()
    {
        GO_TMP_ressource.GetComponent<TextMeshProUGUI>().text = "Ressources : " + I_currentStock;
    }

    public bool RemoveToStock(int ressourcePoints)
    {
        bool b_autoriseRemove = true;
        if(I_currentStock - ressourcePoints < 0)
        {
            b_autoriseRemove = false;
        }
        else
        {
            I_currentStock = I_currentStock - ressourcePoints;
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
