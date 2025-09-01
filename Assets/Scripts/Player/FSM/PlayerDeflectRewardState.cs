using UnityEngine;

public class PlayerDeflectRewardState : PlayerState
{
    public PlayerDeflectRewardState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.DeflectCtrl.Bump();
        player.ManaCtrl.AddMana(1);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.FlipCtrl.onHorizontalInput();
        player.AirMoveCtrl.OnHorizontalInput(input.Xinput);


        //deflect jump => dash
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {
            stateMachine.ChangeState(player.dashState);
            return true;
        }
        //deflect jump => jump
        if ((input.Jump || input.isJumpBuffered) && player.jumpable)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }
        //deflect jump => fall
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }
}
