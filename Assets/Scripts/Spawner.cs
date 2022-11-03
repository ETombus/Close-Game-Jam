using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject logPrefab;
    FireHealthScript fireHealth;
    Vector2 randomPos, firePos;
    
    float timeInterval = 5f;
    float maxSpawnDistance = 10f;
    float minSpawnDistance = 5f;

    private List<GameObject> logs = new List<GameObject>();

    float screenMinY, screenMaxY;

    void Start()
    {
        firePos = fire.transform.position;

        fireHealth = fire.GetComponent<FireHealthScript>();

        SpawnLog();
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while(fireHealth.fireHealth >=1)
        {
            yield return new WaitForSeconds(timeInterval);

            StartCoroutine(SpawnEnemy());

            if(logs.Count !> 5)
                SpawnLog();

            timeInterval *= .95f;
        }
    }

    IEnumerator SpawnEnemy()
    {
        randomPos = RandomPos(randomPos);

        var enemyHolder = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        enemyHolder.GetComponent<EnemyController>().fire = fire;

        yield return new WaitForSeconds(1f/*TODO: Add spawn animation time here*/);
        try{enemyHolder.GetComponent<Rigidbody2D>().velocity = firePos - randomPos / 5;}
        catch{/**/}
    }

    Vector2 RandomPos(Vector2 vector)
    {
        retryPos:
        vector.x = Random.Range(firePos.x - maxSpawnDistance, firePos.x + maxSpawnDistance);
        vector.y = Random.Range(firePos.y - maxSpawnDistance, firePos.y + maxSpawnDistance);

        if(Vector3.Distance(vector,firePos) < minSpawnDistance)
            goto retryPos;
        
        return vector;
    }

    void SpawnLog()
    {
        randomPos = RandomPos(randomPos);

        var logHolder = Instantiate(logPrefab, randomPos, Quaternion.identity);
        logs.Add(logHolder);
    }
}
