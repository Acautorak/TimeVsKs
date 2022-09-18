using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab : MonoBehaviour
{
    public Tile standingOnTile;
    public bool isEnemy;
    public bool isBuilding;
    public int health = 500;
    public bool isDead;
   
    private  void Awake()
    {
        if(isEnemy)
        {
            WaveSpawner.enemyList.Add(this);
            health = 10;
        }
    }

    void Update()
    {
        if (health<=0)
        {
            isDead= true;
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        WaveSpawner.enemyList.Remove(this);
        GameManager.Instance.gold += 10;
        
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }


}
