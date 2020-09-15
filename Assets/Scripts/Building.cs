using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string S_name;
    public int I_ressource = 1;

    void Start()
    {
        TimeDayNightManager.TimePassed += AddRessourcesToStock_TimePassed;
    }

    public void AddRessourcesToStock_TimePassed(EventArgs e)
    {
        RessourcesManager.AddToStock(I_ressource);
    }


}
