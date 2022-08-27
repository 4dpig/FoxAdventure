using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float runningSpeed;
    public float crouchingSpeed;
    private float moveSpeed;
    
    public float jumpForce;
    private bool canJump;

    // 玩家踩到敌人头上时，造成的伤害值
    public int stepOnDamage;

    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;
    public CapsuleCollider2D standingCollider;
    public CapsuleCollider2D crouchingCollider;

    public GroundCheck groundCheck;

    public Transform topPoint;
    public LayerMask groundLayer;

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
        // 一开始的moveSpeed设为runningSpeed
        moveSpeed = runningSpeed;
        // 一开始默认能跳
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        float knockBackLength = PlayerHealthController.instance.knockBackLength;
        float knockBackCounter = PlayerHealthController.instance.GetKnockBackCounter();
        float knockBackForce = PlayerHealthController.instance.knockBackForce;
        
        if (knockBackCounter >= knockBackLength)
        {
            // 处理蹲下的相关问题
            dealCrouch();
            
            // 计算横向和竖向的移动速度
            float hSpeed = Input.GetAxis("Horizontal") * moveSpeed;
            float vSpeed = rb.velocity.y;

            if (canJump && groundCheck.GetContinuousJumpTimes() != 2 && Input.GetButtonDown("Jump"))
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

    private void dealCrouch()
    {
        if (groundCheck.IsGrounded())
        {
            // 切换碰撞体，设置player的移动速度、是否能跳跃、以及动画参数
            if (Input.GetButton("Vertical_Down"))
            {
                if (!anim.GetBool("isCrouching"))
                {
                    standingCollider.enabled = false;
                    crouchingCollider.enabled = true;

                    moveSpeed = crouchingSpeed;
                    canJump = false;
                    
                    anim.SetBool("isCrouching", true);
                }
            }
            else
            {
                if (anim.GetBool("isCrouching"))
                {
                    if (isThereNoGroundOnTop())
                    {
                        standingCollider.enabled = true;
                        crouchingCollider.enabled = false;

                        moveSpeed = runningSpeed;
                        canJump = true;
                        
                        anim.SetBool("isCrouching", false);
                    }
                }
            }
        }
        
    }

    private bool isThereNoGroundOnTop()
    {
        Vector2 center = new Vector2(topPoint.position.x, topPoint.position.y);
        return !Physics2D.OverlapCircle(center, 0.2f, groundLayer);
    }

    public void Reset()
    {
        // 重设朝向为右
        sr.flipX = false;
        
        // 重设速度
        ResetVelocity();
        
        // 重设player为站立状态
        moveSpeed = runningSpeed;
        canJump = true;
        standingCollider.enabled = true;
        crouchingCollider.enabled = false;

        // 重设剩余的一些动画参数
        anim.SetBool("isGrounded", true);
        anim.SetBool("isBeingKnockedBack", false);
        anim.SetBool("isCrouching", false);
    }

    public void GetExtraJumpForceWhenStepOnEnemy()
    {
        // 设置animator参数
        anim.SetFloat("vSpeed", jumpForce);

        // 设置速度
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // 更新lastSpeedDirection
        setLastSpeedDirection(rb.velocity.x, jumpForce);
    }

    public void ResetVelocity()
    {
        anim.SetFloat("absHSpeed", 0);
        anim.SetFloat("vSpeed", 0);
        rb.velocity = Vector2.zero;
    }

    public void TeleportTo(Vector3 position)
    {
        this.transform.position = position;
    }

    public bool IsFalling()
    {
        return !groundCheck.IsGrounded() && rb.velocity.y < 0;
    }
}
