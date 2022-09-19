using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    private EnemyController enemyController;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyController = gameObject.transform.parent.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyController.TakeStepOnDamage();
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            enemyController.TakeStepOnDamage();
        }
        
    }
    
}
