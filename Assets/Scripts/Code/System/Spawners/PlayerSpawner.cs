using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject GameplayHUD;

    void Start()
    {
        if (GameManager.Instance.IsGameLoaded)
        {
            GameplayHUD.SetActive(true);
            var player = SaveManager.Instance.GameData.Player;

            // Setting player
            GameManager.Instance.SetPlayer();
            GameManager.Instance.Player.SetPlayer(player.Name, $"{player.PrefabPath}");
            
            // Spawn player prefab into the game
            Instantiate(Resources.Load<GameObject>(GameManager.Instance.Player.PrefabPath), new Vector3(player.Position[0], player.Position[1], 
                player.Position[2]), Quaternion.Euler(player.Rotation[0], player.Rotation[1], player.Rotation[2]), transform);
            
            // Get player gameobject reference
            GameManager.Instance.Player.SetPlayerGameObject(GameObject.FindGameObjectWithTag("Player"));
            // Initialize player stats
            GameManager.Instance.Player.Stats.LoadStats();
            // Switching input map to gameplay to player be able to perform gameplay actions
            GameManager.Instance.Player.InputManager.SwitchInputMap(InputMap.Gameplay);
            // Set Camera follow
            GameManager.Instance.SetCamera();
            Camera.main.GetComponent<DefaultCamera>().enabled = true;
        }
        else
        {   // Spawn player prefab into the game
            Instantiate(Resources.Load<GameObject>(GameManager.Instance.Player.PrefabPath), transform.position, Quaternion.identity, transform);

            // Get player gameobject reference
            GameManager.Instance.Player.SetPlayerGameObject(GameObject.FindGameObjectWithTag("Player"));
        }
        // Push player movement action onto the player stack
        GameManager.Instance.Player.PushAction(new PlayerMovement(GameManager.Instance.Player));
    }
}