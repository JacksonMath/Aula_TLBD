using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [Header("Current Player Stats")]
    public float currentHealth = 100f;

    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/player_save.json";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentHealth -= 10f;
            Debug.Log("Health reduced to: " + currentHealth);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.O))
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

        Debug.Log("Jogo salvo com sucesso em: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonFormat = File.ReadAllText(saveFilePath);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonContent);

            currentHealth = loadedData.health;

            Vector3 restoredPosition = new Vector3(
                loadedData.positionX,
                loadedData.positionY,
                loadedData.positionZ
                );

            transform.position = restoredPosition;

            Debug.Log("Dados carregados, Vida atual: " + currentHealth);
        }
        else
        {
            Debug.LogWarning("Não existe dados a ser carregado no: " + saveFilePath);
        }
    }

}