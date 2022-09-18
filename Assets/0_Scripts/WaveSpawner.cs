using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
   public GameObject enemyPrefab;
   public float timeBetweenWaves = 10f;
   private int _waveNumber=0;
   [SerializeField] private float _timeBetweenSpawns = 0.4f;
   [SerializeField] private float _countdown = 2f;
   public Transform spawnPoint;

   public static List<Prefab> enemyList = new List<Prefab>();
   
/// <summary>
/// Start is called on the frame when a script is enabled just before
/// any of the Update methods is called the first time.
/// </summary>
private void Start()
{
     enemyPrefab.GetComponent<Prefab>().isEnemy = true;
     
}
   void Update()
   {
        if (_countdown<=0)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;

        }
        _countdown -= Time.deltaTime;
   }

   IEnumerator SpawnWave()
    {
        _waveNumber++;

        for(int i = 0; i < _waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_timeBetweenSpawns);
        }
   
    }
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemyPrefab.GetComponent<Prefab>().isEnemy = true;
        
        
    }
}
