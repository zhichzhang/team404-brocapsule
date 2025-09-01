using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        player.FlipCtrl.onHorizontalInput();
        player.AirMoveCtrl.OnHorizontalInput(input.Xinput);


        // fall => ladder
        if (player.input.Xinput == 0 && player.ladderCheck && player.currentInteractingSpear != null && player.ladderRemountCoolDownTimer.TimeUp())
        {
            player.stateMachine.ChangeState(player.ladderMoveState);
            return true;
        }

        //fall => dash
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {
            if (player.canDash)
            {
                stateMachine.ChangeState(player.dashState);
                return true;
            }
        }
        // fall => jump
        if ((input.Jump || input.isJumpBuffered) && player.jumpable)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }
        // fall => idle
        if (player.LevelCollisionCtrl.IsGroundDetected())
        {
            player.JumpCtrl.ResetCounter(2);
            stateMachine.ChangeState(player.idleState);
            return true;
        }
        return false;
    }

    
}
