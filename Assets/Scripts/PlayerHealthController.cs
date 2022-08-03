using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // 单例模式，以让其他会造成伤害的物体或敌人的脚本能直接访问到PlayerHealthContrller的实例
    public static PlayerHealthController instance;

    public int heartNumber;
    private int maxHealth;
    private int currentHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer sr;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        
        // 一颗心等于2个health point
        maxHealth = heartNumber * 2;
        currentHealth = maxHealth;
        
        // 一开始将invincibleCounter设为invincibleLength，这样最开始player就不是无敌的
        invincibleCounter = invincibleLength;
    }

    // Update is called once per frame
    void Update()
    {
        // 无敌计时器没有超过无敌时间，就继续累加
        if (invincibleCounter < invincibleLength)
        {
            // 更新invincible counter
            invincibleCounter += Time.deltaTime;

            if (invincibleCounter >= invincibleLength)
            {
                // 把Player重新设为不透明
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (invincibleCounter >= invincibleLength)
        {
            // 更新heart ui
            UIController.instance.ReduceHealth(damage);
        
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                this.gameObject.SetActive(false);
            }
            
            // 更新invincible counter为0
            invincibleCounter = 0;
            // 把player设置为半透明，表示目前处于无敌状态
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        }
    }
    
}
