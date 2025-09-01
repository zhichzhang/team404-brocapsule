using Unity.VisualScripting;
using UnityEngine;

public class WeaponController
{
    Player player;


    public WeaponController(Player player)
    {
        this.player = player;
    }

    public void UseGrabSkill()
    {

    }
    public void Attack(AttackInfo ai)
    {
        if (player.weapon0 == null)
        {
            return;
        }
        player.weapon0.attack(ai);
    }
    public void Skill(AttackInfo ai)
    {
        
    }
    public void switchWP1()
    {
        player.input.isSwitchWeaponBuffered = false;

        if(player.weapon1 == null)
        {
            return;
        }
        PlayerWeapon temp = player.weapon0;
        player.weapon0.UnEquip();
        player.weapon0 = player.weapon1;
        player.weapon1 = temp;
        player.weapon0.Equip();
    }

    public void ActiveWP(int id)
    {
        PlayerWeapon wp = player.weaponDictionary.SearchWeapon(id);
        wp.ActivateWeapon();
    }

    public void DeactiveWP(int id)
    {
        PlayerWeapon wp = player.weaponDictionary.SearchWeapon(id);
        wp.DeactivateWeapon();
    }
    //TODO: used in equipwp
    public void EquipWP(int weaponId, int slotId)
    {
        PlayerWeapon wp = player.weaponDictionary.SearchWeapon(weaponId);
        if (!wp.gameObject.activeSelf)
        {
            //If the weapon is not active, abort
            Debug.Log("Set Weapon " + weaponId + " to active first");
            return;
        }
        switch (slotId)
        {
            case 0:
                player.weapon0 = wp;
                player.weapon0.Equip();
                break;
            case 1:
                player.weapon1 = wp;
                break;

        }

    }

    public void checkSwtichPressed()
    {
        if(player.input.SwtichWeapon || player.input.isSwitchWeaponBuffered)
        {
            switchWP1();
        }
    }

}
