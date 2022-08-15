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

    // 无敌时间相关参数
    public float invincibleLength;
    private float invincibleCounter;
    
    // 受到伤害时的击退效果参数
    // 击退效果持续时长 要比 无敌时间持续时长 要短，并在击退过程中显示player_hurt动画
    public float knockBackLength;
    public float knockBackForce;
    private float knockBackCounter;
    public float GetKnockBackCounter() { return knockBackCounter; }

    private Transform player;
    private SpriteRenderer sr;
    private Animator anim;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        
        // 一颗心等于2个health point
        maxHealth = heartNumber * 2;
        currentHealth = maxHealth;
        
        // 一开始将invincibleCounter设为invincibleLength，这样最开始player就不是无敌的
        invincibleCounter = invincibleLength;
        
        // 一开始就把knockBackCounter设为knockBackLength，因为游戏一开始玩家是非无敌状态。
        knockBackCounter = knockBackLength;
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

            if (knockBackCounter < knockBackLength)
            {
                // 更新knockBack coutner
                knockBackCounter += Time.deltaTime;

                if (knockBackCounter >= knockBackLength)
                {
                    // 速度重置为0
                    PlayerController.instance.ResetVelocity();
                    
                    // 重新设回原来的动画
                    anim.SetBool("isBeingKnockedBack", false);
                }
            }
        }
    }

    public void Heal(int healAmount)
    {
        // 更新heart ui
        HeartUIController.instance.IncreaseHealth(healAmount);
        
        // 更新当前生命值
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (invincibleCounter >= invincibleLength)
        {
            // 更新heart ui
            HeartUIController.instance.ReduceHealth(damage);
        
            currentHealth -= damage;
            if (currentHealth > 0)
            {
                // 更新invincible counter为0
                invincibleCounter = 0;
                // 把player设置为半透明，表示目前处于无敌状态
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
                
                // 重设knockBackCounter
                knockBackCounter = 0;
                // knock back期间，设为player_hurt动画
                anim.SetBool("isBeingKnockedBack", true);
            }
            else
            { 
                LevelManager.instance.RespawnPlayer(false);
            }
        }
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        // 重置UI
        HeartUIController.instance.Reset();
    }
    
    public bool IsFullHealth()
    {
        return currentHealth == maxHealth;
    }
}
