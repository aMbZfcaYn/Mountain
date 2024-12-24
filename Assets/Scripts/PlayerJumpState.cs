using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState: PlayerState
{
    public PlayerJumpState ( Player _player,PlayerStateMachine _stateMachine,string _animBoolName ) : base( _player,_stateMachine,_animBoolName )
    {

    }

    public override void Enter ()
    {
        base.Enter();

        player.SetVelocity( 0,player.jumpForce );
    }

    public override void Exit ()
    {
        base.Exit();
    }

    public override void Update ()
    {
        base.Update();

        if(xInput != 0)
            player.SetVelocity( xInput * player.moveSpeed * player.airControlRate,rb.velocity.y );

        if( rb.velocity.y < 0 )
            stateMachine.ChangeState( player.fallState );
    }
}
