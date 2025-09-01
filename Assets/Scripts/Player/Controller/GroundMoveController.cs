using UnityEngine;

public class GroundMoveController
{
    
    public Player player;

    public GroundMoveController(Player player)
    {
        this.player = player;
    }

    public void onHorizontalInput(float Xinput)
    {
        //SetSpeedWithAcceleration(Xinput);



        // stand on spear
        if (player.transform.parent != null)
        {
            Vector2 parentSpeed = player.transform.parent.GetComponent<Rigidbody2D>().linearVelocity;
            player.rb.linearVelocity = new Vector2(Xinput * player.HorizontalSpeedGround, player.rb.linearVelocity.y) + parentSpeed;
        }
        else
        {
            player.rb.linearVelocity = new Vector2(Xinput * player.HorizontalSpeedGround, player.rb.linearVelocity.y);

        }


    }

    /// <summary>
    /// Set the speed of the player with acceleration or deceleration
    /// </summary>
    /// <param name="Xinput"></param>
    private void SetSpeedWithAcceleration(float Xinput)
    {
        var allowedSpeedChange = Xinput != 0 ? player.HorizontalAcceleration : player.HorizontalDeceleration;
        var targetSpeed = (Xinput == 0) ? 0 : player.HorizontalSpeedGround;
        var speed = Mathf.MoveTowards(Mathf.Abs(player.rb.linearVelocity.x), targetSpeed, allowedSpeedChange * Time.deltaTime);
        player.rb.linearVelocityX = player.facingDir * speed;
    }

    public void Freeze()
    {
        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
    }
    
}
