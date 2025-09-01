using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.EventSystems.EventTrigger;


public class HeatmapAnalyzer : MonoBehaviour
{
    [System.Serializable]
    public class CeilData
    {
        public int ceilRowIndex;
        public int ceilColIndex;
        public double time;
    }

    [System.Serializable]
    public class CeilDatatListWrapper
    {
        public List<CeilData> ceilDataList;

        public CeilDatatListWrapper(List<CeilData> ceilDataList)
        {
            this.ceilDataList = ceilDataList;
        }
    } 

    [Header("Input")]
    public Transform checkPointsParent;
    public Tilemap map;
    public Transform goal;
    public int levelIndex = 0;
    public int ceilSizeDefault = 4;


    [Header("Google Form Uploading Setting")]
    private string URL = "https://docs.google.com/forms/d/e/1FAIpQLSf24zV4F-XhBMBknAWDErOCJN4WvykR5jPe0AtKrWeSBJQMUw/formResponse";

    [Header("Level Info")]
    [SerializeField] private long _sessionID;
    [SerializeField] private string level;
    [SerializeField] private int checkPointsPlusGoalCounter;
    [SerializeField] private string checkPointName;
    [SerializeField] private bool completed;
    [SerializeField] private bool success;
    [SerializeField] private float levelWidth;
    [SerializeField] private float levelHeight;
    [SerializeField] private string heatmapJSONString;
    [SerializeField] private Vector2 goalPos;
    //[SerializeField] private float levelTopLeftPointX;
    //[SerializeField] private float levelTopLeftPointY;
    //[SerializeField] private float levelBottomRightX;
    //[SerializeField] private float levelBottomRightY;

    [Header("Player Info")]
    [SerializeField] private float playerX;
    [SerializeField] private float playerY;

    [Header("Heatmap Info")]
    //[SerializeField] private CeilDatatListWrapper ceilDataListWrapper;
    [SerializeField] private float ceilSize;
    [SerializeField] private int ceilRowIndex;
    [SerializeField] private int ceilColumnIndex;
    [SerializeField] private double timeSpentOnTheCeil;

    [Header("Temporary Paramters")]
    [SerializeField] private Dictionary<(int, int), double> timeMap;
    private CapsuleCollider2D playerCollider;
    //private double[] checkPointsX;
    [SerializeField] private float MAPH;
    [SerializeField] private float MAPW;
    [SerializeField] private Vector2 MAP_TOP_LEFT_POINT;
    //[SerializeField] private Vector2 MAP_BOTTOM_RIGHT_POINT;
    [SerializeField] public Dictionary<CheckPoint, bool> visited;
    [SerializeField] private int checkPointsPlusGoalNumber;
    //[SerializeField] private bool isGoalReached = false;

    public static HeatmapAnalyzer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _sessionID = DateTime.Now.Ticks;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    public GameObject findGameObject(string name)
    {
        GameObject obj = GameObject.Find(name);

        if (obj == null)
        {
            Debug.LogError($"GameObject {name} not found.");
            return null;
        }

        return obj;
    }

