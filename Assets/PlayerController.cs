using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    [SerializeField] private float flashDistance;

    protected override void Start ()
    {
        base.Start();
    }


    protected override void Update ()
    {
        base.Update();

        xInput = Input.GetAxis( "Horizontal" );
        rb.velocity = new Vector2( xInput * moveSpeed,rb.velocity.y );

        if( Input.GetKeyDown( KeyCode.Space ) && isGrounded )
            rb.velocity = new Vector2( rb.velocity.x,jumpForce );

        if(Input.GetKeyDown(KeyCode.LeftShift))
            transform.position = new Vector3(transform.position.x + flashDistance * facingDir,transform.position.y);
    }
}
