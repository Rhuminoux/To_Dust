using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumGame;

public class Enemy : MonoBehaviour
{
    [Header("Life Settings")]
    [Header("--= Enemy Attributes =--")]
    public int I_currentLife = 15;
    public int I_maxLife = 15;
    public int range = 1;
    public int speed = 1;

    private GameObject _target;
    private float dist = 1000;

    private int _attack_speed = 1;
    private int _time_to_attack;
    private void Start()
    {
        GetTarget();
    }

    public void TakeDamage(int dmg)
    {
        I_currentLife -= dmg;
    }

    private void Update()
    {
        if (dist > range)
        {
            transform.right = _target.transform.position - transform.position;
            transform.position += transform.right * Time.deltaTime * speed;
            dist = Vector3.Distance(this.transform.position, _target.transform.position);
        }
        else
        {
            if (_time_to_attack < 0)
            {
                Attack();
                _time_to_attack = _attack_speed;
            }
        }
    }

    private void Attack()
    {
        //_target.GetComponent<>();
    }


    public void GetTarget()
    {
        dist = 1000;
        foreach(var t in transform.parent.GetComponent<EnemyManager>().GO_tileManager.board)
        {
            if (t != null && t.typeEnvironement != TypeEnvironement.Empty)
                if (Vector3.Distance(this.transform.position, t.transform.position) < dist)
                {
                    _target = t.gameObject;
                    dist = Vector3.Distance(this.transform.position, t.transform.position);
                }
        }
    }
}
