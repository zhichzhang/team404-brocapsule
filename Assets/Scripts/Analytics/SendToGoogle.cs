using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SendToGoogle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeY4A1l1x3K3anho5MYdgZsPemq6980CobLuXJ0fFmSzpvOiQ/formResponse";
    public static SendToGoogle instance;
    private long _sessionID;
    public int parryAttempted;
    public int parrySuccessful;
    public int dodgeAttempted;
    public int dodgeSuccessful;
    public int jumps;
    public int doubleJumps;
    public int attacks;
    public int verticalAttacks;
    public int time;
    public int duration;
    public int checkStart;
    public int checkEnds;
    public int deaths;
    public int kills;
    public string completeLevel;
    public string version = "gold-1";

    // new matrix,
    // 1. time spend between each checkpoint
    // 2. death count and death location at each checkpoint
    // 3. checkpoint complete in general (how many checkpoint are completed) (or last check point)


    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _sessionID = DateTime.Now.Ticks;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    void Start()
    {
        ResetAll();
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Send()
    {
        // Assign variables
        //parryAttempted = UnityEngine.Random.Range(0, 100);
        //parrySuccessful = UnityEngine.Random.Range(0, parryAttempted);

        //update time
        SetTime((int)Time.time);

        StartCoroutine(Post(_sessionID.ToString(), version,
            parryAttempted.ToString(), parrySuccessful.ToString(), 
            dodgeAttempted.ToString(), dodgeSuccessful.ToString(),
            jumps.ToString(), doubleJumps.ToString(), attacks.ToString(),
            verticalAttacks.ToString(), duration.ToString(), checkStart.ToString(), checkEnds.ToString(), deaths.ToString(), completeLevel)
            );
        ResetAll();
    }

    IEnumerator Post(string sessionID, string version,
        string parryAttempted, string parrySuccessful, 
        string dodgeAttempted, string dodgeSuccessful,
        string jumps, string doubleJumps, string attacks,
        string verticalAttack, string duration, string checkS, string checkE, string deaths, string complete)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.139774989", sessionID);
        form.AddField("entry.2104169773", SceneManager.GetActiveScene().buildIndex.ToString());
        form.AddField("entry.1526695763", version);
        form.AddField("entry.353296791", parryAttempted);
        form.AddField("entry.1606299993", parrySuccessful);
        form.AddField("entry.2013387407", dodgeAttempted);
        form.AddField("entry.452177833", dodgeSuccessful);

        form.AddField("entry.2080864451", jumps);
        form.AddField("entry.405082152", doubleJumps);
        form.AddField("entry.1861830295", duration);
        form.AddField("entry.1609368432", attacks);
        form.AddField("entry.638529257", verticalAttack);
        form.AddField("entry.309811429", checkS);
        form.AddField("entry.1325056948", checkE);
        form.AddField("entry.864959927", deaths);
        form.AddField("entry.182684281", kills);
        form.AddField("entry.2050549034", complete);

        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    public void AddParryCount()
    {
        parryAttempted++;
    }

    public void AddSuccessfulParryCount()
    {
        parrySuccessful++;
    }
    public void AddDodgeCount()
    {
        dodgeAttempted++;
    }

    public void AddSuccessfulDodgeCount()
    {
        dodgeSuccessful++;
    }
    public void AddJumps()
    {
        jumps++;
    }
    public void AddDoubleJumps()
    {
        //doubleJumps++;
    }
    public void AddAttacks()
    {
        attacks++;
    }
    public void AddVerticalAttacks()
    {
        verticalAttacks++;
    }
    public void SetTime(int t)
    {
        duration = t - time;
        time = t;
    }
    public void UpdateCheckStart(int startPoint)
    {
        checkStart = startPoint;
    }
    public void UpdateCheckEnds(int endPoint)
    {
        checkEnds = endPoint;
    }
    public void AddDeaths()
    {
        deaths++;
    }
    public void AddKills()
    {
        kills++;
    }
    public void UpdateCompletion(string status)
    {
        completeLevel = status;
    }

    public void ResetAll()
    {
        parryAttempted = 0;
        parrySuccessful = 0;
        dodgeAttempted = 0;
        dodgeSuccessful = 0;
        jumps = 0;
        doubleJumps = 0;
        attacks = 0;
        verticalAttacks = 0;
        time = (int)Time.time;
        duration = 0;
        checkStart = checkEnds;
        checkEnds = 0;
        completeLevel = "False";
        deaths = 0;
        kills = 0;
    }
}
