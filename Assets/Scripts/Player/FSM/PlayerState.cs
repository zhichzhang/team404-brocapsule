using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    protected PlayerInput input;
    public string animBoolName;
    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //player.anim.SetBool(animBoolName, true);
        
        //Have a shortcut rb,input pointer instead of using player.rb, player.input save typing
        rb = player.rb;
        input = player.input;
    }

    public virtual void Exit()
    {
        //player.anim.SetBool(animBoolName, false);
    }

    public virtual bool Update()
    {

        //any => deflect
        if ((input.Deflect || input.isDeflectBuffered) && player.deflectCoolDownTimer.TimeUp() )
        {
            stateMachine.ChangeState(player.deflectState);
            return true;
        }
        // any=> Grab
        if (input.Grab || input.isGrabBuffered)
        {
            stateMachine.ChangeState(player.grabState);
            return true;
        }
        // any => Attack
        if(input.Attack || input.isAttackBuffered)
        {
            stateMachine.ChangeState(player.attackState);
            return true;

        }
        return false;
    }

    public virtual void LateUpdate()
    {

    }

    public virtual void AnimationOverEvent()
    {

    }

}
