using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameTest : MonoBehaviour
{
    public int frameCounter = 0;
    public int fixedFrameCounter = 0;
    public Rigidbody2D rb;
    private float lastPositionX = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        frameCounter++;
        Debug.Log($"第{frameCounter}帧" +
                  $"\t当前帧开始时间（即从游戏开始到现在的累计时间）：{Time.time}" +
                  $"\t物体当前x坐标：{this.transform.position.x}" +
                  $"\n帧间隔（当前帧开始时间-上一帧开始时间）：{Time.deltaTime}" +
                  $"\t移动距离：{this.transform.position.x - lastPositionX}");

                  lastPositionX = this.transform.position.x;
        rb.velocity = new Vector2(frameCounter, 0f);
        */
        
        this.transform.Translate(new Vector3(0.01f, 0f, 0f), Space.World);
    }

    private void FixedUpdate()
    {
        /*
        fixedFrameCounter++;
        Debug.Log($"当前时间：{Time.time}\t第{fixedFrameCounter}次FixedUpdate调用！");
        */
        
        /*
        Debug.Log($"物体当前x坐标：{this.transform.position.x}");
        rb.velocity = new Vector2(fixedFrameCounter, 0f);
        */
    }
}
