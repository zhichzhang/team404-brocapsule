using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        player.GroundMoveCtrl.onHorizontalInput(input.Xinput);
        player.FlipCtrl.onHorizontalInput();

        // move => Roll
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {

            if (player.canDash)
            {
                stateMachine.ChangeState(player.rollState);
                return true;
            }
        }
        // move => jump
        if ((input.Jump || input.isJumpBuffered) && player.jumpable)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }
        // move => Air
        if (!player.LevelCollisionCtrl.IsGroundDetected() && player.rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        // move => idle
        if (input.Xinput == 0)
        {
            stateMachine.ChangeState(player.idleState);
            return true;
        }
        return false;
    }


    public override void LateUpdate()
    {
        base.LateUpdate();

    }
}
