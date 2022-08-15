using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private SpriteRenderer sr;
    private Vector3 respawnPosition;
    private bool isChecked;
    private bool isInCheckPointArea;
    public static CheckPointManager currentCheckPoint;

    public Sprite checkPoint_off;
    public Sprite checkPoint_on;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        // 将checkPoint的顶部中点设为重生点坐标
        respawnPosition = new Vector3(sr.bounds.center.x, sr.bounds.max.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCheckPointArea && !isChecked && Input.GetButtonDown("CheckPoint"))
        {
            // 把之前已经激活的那个checkPoint给关闭掉（如果有的话）
            if (currentCheckPoint != null)
            {
                currentCheckPoint.sr.sprite = checkPoint_off;
                currentCheckPoint.isChecked = false;
            }

            // 更换sprite图像
            sr.sprite = checkPoint_on;
            // 更改check状态
            isChecked = true;
            // 更改currentCheckPoint为当前这个checkPoint
            currentCheckPoint = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInCheckPointArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInCheckPointArea = false;
        }
    }

    public Vector3 RespwanPosition() { return respawnPosition; }
    
    public static void Reset()
    {
        // 场景切换时，重置checkPoint
        currentCheckPoint.sr.sprite = currentCheckPoint.checkPoint_off;
        currentCheckPoint.isChecked = false;
        currentCheckPoint = null;
    }
}
