using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    public GameData GameData {  get; private set; }

    public bool CanBeSaved = true;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        GameData = null;
    }

    public void CreateGameData() => GameData = new GameData();

    public void SaveGame()
    {
        //Debug.Log($"{Application.persistentDataPath}/Saves/Data.json");

        GameData.SetData();
        string json = JsonUtility.ToJson(GameData);
        File.WriteAllText($"{Application.persistentDataPath}/Saves/Data.json", json);
        Debug.Log($"Game Saved to: {Application.persistentDataPath}/Saves/Data.json");
    }

    public bool LoadGame()
    {
        string filePath = $"{Application.persistentDataPath}/Saves/Data.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(json);
            GameData = loadedData;
            Debug.Log("Game Loaded from: " + filePath);
            return true;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return false;
        }
    }
}