using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.Windows;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isJumpBuffered = false;
        player.JumpCtrl.Bump();
        if (SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddJumps();
        }
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


        // jump => ladder
        if (player.input.Xinput == 0 && player.ladderCheck && player.currentInteractingSpear != null && player.ladderRemountCoolDownTimer.TimeUp())
        {
            player.stateMachine.ChangeState(player.ladderMoveState);
            return true;
        }

        //jump => dash
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {
            if (player.canDash)
            {
                stateMachine.ChangeState(player.dashState);
                return true;
            }
        }
        //jump => jump
        if ((input.Jump || input.isJumpBuffered) && player.jumpable)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }
        //jump => fall
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }

        return false;

    }


}
