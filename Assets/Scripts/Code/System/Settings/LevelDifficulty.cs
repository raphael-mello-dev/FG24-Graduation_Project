using UnityEngine;

public class LevelDifficulty
{
    #region Enemies

    public static int GetAmountOfEnemiesSpawned()
    {
        int amount = 4;

        for (int i = 0; i <= ((int)GameSettings.Instance.Difficulty); i++)
            amount += i;

        return amount;
    }

    public static int GetEnemiesMaxLevel()
    {
        int gameLevel = ((int)GameSettings.Instance.Difficulty);
        return (gameLevel + (gameLevel + 1));
    }

    public static float GetMaxSpawnRange()
    {
        float spawnRange = 5 + (5 * (int)GameSettings.Instance.Difficulty);
        return spawnRange;
    }

    public static bool GetCanEnemiesRespawn()
    {
        bool canRespawn = (GameSettings.Instance.Difficulty == GameDifficulty.Easy) ? false : true;
        return canRespawn;
    }

    public static int GetDamageReduction()
    {
        int level = ((int) GameSettings.Instance.Difficulty); 
        return level / (level + 3);
    }

    public static int GetBossLevel()
    {
        float bossLevel = (GameSettings.Instance.Difficulty == GameDifficulty.Easy ? 1f : GameManager.Instance.Player.Stats.Level / 2 * ((int)GameSettings.Instance.Difficulty));
        return Mathf.RoundToInt(bossLevel);
    }

    #endregion

    #region Player

    public static int GetExtraPoints()
    {
        return 7 - ((int) GameSettings.Instance.Difficulty);
    }

    public static bool GetCanHeal()
    {
        bool canHeal = (((int)GameSettings.Instance.Difficulty) < 2) ? true : false;
        return canHeal;
    }

    #endregion

    public static bool PlayerCanSave()
    {
        bool canSave = (((int)GameSettings.Instance.Difficulty) < 2) ? true : false;
        return canSave;
    }
}