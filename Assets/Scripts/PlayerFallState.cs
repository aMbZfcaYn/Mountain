using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState: PlayerState
{
    public PlayerFallState ( Player _player,PlayerStateMachine _stateMachine,string _animBoolName ) : base( _player,_stateMachine,_animBoolName )
    {

    }

    public override void Enter ()
    {
        base.Enter();
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

        if( player.IsGroundDetected() )
            stateMachine.ChangeState( player.idleState );
    }
}