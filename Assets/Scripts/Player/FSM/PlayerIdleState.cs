using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.GroundMoveCtrl.Freeze();

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
        player.FlipCtrl.onHorizontalInput();



        // idle => ladder
        if (player.input.Xinput == 0 && player.ladderCheck && player.currentInteractingSpear != null && player.ladderRemountCoolDownTimer.TimeUp())
        {
            player.stateMachine.ChangeState(player.ladderMoveState);
            return true;
        }

        // stand on spear
        if (player.OnFlyableCtrl.OnFlyingPlatform)
        {
            player.OnFlyableCtrl.Still();
        }

        // idle => Roll
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {

            if (player.canDash)
            {
                stateMachine.ChangeState(player.dashState);
                return true;
            }
        }
        // idle => Jump
        if ((input.Jump || input.isJumpBuffered) && player.jumpable)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }
        // Ground => Air
        if (!player.LevelCollisionCtrl.IsGroundDetected() && player.rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        // idle => move
        if (input.Xinput != 0)
        {
            stateMachine.ChangeState(player.moveState);
            return true;
        }

        return false;


    }

}