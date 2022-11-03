using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [SerializeField] GameObject logPrefab;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(logPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
