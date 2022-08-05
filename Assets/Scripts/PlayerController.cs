using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public float moveSpeed;
    public float jumpForce;

    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;

    public GroundCheck groundCheck;

    // 记录上一次由玩家输入所计算出的速度的方向，1为正方向，0为无速度，-1为负方向
    private int lastHSpeedDirection, lastVSpeedDirection;
    private void setLastSpeedDirection(float hSpeed, float vSpeed)
    {
        if (hSpeed > 0)
        {
            lastHSpeedDirection = 1;
        }
        else if (hSpeed < 0)
        {
            lastHSpeedDirection = -1;
        }
        else
        {
            lastHSpeedDirection = 0;
        }
        
        if (vSpeed > 0)
        {
            lastVSpeedDirection = 1;
        }
        else if (vSpeed < 0)
        {
            lastVSpeedDirection = -1;
        }
        else
        {
            lastVSpeedDirection = 0;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float knockBackLength = PlayerHealthController.instance.knockBackLength;
        float knockBackCounter = PlayerHealthController.instance.GetKnockBackCounter();
        float knockBackForce = PlayerHealthController.instance.knockBackForce;
        
        if (knockBackCounter >= knockBackLength)
        {
            // 计算横向和竖向的移动速度
            float hSpeed = Input.GetAxis("Horizontal") * moveSpeed;
            float vSpeed = rb.velocity.y;

            if (groundCheck.GetContinuousJumpTimes() != 2 && Input.GetButtonDown("Jump"))
            {
                groundCheck.PlusContinuousJumpTimes();
                vSpeed = jumpForce;
            }

            // 设置翻转
            if (hSpeed < 0)
            {
                sr.flipX = true;
            }
            else if(hSpeed > 0)
            {
                sr.flipX = false;
            } 
        
            // 设置animator参数
            anim.SetFloat("absHSpeed", Mathf.Abs(hSpeed));
            anim.SetFloat("vSpeed", vSpeed);
            anim.SetBool("isGrounded", groundCheck.IsGrounded());
        
            // 设置速度
            rb.velocity = new Vector2(hSpeed, vSpeed);
            // 更新lastSpeedDirection
            setLastSpeedDirection(hSpeed, vSpeed);
        }
        else
        {
            // 击退时，速度大小等于knockBackForce，方向与上一次由玩家输入所产生的速度相反。
            rb.velocity = new Vector2(knockBackForce * -lastHSpeedDirection, knockBackForce * -lastVSpeedDirection);
        }
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
    }
}
