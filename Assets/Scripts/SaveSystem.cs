using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [Header("Current player Stats")]
    public float currentHealth = 100f;

    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/player_save.json";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentHealth -= 10f;
            Debug.Log("Health reduced to: " + currentHealth);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        PlayerData data = new PlayerData();

        data.health = currentHealth;
        data.positionX = transform.position.x;
        data.positionY = transform.position.y;
        data.positionZ = transform.position.z;

        string jsonFormat = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, jsonFormat);

        Debug.Log("Game saved successfully at: " + saveFilePath);
    }

    public void LoadGame()
    {

    }
}