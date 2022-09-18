using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : Prefab
{
   private Vector3 projectileSpawnPosition;
   public Vector3 mousePos;
   public Projectile projectile;
   private float range;
   private float shootTimerMax ;
   private float shootTimer;
   private int damage;
   public int cost;
   public int generateGold;

   private void Awake()
   {
    projectileSpawnPosition = transform.Find("GunPoint").position;
    range = 5;
    shootTimerMax = 1f;
    damage = 5;
    cost = 100;
   }

   private void Update()
   {
    shootTimer-=Time.deltaTime;
        if(shootTimer<=0)
        {

        shootTimer = shootTimerMax;

        Prefab enemy = FindClosestEnemyinRange(transform.position, range);
        if(enemy != null)
            {
            projectile.Create(projectileSpawnPosition, enemy, Random.Range(damage-2,damage+2) );    
            }
            
            GameManager.Instance.gold += generateGold;

        }
   }

   private Prefab FindClosestEnemyinRange (Vector3 position, float maxRange)
   {
    Prefab closestEnemy = null;
    foreach(Prefab enemy in WaveSpawner.enemyList)
    {
        if(Vector3.Distance(position, enemy.transform.position)<=maxRange)
        {
            if(closestEnemy==null)
            {
                closestEnemy = enemy;
            } else
            {
                if(Vector3.Distance(position, enemy.transform.position)<Vector3.Distance(position, closestEnemy.transform.position))
                {
                    closestEnemy = enemy;
                }
            }
        }
    }
    return closestEnemy;
   }

    public void UpgradeDamage()
    {
        damage += 2;
    }

    public void UpgradeRange()
    {
        range += 4;
    }

    private void OnMouseEnter()
    {
        UpgradeOverlay.ShowStatic(this);
    }
   
}
