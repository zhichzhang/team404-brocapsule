using System.Collections.Generic;
using UnityEngine;

// TODO: replace with Unity built in Timer
public class TimerCountDownController
{
    HashSet<Timer> timers;
    Player player;

    public TimerCountDownController(Player player)
    {
        this.player = player;
        timers = new HashSet<Timer>();
    }

    public void register(Timer timer)
    {
        timers.Add(timer);
    }
    public void unregister(Timer timer)
    {
        timers.Remove(timer);
    }
    public void Update()
    {
        foreach (Timer timer in timers)
        {
            timer.CountDown();
        }
    }
}
