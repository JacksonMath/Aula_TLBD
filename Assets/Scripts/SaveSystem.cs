using UnityEngine;

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
    }
      
}
