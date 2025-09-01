using UnityEngine;

public class ManaController
{
    public Player player;

    public ManaController(Player _player)
    {
        this.player = _player;
    }
    public void Initialize(int initialMana, int maxMana)
    {
        player.Mana = initialMana;
        player.MaxMana = maxMana;
    }
    public void AddMana(int mana)
    {
        //TODO: update mana UI change
        player.Mana = Mathf.Min(player.Mana + mana, player.MaxMana);
        //player.playerEmbeddedUI.increaseMana();
    }
    //return true if mana is enough, and cost mana
    //return false if mana is not enough
    public bool CostMana(int mana)
    {
        //TODO: update mana UI change
        if (player.Mana >= mana)
        {
            //player.playerEmbeddedUI.decreaseMana();  
            player.Mana -= mana;
            return true;
        }
        return false;
    }
}
