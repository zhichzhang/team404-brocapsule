using UnityEngine;

public class HealthController
{
    public Player player;
    private bool levelTransitionLock = false;   

    public HealthController(Player _player)
    {
        this.player = _player;
    }
    public void LoseHealth(int _health)
    {
        // TODO: update date health lost mechanism
        // TODO cancelled, I updated health using playerINFO
        if (player.Health > _health) 
        { 
            player.Health -= _health;
            //player.playerEmbeddedUI.decreaseHealth();
        }
        else
        {
            //if (levelTransitionLock) return;
            //levelTransitionLock = true;
            //player.Health -= _health;
            //LevelManager.instance.StartTransitionToRestartLevel();
            //reset to Last Good position
            if (ExternalDataManager.instance != null)
            {
                ExternalDataManager.instance.PlayerDeaths();
            }
            ResetToCheckPoint();
            FadeBlackOut.Instance.BlackOut();
        }
    }
    public void GainHealth(int _health)
    {
        player.Health = Mathf.Min(player.Health + _health, player.MaxHealth);
    }

    public void ResetToCheckPoint()
    {
        player.transform.position = new Vector3(player.LastGoodPosition.x, player.LastGoodPosition.y, player.LastGoodPosition.z);
        player.Health = 9;
        player.Mana = 0;
    }
}
