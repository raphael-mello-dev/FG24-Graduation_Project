using UnityEngine;
using TMPro;

public class PauseProfileScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI statsText;
    
    private PlayerStats playerStats;


    private void Awake() => playerStats = GameManager.Instance.Player.Stats;

    private void OnEnable() => UpdateStatsText();

    void Start() => nameText.text = GameManager.Instance.Player.Name;

    // Update player info in the profile screen
    private void UpdateStatsText()
    {
        statsText.text = $"Level: {playerStats.Level} (EXP: {playerStats.ExperienceGained}/{(playerStats.Level + 1) * 10})" +
            $"\t\tExtra Points: {playerStats.ExtraPoints}\r\n\r\nHealth: {playerStats.CurrentHealth}/{playerStats.MaxHealth}" +
            $"\t\tStamina: {playerStats.CurrentStamina}/{playerStats.MaxStamina}\r\n\r\nStrength: {playerStats.Strength}\t\t" +
            $"Agility: {playerStats.Agility}\r\n\r\nIntelligence: -\t\tConstitution: {playerStats.Defense}";
    }
}