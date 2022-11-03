using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayerFixer : MonoBehaviour
{
    private GameObject playerTrans;
    private SpriteRenderer spritRend;

    public int orderAddition = 0;
    private void Start()
    {
        spritRend = GetComponent<SpriteRenderer>();
        playerTrans = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        
        if(playerTrans.transform.position.y > transform.position.y)
        {
            spritRend.sortingOrder = 10 + orderAddition;
        }
        else
        {
            spritRend.sortingOrder = 4 + orderAddition;
        }
    }


}
