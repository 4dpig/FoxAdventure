using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;
    public GameObject deathEffectPrefab;

    // 敌人的健康值
    public int health;
    // 敌人对玩家造成的伤害值
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnEnemyDeadAnimationFinished()
    {
        Destroy(this.gameObject);
    }

    public virtual void TakeStepOnDamage()
    {
        if (!PlayerHealthController.instance.IsInvincible())
        {
            Debug.Log("敌人受到伤害");
            // 玩家获取额外的一次跳跃力
            PlayerController.instance.GetExtraJumpForceWhenStepOnEnemy();
        
            // 当玩家踩到敌人的头顶时，敌人承受一次伤害
            health -= PlayerController.instance.stepOnDamage;

            if (health <= 0)
            {
                // 生成死亡效果
                Instantiate(deathEffectPrefab, this.transform.position, this.transform.rotation);
                // 销毁这个gameobject
                Destroy(this.gameObject);
            } 
        }
    }

    public virtual void DamagePlayer()
    {
        if (!PlayerHealthController.instance.IsInvincible())
        {
            Debug.Log("玩家受到伤害");
            PlayerHealthController.instance.TakeDamage(damage);
        }
    }
    
}
