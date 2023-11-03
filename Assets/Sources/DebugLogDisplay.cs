using UnityEngine;

public class DebugLogDisplay : MonoBehaviour
{
    private const int MaxLogLines = 10;
    private string logText = "";
    private GUIStyle guiStyle = new GUIStyle();
    private bool showLogInGame = false;

    private float lastLogTime = 0f;

    private void Start()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;

        showLogInGame = !Application.isEditor;
    }

    private void OnGUI()
    {
#if UNITY_STANDALONE_WIN
        if (showLogInGame && Input.GetKeyDown(KeyCode.Alpha0))
        {
            showLogInGame = !showLogInGame;
        }

        if (showLogInGame)
        {
            GUI.Label(new Rect(10, 10, Screen.width, Screen.height), logText, guiStyle);
        }
#endif
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void Update()
    {
        if (showLogInGame && Time.time - lastLogTime > 3f)
        {
            logText = "";
        }
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        logText += logString + "\n";

        string[] logLines = logText.Split('\n');
        if (logLines.Length > MaxLogLines)
        {
            logText = string.Join("\n", logLines, logLines.Length - MaxLogLines, MaxLogLines);
        }

        lastLogTime = Time.time;
    }
}

