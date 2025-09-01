using UnityEngine;

public class LanceDisappearState : LanceState
{
    public LanceDisappearState(LanceStateMachine stateMachine, Lance lance) : base(stateMachine, lance)
    {
    }

    public override void Enter()
    {
        base.Enter();
        lance.Disappear();
        //TODO: make this cooldown timer edible in editor
        lance.timer = 1f;
    }

    public override void Exit()
    {
        base.Exit();
        lance.Appear();
    }

    public override void Update()
    {
        base.Update();
        if(lance.timer <= 0f)
        {
            stateMachine.ChangeState(lance.idleState);
        }
    }
}
