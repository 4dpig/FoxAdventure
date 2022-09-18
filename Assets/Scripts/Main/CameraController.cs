using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform farBackground;
    public Transform middleBackground;

    public float minHeight, maxHeight;

    // 记录相机上一次的横纵坐标位置
    private float lastPosX, lastPosY;
    
    // Start is called before the first frame update
    private void Start()
    {
        // 一开始就把相机的x、y坐标位置设为跟玩家的位置一致
        transform.position = new Vector3
        (
            target.position.x, 
            Mathf.Clamp(target.position.y, minHeight, maxHeight), 
            transform.position.z
        );
        
        lastPosX = transform.position.x;
        lastPosY = transform.position.y;
    }

    private void Update()
    {
        // 相机的x坐标始终设置为与玩家一致，y坐标亦跟随玩家，但同时控制在范围之内
        transform.position = new Vector3
        (
            target.position.x, 
            Mathf.Clamp(target.position.y, minHeight, maxHeight), 
            transform.position.z
        );
        
        // 计算相机的移动距离
        float moveDistanceX = transform.position.x - lastPosX;
        float moveDistanceY = transform.position.y - lastPosY;

        // 远背景每次x移动的距离 = 相机的x移动距离，y移动距离 = 0.8 * 相机的y移动距离
        farBackground.position += new Vector3(moveDistanceX, moveDistanceY * 0.8f, 0f);
        
        // 中背景每次移动的距离 = 0.5 * 相机的移动距离
        middleBackground.position += new Vector3(moveDistanceX * 0.5f, moveDistanceY * 0.5f, 0f);
        
        // 更新lastPosX、lastPosY
        lastPosX = transform.position.x;
        lastPosY = transform.position.y;
    }
}
