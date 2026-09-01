using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public PlayerData data;

    private void OnTriggerEnter(Collider item)
    {
        if (item != null && item.CompareTag("Item"))
        {
            CollectIten();
        }
    }

    public void CollectIten()
    {
        if (data != null && !data.hasCollectedItem)
        data.hasCollectedItem = true;
    }
}
