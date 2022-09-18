using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    private bool isCollected;
    public int healAmount;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected && !PlayerHealthController.instance.IsFullHealth())
        {
            // 防止同一个Cherry被拾取多次
            isCollected = true;
            // 播放拾取音效
            AudioManager.instance.PlaySoundEffect(AudioManager.SoundEffectName.Pickup_Health);
            // 播放拾取动画
            anim.SetBool("isCollected", true);
        }
    }
    
    private void onCollectAnimationFinished()
    {
        // UI更新
        PlayerHealthController.instance.Heal(healAmount);
        // 销毁这个Cherry（下一帧才会生效）
        Destroy(this.gameObject);
    }
}
