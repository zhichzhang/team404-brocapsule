using UnityEngine;

public class ManaLockTriggerDisabler : TriggerDisablerPlus
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override bool CertainCondition()
    {
        if (PlayerInfo.instance.player.Mana >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
