using System;
using UnityEngine;
using UnityEngine.Windows;

public class AirMoveController
{
    public Player player;

    public AirMoveController(Player player)
    {
        this.player = player;
    }
    
    public void OnHorizontalInput(float Xinput)
    {
        player.rb.linearVelocityX = Xinput * player.HorizontalSpeedFalling;
    }

    public void Freeze()
    {
        player.rb.linearVelocity = new Vector2(0,0);
        player.rb.gravityScale = 0;
    }
    public void UnFreeze()
    {
        player.rb.gravityScale = player.gravityScale;
    }
}
