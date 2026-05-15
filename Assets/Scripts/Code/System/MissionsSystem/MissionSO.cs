using System;
using UnityEngine;

public class MissionSO : ScriptableObject
{
    public int ID;
    public string Title;
    public string Description;

    protected bool isCompleted;
    public bool IsCompleted {  get { return isCompleted; } }

    public event Action<string> OnProgressDiplayed;

    public virtual void OnStart() => isCompleted = false;
    public virtual void OnMissionUpdated() { }
    public virtual void OnEnd() { }
    protected void DisplayMissionProgress(string text) => OnProgressDiplayed?.Invoke(text);
}