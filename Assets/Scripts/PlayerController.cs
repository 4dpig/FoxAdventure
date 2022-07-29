using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;

    public GroundCheck groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }
}
