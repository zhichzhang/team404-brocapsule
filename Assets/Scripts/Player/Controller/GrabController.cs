using UnityEngine;

public class GrabController
{
    Player player;
    public Timer timer;
    public Vector3 position;

    public GrabController(Player player)
    {
        this.player = player;
        timer = new Timer();
        player.grabTimer = timer;
        player.TimerCountDownCtrl.register(timer);
    }

    public void Grab()
    {
        player.grabBox.SetActive(true);
        timer.Set(player.grabDuration);
    }

    public void GrabOver()
    {
        player.grabBox.SetActive(false);
    }

}
