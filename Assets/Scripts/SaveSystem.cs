using System.IO;
using TMPro;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("Current Player Stats")]
    public float currentHealth = 100f;
    public PlayerData currentPlayer;
    public GameObject player;

    [Header("UI References")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI itemText;

    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/player_save.json";
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthText.text = "Vida: " + currentHealth;
        // Operador ternário para mostrar Sim/Não baseado no booleano
        itemText.text = "Item: " + (currentPlayer.hasCollectedItem ? "Sim" : "Não");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentHealth -= 10f;
            UpdateUI();
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
        data.positionX = player.transform.position.x;
        data.positionY = player.transform.position.y;
        data.positionZ = player.transform.position.z;

        string jsonFormat = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, jsonFormat);

        Debug.Log("Jogo salvo com sucesso em: " + saveFilePath);

        UpdateUI();
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonContent = File.ReadAllText(saveFilePath);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonContent);

            currentHealth = loadedData.health;

            Vector3 restoredPosition = new Vector3(
                loadedData.positionX,
                loadedData.positionY,
                loadedData.positionZ
                );

            player.transform.position = restoredPosition;

            Debug.Log("Dados carregados, Vida atual: " + currentHealth);
        }
        else
        {
            Debug.LogWarning("Não existe dados a ser carregado no: " + saveFilePath);
        }

        UpdateUI();
    }

}