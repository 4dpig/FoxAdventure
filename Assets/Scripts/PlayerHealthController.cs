using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // 单例模式，以让其他会造成伤害的物体或敌人的脚本能直接访问到PlayerHealthContrller的实例
    public static PlayerHealthController instance;

    public int heartNumber;
    private int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        instance = this;
        
        // 一颗心等于2个health point
        maxHealth = heartNumber * 2;
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        // 更新heart ui
        UIController.instance.ReduceHealth(damage);
        
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
