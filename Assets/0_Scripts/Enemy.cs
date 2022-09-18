using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Prefab
{
    // Start is called before the first frame update
    void Start()
    {
       isEnemy = true;
       isBuilding = false; 
    }

    // Update is called once per frame

}

[Serializable] public struct Stats
{
    public int health;
    public int attackDamage;
    public int moveSpeed;
}
