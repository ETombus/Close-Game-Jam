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

            SpawnEnemy();

            if(logs.Count !> 5)
                SpawnLog();
            Debug.Log("logs = "+logs.Count);

            timeInterval *= .95f;
            Debug.Log(timeInterval);
        }
    }

    IEnumerator SpawnEnemy()
    {
        RandomPos(randomPos);

        var enemyHolder = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        enemyHolder.GetComponent<EnemyController>().fire = fire;

        yield return new WaitForSeconds(1f/*TODO: Add spawn animation time here*/);
        try{enemyHolder.GetComponent<Rigidbody2D>().velocity = firePos - randomPos / 5;}
        catch{/**/}
    }

    Vector2 RandomPos(Vector2 vector)
    {
        vector.x = Random.Range(firePos.x + minSpawnDistance, firePos.x + maxSpawnDistance);
        vector.y = Random.Range(firePos.y + minSpawnDistance, firePos.y + maxSpawnDistance);

        if(Random.Range(0,2) == 1)
            vector.x *= -1f;

        if(Random.Range(0,2) == 1)
            vector.y *= -1f;
        
        return vector;
    }

    void SpawnLog()
    {
        randomPos = RandomPos(randomPos);

        var logHolder = Instantiate(logPrefab, randomPos, Quaternion.identity);
        logs.Add(logHolder);
    }
}
