using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumGame;

public class OffensiveInstallation : Building
{
    [Header("Fire Settings")]
    [Header("--= OffensiveInstallation Attributes =--")]
    public int I_firePower = 1;
    public int I_firePowerL2 = 2;
    public int I_firePowerL3 = 3;

    public float F_fireRate = 1;
    public float F_fireRateL2 = 1.25f;
    public float F_fireRateL3 = 1.5f;

    public float R_range = 2;

    public GameObject ennemies_Manager;

    private GameObject _target;
    private float _time_to_attack;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ActualBuildingType = TypeEnvironement.OffensiveInstallation;
        ennemies_Manager = GameObject.FindWithTag("EnemyManager");
    }

    private void Update()
    {
        if (_target != null)
        {
            _time_to_attack -= Time.deltaTime;
            if (_time_to_attack < 0)
            {
                Attack(_target);
                _time_to_attack = F_fireRate;
            }
        }
        else
            GetTarget();
    }

    public void Attack(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().TakeDamage(I_firePower);
    }

    private void GetTarget()
    {
        if (ennemies_Manager.transform.childCount != 0)
            for (int i = 0; i < ennemies_Manager.transform.childCount; ++i)
            {
                if (Vector3.Distance(this.transform.position,
                    ennemies_Manager.transform.GetChild(i).position) < R_range)
                    _target = ennemies_Manager.transform.GetChild(i).gameObject;
            }
    }

    public override void EvolveStatsCurrentBuilding(int level)
    {
        I_firePower = (int)this.GetType().GetField("I_firePowerL" + level).GetValue(this);
        F_fireRate = (int)this.GetType().GetField("I_fireRateL" + level).GetValue(this);
        I_maxLife = (int)this.GetType().GetField("I_maxLifeL" + level).GetValue(this);
        I_regenPoint = (int)this.GetType().GetField("I_regenPointL" + level).GetValue(this);
    }
}
