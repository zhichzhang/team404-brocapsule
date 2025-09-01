using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Windows;
using static Player;

public class PlayerLadderMoveState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerLadderMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
       
        base.Enter();
        //stateMachine.stateLocked = true;
        player.LadderMoveCtrl.climbLadder();
    }

    public override void Exit()
    {
        player.LadderMoveCtrl.leaveLadder();
        
        base.Exit();
    }

    public override bool Update()
    {

        //Debug.Log("Battle Info: "+ player.battleInfo);

        player.FlipCtrl.onHorizontalInput();


        #region LaddderMove

        // change state to fall if player interact box leaves ladder
        if (!(player.ladderCheck && player.currentInteractingSpear != null))
        {
            player.stateMachine.ChangeState(player.fallState);
            return true;
        }

        // prepare variables
        DrpSpearVertical.SpearType spearType = player.currentInteractingSpear.type;
        const DrpSpearVertical.SpearType upType = DrpSpearVertical.SpearType.Up;
        float ladderX = player.currentInteractingSpear.transform.position.x;
        float ladderBoundY = player.currentInteractingSpear.boundPosition.transform.position.y;
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float Xinput = input.Xinput;
        float Yinput = input.Yinput;


        



        // vertical movement allowed when very close to ladder
        // bounds applied to prevent player from walking off the ladder from top or bottom
        if (Mathf.Abs(playerX - ladderX) < player.ladderCenterDeadZone)
        {
            // reset timer: onLadderSignal
            
            player.ladderSnapSpeedX = 0;
            if (spearType == upType)
            {
                if (playerY > ladderBoundY)
                {
                    rb.linearVelocityY = Yinput * player.ladderVerticalSpeed;
                }
                else
                {
                    if (Yinput > 0)
                    {
                        rb.linearVelocityY = Yinput * player.ladderVerticalSpeed;

                    }
                    else { 
                        rb.linearVelocityY = 0;
                    }
                }
            }
            else
            {
                if (playerY < ladderBoundY)
                {
                    rb.linearVelocityY = Yinput * player.ladderVerticalSpeed;
                }
                else
                {
                    if (Yinput < 0)
                    {
                        rb.linearVelocityY = Yinput * player.ladderVerticalSpeed;

                    }
                    else
                    {
                        rb.linearVelocityY = 0;
                    }
                }
            }
        }

        // horizontal movement but snapping force applied when no horizontal input
        if (Xinput != 0 && Yinput == 0)
        {
            player.rb.linearVelocityX = Xinput * player.ladderHorizontalSpeed;
        }
        else
        {
            float goalX = Mathf.SmoothDamp(playerX, ladderX, ref player.ladderSnapSpeedX, player.ladderSnapTime);
            player.rb.linearVelocityX = (goalX - playerX) / Time.deltaTime;
        }


        // vertical snapping applied player is below ladder boundPosition Y when no vertical input

        if (Yinput == 0 && Xinput == 0)
        {
            if (Mathf.Abs(playerY - ladderBoundY) < 0.01)
            {
                rb.linearVelocityY = 0;
            }


            if (spearType == upType)
            {
                if (playerY < ladderBoundY)
                {
                    float goalY = Mathf.SmoothDamp(playerY, ladderBoundY, ref player.ladderSnapSpeedY, player.ladderSnapTime);
                    rb.linearVelocityY = (goalY - playerY) / Time.deltaTime;
                }
            }
            else
            {
                if (playerY > ladderBoundY)
                {
                    float goalY = Mathf.SmoothDamp(playerY, ladderBoundY, ref player.ladderSnapSpeedY, player.ladderSnapTime);
                    rb.linearVelocityY = (goalY - playerY) / Time.deltaTime;
                }
            }
        }

        //

        #endregion


        //ladder => roll
        if (player.LevelCollisionCtrl.IsGroundDetected() && ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp()))
        {
            //player.stateMachine.stateLocked = false;
            player.ladderRemountCoolDownTimer.Set(player.ladderRemountCoolDown);
            stateMachine.ChangeState(player.rollState);
            return true;
        }
        //ladder => dash
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {

            if (player.canDash)
            {
                player.stateMachine.stateLocked = false;
                stateMachine.ChangeState(player.dashState);
                player.ladderRemountCoolDownTimer.Set(player.ladderRemountCoolDown);
                return true;  
            }
        }

        //ladder =>jump
        if (input.Jump || input.isJumpBuffered)
        {
            if (Yinput < 0)
            {
                input.isJumpBuffered = false;
                player.stateMachine.ChangeState(player.fallState);
                player.ladderRemountCoolDownTimer.Set(player.ladderRemountCoolDown + 0.2f);
                return true;
            }
            else
            {
                input.isJumpBuffered = false;
                stateMachine.ChangeState(player.jumpState);
                player.ladderRemountCoolDownTimer.Set(player.ladderRemountCoolDown);
                return true;
            }
            
        }

        //ladder => idle/fall
        // conflict with interact input buffer, so no buffer here
        if (!player.ladderCheck)
        {
            if (player.LevelCollisionCtrl.IsGroundDetected())
            {
                //player.stateMachine.stateLocked = false;
                stateMachine.ChangeState(player.idleState);
                player.ladderRemountCoolDownTimer.Set(player.ladderRemountCoolDown);
                return true;
            }
            else
            {
                //player.stateMachine.stateLocked = false;
                stateMachine.ChangeState(player.fallState);
                player.ladderRemountCoolDownTimer.Set(player.ladderRemountCoolDown);
                return true;
            }
        }

        return false;
        
    }
}
