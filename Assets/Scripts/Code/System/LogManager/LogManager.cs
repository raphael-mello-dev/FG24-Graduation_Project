using UnityEngine;

public class LogManager
{
    public bool CanLogMessage;
    public bool CanLogWarning;
    public bool CanLogError;

    public void LogMessage(string text)
    {
        if (CanLogMessage) Debug.Log(text);
    }

    public void LogWarning(string text)
    {
        if (CanLogWarning) Debug.LogWarning(text);
    }

    public void LogError(string text)
    {
        if (CanLogError) Debug.LogError(text);
    }
}