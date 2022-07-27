using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Rigidbody2D rb;
    
    public GroundCheck groundCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        float vSpeed = rb.velocity.y;

        if (groundCheck.GetContinuousJumpTimes() != 2 && Input.GetButtonDown("Jump"))
        {
            groundCheck.PlusContinuousJumpTimes();
            vSpeed = jumpForce;
        }

        rb.velocity = new Vector2(hSpeed, vSpeed);
    }
}
