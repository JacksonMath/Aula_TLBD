using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private PlayerData data;

    private void OnTriggerEnter(Collider item)
    {
        if (item.CompareTag("Item"))
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
