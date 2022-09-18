using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyCheck : MonoBehaviour
{
    private EnemyController enemyController;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyController = gameObject.transform.parent.GetComponent<EnemyController>();
        Debug.Log(enemyController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyController.DamagePlayer();
        }
        else if (other.CompareTag("PlayerFeet"))
        {
            enemyController.TakeStepOnDamage();
        }
    }
    
}
