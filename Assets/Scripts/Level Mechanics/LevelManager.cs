using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float timeToWaitBeforeRespawn;
    private Vector3 levelBeginPosition;
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

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }
    
    private IEnumerator RespawnCoroutine()
    {
        // 先禁用Player，然后等待指定时间后再进行复活相关的操作
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeToWaitBeforeRespawn);

        // 重新启用Player
        PlayerController.instance.gameObject.SetActive(true);
        // 重置健康值、健康UI
        PlayerHealthController.instance.Reset();
        UIController.instance.Reset();
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
