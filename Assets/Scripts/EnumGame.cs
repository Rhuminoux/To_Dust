using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnumGame : MonoBehaviour
{
    public enum LifeStatus
    {
        GoodShape = 1,
        BadShape = 2,
        Dead = 3
    };

    public enum TypeEnvironement
    {
        Enemy = -2,
        Empty = -1,
        Road = 0,
        RessourceTank = 1,
        RessourcesProducer = 2,
        OffensiveInstallation = 3
    };
}
