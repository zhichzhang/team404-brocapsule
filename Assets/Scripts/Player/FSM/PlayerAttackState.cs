using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isAttackBuffered = false;
        if (player.weapon0 == null)
        {
            //if there is not weapon, nothing happen;
            return;
        }
        if (!player.canComboAttack)
        {
            player.WeaponCtrl.Attack(new AttackInfo(0b00));
            return;
        }
        if (input.Yinput >0 || input.isUpBuffered)
        {
            input.isUpBuffered = false;
            player.WeaponCtrl.Attack(new AttackInfo(0b01));
            if(ExternalDataManager.instance != null)
            {
                ExternalDataManager.instance.PlayerAttack();
            }
            return;
        }
        if(input.Yinput < 0 || input.isDownBuffered)
        {
            input.isDownBuffered = false;
            player.WeaponCtrl.Attack(new AttackInfo(0b10));
            if (ExternalDataManager.instance != null)
            {
                ExternalDataManager.instance.PlayerAttack();
            }
            return;
        }
        player.WeaponCtrl.Attack(new AttackInfo(0b00));

        if(ExternalDataManager.instance != null)
        {
            ExternalDataManager.instance.PlayerAttack();
        }

        return;
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
        stateMachine.ChangeState(player.fallState);
        return true;
        //return false;
    }
}
