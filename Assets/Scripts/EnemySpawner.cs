using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject enemyPrefab;
    FireHealthScript fireHealth;
    Vector2 randomPos, firePos;
    
    float timeInterval = 5f;
    float spawnDistance = 5f;

    float screenMinY, screenMaxY;

    void Start()
    {
        screenMinY = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        screenMaxY = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height)).y;
        
        firePos = fire.transform.position;

        fireHealth = fire.GetComponent<FireHealthScript>();

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(fireHealth.fireHealth >=1)
        {
            yield return new WaitForSeconds(timeInterval);

            randomPos.y = Random.Range(screenMinY, screenMaxY);
            
            if(Random.Range(0,2) == 0)
                randomPos.x = Random.Range(firePos.x - spawnDistance*2, firePos.x - spawnDistance);
            else
                randomPos.x = Random.Range(firePos.x + spawnDistance, firePos.x + spawnDistance*2);

            var enemyHolder = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            enemyHolder.GetComponent<EnemyController>().fire = fire;

            yield return new WaitForSeconds(1f/*TODO: Add spawn animation time here*/);
            enemyHolder.GetComponent<Rigidbody2D>().velocity = firePos - randomPos / 5;

            timeInterval *= .95f;
            Debug.Log(timeInterval);
        }
    }
}
