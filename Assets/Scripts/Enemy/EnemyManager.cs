using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumGame;

public class EnemyManager : MonoBehaviour
{
    [Header("--= EnemyManager Attributes =--")]
    public List<GameObject> PrefabEnemy;
    
    public TileManager GO_tileManager;
    public List<GameObject> enemies;
    public float spawnTime = 10;

    private float _timer;
    private GameObject _new_enemy;
    void Start()
    {
        _timer = spawnTime;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            SpawnEnemy();
            _timer = spawnTime;
        }
    }

    private void SpawnEnemy()
    {
        _new_enemy = GameObject.Instantiate(PrefabEnemy[Random.Range(0, PrefabEnemy.Count - 1)], new Vector3(1, Random.Range(1, GO_tileManager.size_y), -5), new Quaternion(0, 0, 0, 0));

        _new_enemy.transform.SetParent(transform);
        enemies.Add(_new_enemy);
    }

}
