using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    // 玩家死亡和重生相关的字段
    public PlayerDeathEffect playerDeathEffect;
    public float timeToWaitBeforeRespawn;
    private Vector3 levelBeginPosition;
    
    // 记录游戏状态（暂停 / 运行中）
    private bool isGameRunning = true;
    public bool IsGameRunning
    {
        get { return isGameRunning; }
        set { isGameRunning = value;  }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 关卡开始位置设为玩家一开始的位置
        levelBeginPosition = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer(bool dieOutsideMap)
    {
        StartCoroutine(RespawnCoroutine(dieOutsideMap));
    }
    
    private IEnumerator RespawnCoroutine(bool dieOutsideMap)
    {
        // 先禁用Player
        PlayerController.instance.gameObject.SetActive(false);
        // 播放死亡音效
        AudioManager.instance.PlaySoundEffect(AudioManager.SoundEffectName.Player_Death);
        
        // 播放死亡效果
        playerDeathEffect.gameObject.SetActive(true);
        playerDeathEffect.StartDeathEffect(dieOutsideMap);
        while (!playerDeathEffect.IsFinished())
        {
            yield return null;
        }
        // 播放结束，禁用deathEffect。
        playerDeathEffect.gameObject.SetActive(false);
        
        // 等待指定时间后复活玩家
        yield return new WaitForSeconds(timeToWaitBeforeRespawn);
        
        // 重新启用Player
        PlayerController.instance.gameObject.SetActive(true);
        
        // 重设Player
        PlayerController.instance.Reset();
        // 重置健康
        PlayerHealthController.instance.Reset();
        // 玩家传送到重生点
        CheckPointManager currentCheckPoint = CheckPointManager.currentCheckPoint;
        if (currentCheckPoint != null)
        {
            PlayerController.instance.TeleportTo(CheckPointManager.currentCheckPoint.RespwanPosition());
        }
        else
        {
            PlayerController.instance.TeleportTo(levelBeginPosition);
        }
    }
}
