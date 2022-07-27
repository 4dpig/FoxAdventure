using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // 为了实现二段跳功能，记录玩家连续的跳跃次数
    private int continuousJumpTimes;

    public int GetContinuousJumpTimes()
    {
        return continuousJumpTimes;
    }

    public void PlusContinuousJumpTimes()
    {
        continuousJumpTimes++;
    }
    
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // 回到地面上后，重置连续跳跃次数
            continuousJumpTimes = 0;
        }
    }
    
}
