using UnityEngine;
using System.IO;
using TMPro;

public class SaveSystem : MonoBehaviour
{
    [Header("Current player Stats")]
    public float currentHealth = 100f;

    [Header("Player Reference")]
    [SerializeField] private GameObject player;

    [Tooltip("Referencia dos textos na UI do jogo")]
    [Header("UI Reference")]
    public TextMeshProUGUI healthText;

    private string saveFilePath;

    private void Start()
    {
        UpdateUI();
    }

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/player_save.json";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentHealth -= 10f;
            UpdateUI();
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

    private void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "Vida: " + currentHealth;
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

        Debug.Log("Game saved successfully at: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonContent = File.ReadAllText(saveFilePath);
            PlayerData loadData = JsonUtility.FromJson<PlayerData>(jsonContent);

            currentHealth = loadData.health;

            Vector3 restoresPosition = new Vector3(
                loadData.positionX,
                loadData.positionY,
                loadData.positionZ
            );

            player.transform.position = restoresPosition;

            UpdateUI();

            Debug.Log("Dados carregados, Vida Atual: " + currentHealth);
        }
        else
        {
            Debug.LogWarning("Não existe dados a ser carregado no: " + saveFilePath);
        }
    }
}