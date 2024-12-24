using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashState: PlayerState
{

    private float timer = 0f;
    private float gravityScale;

    public PlayerFlashState ( Player _player,PlayerStateMachine _stateMachine,string _animBoolName ) : base( _player,_stateMachine,_animBoolName )
    {

    }

    public override void Enter ()
    {
        base.Enter();
        rb.velocity = new Vector2( 0,0 );

        gravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Exit ()
    {
        base.Exit();
        rb.gravityScale = gravityScale;
    }

    public override void Update ()
    {
        base.Update();

        

        timer += Time.deltaTime;
        if( timer >= player.flashPreDelay)
            rb.position = player.targetPos;

        if( timer >= player.flashPreDelay + player.flashPostDelay )
        {
            timer = 0f;
            stateMachine.ChangeState( player.idleState );
        }
            
    }

}
