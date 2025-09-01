using UnityEngine;

public class Timer
{
    public float timer;
    public bool triggerOnTimeUp;
    public string ChannelName;
    public void Set(float duration)
    {
        timer = duration;
        triggerOnTimeUp = false;
    }
    public void Set(float duration, bool triggerOnTimeUp, string ChannelName) 
    {
        timer = duration;
        triggerOnTimeUp = true;
        this.ChannelName = ChannelName;
    }

    public void CountDown()
    {
        timer = Mathf.Max(timer - Time.deltaTime, 0f);
    }

    public bool TimeUp()
    {
        return timer == 0f;
    }

    public void Alarm()
    {
        EventManager.TriggerEvent(ChannelName + "TimeUp", this);
    }
}
