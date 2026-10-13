using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // Verifica se a cena deixou a flag "LoadSave" como verdadeiro
        if (PlayerPrefs.GetInt("LoadSave", 0) == 1)
        {
            LoadGame(); // Carrega os dados JSON

            // Reseta a flag para 0, para que se o jogador morrer e a cena reiniciar normalmente, não carregue o save de forma acidental
            PlayerPrefs.SetInt("LoadSave", 0);
            PlayerPrefs.Save();
        }
    }

    public void UpdateUI()
    {
        healthText.text = "Vida: " + currentHealth;
        // Operador ternário para mostrar Sim/Não baseado no booleano
        itemText.text = "Item: " + (currentPlayer.hasCollectedItem ? "Sim" : "Não");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentHealth >= 0)
        {
            currentHealth -= 10f;
            UpdateUI();
            Debug.Log("Health reduced to: " + currentHealth);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.O) || currentHealth <= 0)
        {
            ReloadSceneWithSave();
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

    public void ReloadSceneWithSave()
    {
        // Cria um bilhete dizendo para quando a cena abrir, carregar o save
        PlayerPrefs.SetInt("LoadSave", 1);
        PlayerPrefs.Save();

        // Pega o indice da cena atual e recarrega do zero
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
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