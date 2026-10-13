using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider item)
    {
        if (item.CompareTag("Item"))
        {
            CollectItem(item.gameObject);
        }
    }

    public void CollectItem(GameObject item)
    {
        SaveSystem saveSystem = FindFirstObjectByType<SaveSystem>();

        if (saveSystem != null)
        {
            saveSystem.currentPlayer.hasCollectedItem = true;

            saveSystem.UpdateUI();

            Destroy(item);
        }
    }
}
