using UnityEngine;

public class PlayerGrabState : PlayerState
{
    public PlayerGrabState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        input.isGrabBuffered = false;
        player.GrabCtrl.Grab();

    }

    public override void Exit()
    {
        base.Exit();
        player.GrabCtrl.GrabOver();
    }


    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        if (player.GrabCtrl.timer.TimeUp())
        {
            player.stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }
}
