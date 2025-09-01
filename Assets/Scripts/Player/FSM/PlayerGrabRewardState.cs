using Unity.VisualScripting;
using UnityEngine;

public class PlayerGrabRewardState : PlayerState
{
    public PlayerGrabRewardState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        int eventId = player.trigger.GetComponent<GrabEvent>().GetEventID();
        //Invoke event based on ID
        //TODO: implement
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
        if(base.Update()){
            return true;   
        }
        stateMachine.ChangeState(player.idleState);
        return true;
        //return false;
    }
}
