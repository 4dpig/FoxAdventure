using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float userInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveSpeed * userInput, rb.velocity.y);
    }
}
