using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isJumpBuffered = false;
        player.WallMovementCtrl.Prep();
    }

    public override void Exit()
    {
        base.Exit();
        player.AirMoveCtrl.Freeze();
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }
        player.WallMovementCtrl.Bump();


        //walljump => dash
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {
            stateMachine.ChangeState(player.dashState);
            return true;
        }
        //walljump => jump
        if ((input.Jump || input.isJumpBuffered) && player.jumpable)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }
        //walljump => fall
        if (player.WallMovementCtrl.wallJumpFreezeTimer.TimeUp())
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;

    }


}