    public void init()  // Initialization for the heatmap analyzer.
    {
        Debug.Log("Heatmap initialization starts.");

        string mapName = "Tilemap";
        string checkPointsParentName = "CheckPoints";
        string goalName = "Goal";

        GameObject mapObj = findGameObject(mapName);

        map = mapObj.GetComponent<Tilemap>();

        if (map == null)
        {
            Debug.LogError($"Tilemap component missing on GameObject: {mapObj.name}");
            return;
        }

        GameObject checkPointsParentObj = findGameObject(checkPointsParentName);

        checkPointsParent = checkPointsParentObj.transform;

        if (checkPointsParent == null)
        {
            Debug.LogError($"CheckPointsParent component missing on GameObject: {checkPointsParentObj.name}");
            return;
        }

        GameObject goalGameObj = findGameObject(goalName);
        goal = goalGameObj.transform;

        if (goal == null)
        {
            Debug.LogError($"Goal component missing on GameObject: {goalGameObj.name}");
            return;
        }

        goalPos = goal.position;

        BoundsInt bounds = map.cellBounds;
        MAPH = bounds.size.y;
        MAPW = bounds.size.x;
        MAP_TOP_LEFT_POINT = new Vector2(bounds.xMin, bounds.yMax);

        levelHeight = MAPH;
        levelWidth = MAPW;

        completed = false;
        success = false;
        checkPointsPlusGoalCounter = 0;
        ceilSize = ceilSizeDefault;

        StartCoroutine(WaitForPlayerThenInit());

        visited = new Dictionary<CheckPoint, bool>();
        checkPointsPlusGoalNumber = checkPointsParent.childCount + 1;

        this.level = SceneManager.GetActiveScene().name;
        checkPointName = "";
        initTimeMap();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}, reinitializing HeatmapAnalyzer.");
        init();
    }

    private IEnumerator WaitForPlayerThenInit()
    {
        while (PlayerInfo.instance == null || PlayerInfo.instance.player == null)
            yield return null;

        playerCollider = PlayerInfo.instance.player.GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {

        CountTime();

        float distance = Vector2.Distance(playerCollider.bounds.center, goalPos);

        if (distance <= 2.0f && !success)  // Check if the player reachs the destination or not.
        {
            Debug.Log($"Reached the goal. Start to send a heatamp...");
            checkPointName = goal.name;
            completed = true;
            success = true;
            SendAHeatmap();
            completed = false;
        }
    }

    void OnApplicationQuit()
    {
        if (!success)  // If player did not go through the level.
        {
            Debug.Log("Application quits accidentially. Uploading heatmap...");
            this.checkPointName += "_";  // The underline mark denotes that the player did not finish the route after the most recently visited checkpoint.
            SendAHeatmap();
            Debug.Log("Uploaded heatmap successfully.");
        }
    }

    public void isCheckPointCompleted(CheckPoint cp)
    {

        if (checkPointsPlusGoalCounter == 0) // Check if the player meets the first check point or not.
        {
            Debug.Log("Checkpoint reached: " + cp.name + " (This is the first checkpoint)");
            visited[cp] = true;
            checkPointName = cp.name;
            checkPointsPlusGoalCounter++;
        }
      
        if (!visited.ContainsKey(cp))  // Check if the check point has been visited or not.
        {
            visited[cp] = true;
            Debug.Log("Checkpoint reached: " + cp.name + " (New)");


            Debug.Log("Reached a new checkpoint: " + cp.name);
            checkPointName = cp.name;
            completed = true;
            Debug.Log("Checkpoint " + checkPointsPlusGoalCounter + " reached. Proceed to the next one.");

            SendAHeatmap();
            
            checkPointsPlusGoalCounter++;
            Debug.Log("Good! Try to reach the next checkpoint.");
            completed = false;
            initTimeMap();

            if (checkPointsPlusGoalCounter == checkPointsPlusGoalNumber - 1)
            {
                Debug.Log("All checkpoints are visited! Great job!");
            }
        }
        else
        {
            Debug.Log("Checkpoint revisited: " + cp.name);
        }
    }

    public void CountTime()
    {
        (int, int) rowCol = getPlayerCeilRowColumn();
        float deltaTime = Time.deltaTime;

        if (timeMap.TryGetValue(rowCol, out double currentTime))
        {
            timeMap[rowCol] = currentTime + deltaTime;
        }
        else
        {
            timeMap[rowCol] = deltaTime;
        }
    }

    public (int, int) getPlayerCeilRowColumn()
    {
        Vector2 playerPos = playerCollider.bounds.center;

        float horizontalDis = playerPos.x - MAP_TOP_LEFT_POINT.x;
        float verticalDis = MAP_TOP_LEFT_POINT.y - playerPos.y;

        (int, int) rowCol = getCeilRowColumn(horizontalDis, verticalDis);
        int rowNumber = rowCol.Item1;
        int columnNumber = rowCol.Item2;

        return (rowNumber, columnNumber);
    }

    public (int, int) getCeilRowColumn(float width, float height)
    {
        int rowNumber = Mathf.FloorToInt(height / ceilSize);
        int columnNumber = Mathf.FloorToInt(width / ceilSize);

        return (rowNumber, columnNumber);
    }

    public void initTimeMap()
    {
        Debug.Log("Init Time Map.");
        timeMap = new Dictionary<(int, int), double>();

        
        (int, int) rowCol  = getCeilRowColumn(levelWidth, levelHeight);
        int rowN = rowCol.Item1;
        int columnN = rowCol.Item2;

        for (int i = 0; i <= rowN; i++)
        {
            for (int j = 0; j <= columnN; j++)
            {
                var coord = (i, j);
                timeMap.Add(coord, 0.0);
                
            }
        }
    }

    public void SendAHeatmap()
    {
        Debug.Log("Send to Google.");  // Send to google.
        SendToGoogle.instance.Send();

        List<CeilData> ceilDataList = new List<CeilData>();

        foreach (var entry in timeMap)
        {
            if (entry.Value > 0.001)
            {
                CeilData ceilData = new CeilData {ceilRowIndex = entry.Key.Item1, ceilColIndex = entry.Key.Item2, time = Math.Round(entry.Value, 3)};
                ceilDataList.Add(ceilData);
            }
        }

        CeilDatatListWrapper wrapper = new CeilDatatListWrapper(ceilDataList);
        heatmapJSONString = JsonUtility.ToJson(wrapper);
        StartCoroutine(UploadHeatmapCoroutine());

        //Debug.Log($"> Session_ID: {_sessionID}");
        //Debug.Log($"> CeilDataList: {ceilDataList.ToString()}");
        //Debug.Log($"> Session ID: {_sessionID}");
        //Debug.Log($"> Level: {level}");
        //Debug.Log($"> Level Width: {levelWidth}");
        //Debug.Log($"> Level height: {levelHeight}");
        //Debug.Log($"> CheckPoint: {checkPointName}");
        //Debug.Log($"> CheckPoint Order: {checkPointsPlusGoalCounter}");
        //Debug.Log($"> Completed: {completed}");
        //Debug.Log($"> Success: {success}");
        //Debug.Log($"> HeatmapJSONString: {heatmapJSONString}");
    }


    IEnumerator UploadHeatmapCoroutine()
    {
        yield return Post();
    }

    IEnumerator Post()
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.27628311", _sessionID.ToString()); // Session ID
        form.AddField("entry.813258949", level.ToString());  // Level
        form.AddField("entry.1274235764", checkPointName); // Checkpoint name
        form.AddField("entry.1204114337", levelWidth.ToString()); // Level width
        form.AddField("entry.1156096476", levelHeight.ToString()); // Level height
        form.AddField("entry.274085677", ceilSize.ToString()); // Ceil size
        form.AddField("entry.1173691568", completed.ToString().ToLower()); // Completed
        form.AddField("entry.11526168", success.ToString().ToLower()); // Success
        form.AddField("entry.688043743", checkPointsPlusGoalCounter.ToString()); // Check point order
        form.AddField("entry.474177861", heatmapJSONString); // Heatmap JSON string

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Player is player too fast, some data will be dropped");
            Debug.Log(www.responseCode);
        }
        else
        {
            Debug.Log("Data successfully uploaded!");
        }
    }
}
