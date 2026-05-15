using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<MissionSO> MissionsList;
    [SerializeField] private TextMeshProUGUI missionsTitleText;

    private int currentMissionIndex;

    public static event Action OnListEmptied;

    void Start()
    {
        MissionsList = MissionsList.ConvertAll(MSO => Instantiate(MSO));

        if (GameManager.Instance.IsGameLoaded)
            LoadMissions();
        else
            NewGameMissions();        
    }

    void Update()
    {
        if (MissionsList[0] is GoToSO)
            MissionsList[0].OnMissionUpdated();

        if (MissionsList[0].IsCompleted)
            ChangeCurrentMission();
    }

    private void LoadMissions()
    {

    }

    private void NewGameMissions()
    {
        currentMissionIndex = 0;
        MissionsList[0].OnProgressDiplayed += UpdateMissionHUD;
        MissionsList[currentMissionIndex].OnStart();
    }

    private void ChangeCurrentMission()
    {
        MissionsList[0].OnProgressDiplayed -= UpdateMissionHUD;
        MissionsList[0].OnEnd();
        MissionsList.RemoveAt(0);

        if (MissionsList.Count == 0)
        {
            OnListEmptied?.Invoke();
            return;
        }

        currentMissionIndex = MissionsList[0].ID;
        MissionsList[0].OnProgressDiplayed += UpdateMissionHUD;
        MissionsList[0].OnStart();
    }

    private void UpdateMissionHUD(string text) => missionsTitleText.text = text;
}