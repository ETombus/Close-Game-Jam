using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject playerPrefab;
    FireHealthScript fireHealth;
    Vector2 randomPos, firePos;
    
    float timeInterval = 5f;
    float maxSpawnDistance = 10f;
    float minSpawnDistance = 5f;

    private List<GameObject> trees = new List<GameObject>();

    float screenMinY, screenMaxY;

    void Start()
    {
        firePos = fire.transform.position;

        fireHealth = fire.GetComponent<FireHealthScript>();

        SpawnTree();
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while(fireHealth.fireHealth >=1)
        {
            yield return new WaitForSeconds(timeInterval);

            if(trees.Count < 5)
                SpawnTree();
                
            StartCoroutine(SpawnEnemy());

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

    void SpawnTree()
    {
        randomPos = RandomPos(randomPos);

        var treeHolder = Instantiate(treePrefab, randomPos, Quaternion.identity);
        trees.Add(treeHolder);
    }

    public void CallSpawnPlayer()
    {
        StartCoroutine(SpawnPlayer());
    }

    IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(2.5f);
        var player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        player.GetComponent<PlayerHealthScript>().fire = fire;
    }
}
