﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceTanks : Building
{
    public int I_ressouceTank = 100;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        GO_ressourcesManager.GetComponent<RessourcesManager>().AddLimitStock(I_ressouceTank);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
