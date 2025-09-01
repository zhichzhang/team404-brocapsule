using UnityEngine;

public class ExternalDataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static ExternalDataManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    /// <summary>
    /// This function is called when player make deflect move
    /// </summary>
    public void PlayerDeflect()
    {
        if(SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddParryCount();
        }
    }

    /// <summary>
    /// This function is called when player make deflect move successfully
    /// </summary>
    public void PlayerDeflectSuccess()
    {
        if(SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddSuccessfulParryCount();
        }
    }

    /// <summary>
    /// This function is called when player make dodge move
    /// </summary>
    public void PlayerDodge()
    {
        if(SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddDodgeCount();
        }
    }

    /// <summary> 
    /// This function is called when player make dodge move successfully
    /// </summary>
    public void PlayerDodgeSuccess()
    {
        if (SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddSuccessfulDodgeCount();
        }
    }   

    public void PlayerJump()
    {
        if(SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddJumps();
        }
    }

    public void PlayerAttack()
    {
        if (SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddAttacks();
        }
    }

    public void PlayerVerticalAttack()
    {
        if (SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddVerticalAttacks();
        }
    }

    public void PlayerDeaths()
    {
        if (SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddDeaths();
        }
    }

    public void PlayerKills()
    {
        if (SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddKills();
        }
    }

    public void OnLevelTransition()
    {
        if (SendToGoogle.instance != null)
        {
            if (SendToGoogle.instance != null)
            {
                //send data when transit to next level 
                // while we can use Q key to manually send data
                //SendToGoogle.instance.SetTime((int)Time.time);
                SendToGoogle.instance.UpdateCompletion("True");
                // send player position?
                // int x = (int)PlayerInfo.instance.player.rb.position.x;
                // int y = (int)PlayerInfo.instance.player.rb.position.y;
                // SendToGoogle.instance.UpdateCheckEnds(x * 10000 + y);
                SendToGoogle.instance.Send();
                SendToGoogle.instance.ResetAll();
            }
        }
    }

    public void NewCheckPointReached(Vector2 checkPointPosition)
    {
        if (SendToGoogle.instance != null)
        {
            //very simple and lazy way to generate a "id" of the checkpoint for now.
            //porb should replace this part in future
            int x = (int)checkPointPosition.x;
            int y = (int)checkPointPosition.y;

            SendToGoogle.instance.UpdateCheckEnds(x * 10000 + y);
        }
    }
}
