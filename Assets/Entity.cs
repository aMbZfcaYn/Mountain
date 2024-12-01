using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    [Header("Moving info")]
    [SerializeField] protected float moveSpeed;
    protected float xInput;

    [Header( "Jumping info" )]
    [SerializeField] protected float jumpForce;


    [Header( "Layers" )]
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Collision Check info")]
    [SerializeField] protected bool isGrounded = true;
    [SerializeField] protected float groundCheckDistance;

    protected float facingDir = 1;
    protected bool isFacingRight = true;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    protected virtual void Update()
    {
        FlipController();
        CollisionChecks();
    }


    protected virtual void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast( transform.position,Vector2.down,groundCheckDistance,whatIsGround );

    }
    protected virtual void FlipController()
    {
        if( (facingDir == 1 && rb.velocity.x < 0) || (facingDir == -1  && rb.velocity.x > 0) )
            Flip();

    }

    protected virtual void Flip ()
    {
        transform.Rotate( 0,180,0 );
        facingDir *= -1;
        isFacingRight = !isFacingRight;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine( transform.position,new Vector3( transform.position.x,transform.position.y - groundCheckDistance ) );

    }

}
