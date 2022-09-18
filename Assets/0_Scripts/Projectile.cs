using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

 //   public GameObject fireballPrefab;

     private Prefab enemy;
     private int damageAmount;
     private GameObject enemyObject;
     private float travelTime;
     private float maxTravelTime;

     void Awake()
     {
        maxTravelTime = 3f;
        travelTime = 2f;
     }
  
//                     Tower tower  Vector3 spawnPosition = tower.transform.position
   public void Create(Vector3 spawnPosition,Prefab enemy, int damageAmount) 
   {
    Transform projectileTransform = Instantiate(this.transform, spawnPosition, Quaternion.identity);
    Projectile  projectile = projectileTransform.GetComponent<Projectile>();
    projectile.Setup(enemy, damageAmount);

   }


    private void Setup (Prefab enemy, int damageAmount)
    {
        this.enemy =enemy;
        this.damageAmount = damageAmount;
        this.enemyObject = enemy.gameObject; 
    }


   
   private void Update()
   {
    Vector3 targetPos = enemy.transform.position;
    Vector3 movedir = (targetPos - transform.position).normalized;
    float moveSpeed = 7f;
    transform.position += movedir * moveSpeed * Time.deltaTime;

    
    if((Vector3.Distance(transform.position, targetPos) < 0.05))
    {
        enemy.TakeDamage(damageAmount);
        Destroy(gameObject);
    }
    
    Destroy(gameObject,0.6f);


   }
    
}

