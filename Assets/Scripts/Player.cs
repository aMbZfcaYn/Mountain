using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{

    [Header( "Move info" )]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header( "Jump info" )]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    public float airControlRate;

    [Header( "Flash info" )]
    [SerializeField] private float flashCooldown;
    [SerializeField] private float flashCheckRadius;
    [SerializeField] private float flashFixHeight;
    private RaycastHit2D hitInfo;
    private float flashCooldownTimer = 0f;
    public Vector3 targetPos;
    public Transform flashCheck;
    public float flashPreDelay;
    public float flashPostDelay;
    public float flashDistance = 5f;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator anim;
    public Rigidbody2D rb;
    #endregion

    #region States
    public PlayerStateMachine stateMachine;

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerFlashState flashState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    #endregion 

    private void Awake ()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState( this,stateMachine,"Idle" );
        moveState = new PlayerMoveState( this,stateMachine,"Move" );
        flashState = new PlayerFlashState( this,stateMachine,"Flash" );
        jumpState = new PlayerJumpState( this,stateMachine,"Jump" );
        fallState = new PlayerFallState( this,stateMachine,"Jump" );
    }

    private void Start ()
    {
        stateMachine.Initialize( idleState );
    }

    private void Update ()
    {

        stateMachine.currentState.Update();
        Debug.Log(stateMachine.currentState);
        FlashCheck();
    }


    public void SetVelocity(float _xVelocity,float _yVelocity)
    {
        rb.velocity = new Vector2 ( _xVelocity, _yVelocity );
        FlipController( _xVelocity );
    }

    public bool IsGroundDetected () => Physics2D.Raycast( groundCheck.position,Vector2.down,groundCheckDistance,whatIsGround );

    private void FlashCheck()
    {
        flashCooldownTimer += Time.deltaTime;
        if( Input.GetKeyDown( KeyCode.LeftShift ) && flashCooldownTimer >= 0)
        {
            CheckTargetPos();

            stateMachine.ChangeState( flashState );
            flashCooldownTimer = -flashCooldown;
        }
    }

    private void CheckTargetPos ()
    {
        targetPos = new Vector3( flashCheck.position.x + flashDistance * facingDir,flashCheck.position.y );
        hitInfo = Physics2D.Raycast( flashCheck.position,new Vector3(facingDir,0,0),flashDistance,whatIsGround );

        if( hitInfo.collider != null )
        {
            float resDistance = flashDistance - hitInfo.distance;
            float wallDepth = hitInfo.collider.bounds.size.x;
            if( resDistance >= wallDepth / 2 )
                targetPos = new Vector3( hitInfo.point.x + ( wallDepth + flashCheckRadius ) * facingDir,hitInfo.point.y );
            else
                targetPos = new Vector3( hitInfo.point.x - flashCheckRadius * facingDir,hitInfo.point.y );
        }

        targetPos = new Vector3( targetPos.x,targetPos.y + flashFixHeight );
    }

    public void Flip ()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate( 0,180,0 );
    }

    public void FlipController ( float _x )
    {
        if( ( _x > 0 && !facingRight ) || ( _x < 0 && facingRight ) )
            Flip();
    }
    private void OnDrawGizmos ()
    {
        Gizmos.DrawLine( groundCheck.position,new Vector3( groundCheck.position.x,groundCheck.position.y - groundCheckDistance ) );
        Gizmos.DrawLine( new Vector3(targetPos.x - flashCheckRadius,targetPos.y) ,new Vector3( targetPos.x + flashCheckRadius,targetPos.y ) );
    }


}
