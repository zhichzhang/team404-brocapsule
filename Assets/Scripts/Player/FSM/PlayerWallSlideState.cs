using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.WallMovementCtrl.Slide();


        // wallslide => fall
        if ( (!player.LevelCollisionCtrl.IsWallDetected()) || player.facingDir * input.Xinput <= 0)
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        // wallslide => idle
        if (player.LevelCollisionCtrl.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return true;
        }

        return false;
    }
}
