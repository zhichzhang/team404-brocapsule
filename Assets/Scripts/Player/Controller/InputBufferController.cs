using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class InputBufferController
{
    public Player player;

    public InputBufferController(Player player)
    {
        this.player = player;
    }

    public void Initialize(Player player)
    {
        this.player = player;
    }

    public void SetBufferOnInput()
    {
        if (player.input.Jump)
        {
            player.input.SetJumpBufferTimer();
        }
        if (player.input.Roll)
        {
            player.input.SetRollBufferTimer();
        }
        if (player.input.Attack)
        {
            player.input.SetAttackBufferTimer();
        }
        if (player.input.Skill)
        {
            player.input.SetSkillGrabBufferTimer();
        }
        if (player.input.Deflect)
        {
            player.input.SetDeflectBufferTimer();
        }
        if (player.input.Grab)
        {
            player.input.SetGrabBufferTimer();
        }
        if (player.input.SwtichWeapon)
        {
            player.input.SetSwitchWeaponBufferTimer();
        }
        if(player.input.Yinput > 0)
        {
            player.input.SetUpBufferTimer();
        }
        if (player.input.Yinput < 0)
        {
            player.input.SetDownBufferTimer();
        }
        if (player.input.Interact)
        {
            player.input.SetInteractBufferTimer();
        }
    }
}
