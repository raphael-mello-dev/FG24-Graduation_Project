using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameDifficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2,
    Master = 3
}

public enum GraphicsQuality
{
    Low,
    Medium,
    High
}

public enum Switch
{
    On,
    Off
}

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    #region Gameplay

    [Header("Gameplay")]
    public GameDifficulty Difficulty = GameDifficulty.Normal;

    [Range(1f, 20f)]
    public float CameraZoom = 6f;

    [Range(0f, 1f)]
    public float CameraFollowAngle = 0.1f;

    #endregion

    #region Graphics

    [Header("Graphics")]
    public FullScreenMode Fullscreen = FullScreenMode.FullScreenWindow;

    public int[,] Resolutions = { { 1920, 1080 }, { 1280, 720 }, { 800, 600 } };

    public GraphicsQuality graphicsQuality = GraphicsQuality.High;

    public Switch VSynch = Switch.Off;

    #endregion

    #region Audio

    [Header("Audio")]
    [Range(0f, 1f)]
    public float MasterVolume = 1f;
    [Range(0f, 1f)]
    public float MusicVolume = 1f;
    [Range(0f, 1f)]
    public float SFXVolume = 1f;

    #endregion

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}

[Serializable]
public struct Resolution
{
    public int width;
    public int height;
}