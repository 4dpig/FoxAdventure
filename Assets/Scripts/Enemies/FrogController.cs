using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrogController : EnemyController
{
    public EnemyGroundCheck groundCheck;

    public Transform leftPoint;
    public Transform rightPoint;
    private float leftBoundaryX, rightBoundaryX;
    /*
     * 记录一开始当青蛙在地面上时候的y坐标，
     * 因为跳落回到地面上后，y坐标可能会在地面上面一点，也可能陷进去地面一点，
     * 所以需要重置为一开始的y坐标
     */
    private float yPositionWhenFrogOnGround;

    public float gravity;
    public float jumpHorizontalForce;
    public float jumpVerticalForce;

    public float referenceWaitTime;
    private float randomedWaitTime;
    private float waitTimeCounter;

    private bool isJumping;
    private bool isJumpingLeft = true;

    void RandomizeWaitTime()
    {
        randomedWaitTime = Random.Range(referenceWaitTime * 0.5f, referenceWaitTime * 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        leftBoundaryX = leftPoint.position.x;
        rightBoundaryX = rightPoint.position.x;
        yPositionWhenFrogOnGround = this.transform.position.y;
        
        // 随机生成距离下一次跳跃的等待时间
        RandomizeWaitTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJumping)
        {
            waitTimeCounter += Time.deltaTime;
            if (waitTimeCounter >= randomedWaitTime)
            {
                // 进行跳跃前的相关设置，并开始跳跃
                StartJumping();
            }
        }
        else
        {
            if (groundCheck.IsGrounded())
            {
                // 已经回到地面上，结束本次跳跃
                EndJumping();
            }
            else
            {
                // 持续更新vSpeed
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravity * Time.deltaTime);
                anim.SetFloat("vSpeed", rb.velocity.y);
            }
        }
    }
    
    void StartJumping()
    {
        // 如果到达边界，则调转跳动的方向
        if (this.transform.position.x <= leftBoundaryX)
        {
            isJumpingLeft = false;
            sr.flipX = true;
        }
        else if(this.transform.position.x >= rightBoundaryX)
        {
            isJumpingLeft = true;
            sr.flipX = false;
        }
        
        // 设置速度
        if (isJumpingLeft)
        {
            rb.velocity = new Vector2(-jumpHorizontalForce, jumpVerticalForce);
        }
        else
        {
            rb.velocity = new Vector2(jumpHorizontalForce, jumpVerticalForce);
        }
        
        // isJumping设为true
        isJumping = true;
        // 离开地面，手动把isGrounded设为false
        groundCheck.OffTheGround();
        // 设置动画参数
        anim.SetBool("isGrounded", false);
        anim.SetFloat("vSpeed", jumpVerticalForce);
    }

    void EndJumping()
    {
        // 重设相关参数
        isJumping = false;
        waitTimeCounter = 0f;
        RandomizeWaitTime();
        
        rb.velocity = Vector2.zero;
        this.transform.position = new Vector3(this.transform.position.x, yPositionWhenFrogOnGround, 0f);
                
        anim.SetBool("isGrounded", true);
        anim.SetFloat("vSpeed", 0f);
    }
}
