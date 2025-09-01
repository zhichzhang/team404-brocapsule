using UnityEngine;

public class LevelCollisionController
{

    public Player player;


    public LevelCollisionController(Player _player)
    {
        player = _player;
    }


    // GroundCheck
    public bool IsGroundDetected()
    {

        bool leftCheck = Physics2D.Raycast(player.groundCheckLeft.position, Vector2.down, player.groundCheckDistance, player.level);
        bool rightCheck = Physics2D.Raycast(player.groundCheckRight.position, Vector2.down, player.groundCheckDistance, player.level);


        return leftCheck || rightCheck;
    }

    // WallCheck
    public bool IsWallDetected()
    {


        bool topCheck = Physics2D.Raycast(player.wallCheckTop.position, Vector2.right * player.facingDir, player.wallCheckDistance, player.level);
        bool bottomCheck = Physics2D.Raycast(player.wallCheckBottom.position, Vector2.right * player.facingDir, player.wallCheckDistance, player.level);

        return topCheck && bottomCheck;
    }
    public void draw()
    {

        Gizmos.DrawLine(player.groundCheckLeft.position, new Vector3(player.groundCheckLeft.position.x, player.groundCheckLeft.position.y - player.groundCheckDistance));
        Gizmos.DrawLine(player.groundCheckRight.position, new Vector3(player.groundCheckRight.position.x, player.groundCheckRight.position.y - player.groundCheckDistance));
        //WallCheck
        Gizmos.DrawLine(player.wallCheckTop.position, new Vector3(player.wallCheckTop.position.x + player.wallCheckDistance * player.facingDir, player.wallCheckTop.position.y));
        Gizmos.DrawLine(player.wallCheckBottom.position, new Vector3(player.wallCheckBottom.position.x + player.wallCheckDistance * player.facingDir, player.wallCheckBottom.position.y));
    }
}
