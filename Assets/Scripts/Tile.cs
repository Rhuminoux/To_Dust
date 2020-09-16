using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Building.BuildingType? type = null;
    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {}
    
    public void CreateBuilding(Building.BuildingType type)
    {
        this.type = type; 
        if (type == 0)
            gameObject.AddComponent<Road>();
        else if (type == Building.BuildingType.RessourceTank)
            gameObject.AddComponent<RessourceTank>();
        else if (type == Building.BuildingType.RessourcesProducer)
            gameObject.AddComponent<RessourceProducer>();
        else if (type == Building.BuildingType.OffensiveInstallation)
            gameObject.AddComponent<OffensiveInstallation>();
    }
}
