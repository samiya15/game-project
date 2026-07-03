using UnityEngine;

public class ShopItem : MonoBehaviour, IInteractable
{
    public string itemName;
    public float timeCost = 2f;

    public void BuyItem()
    {
        Debug.Log("Bought " + itemName);


    }

    public void Interact()
    {
        BuyItem();
    }

    public string GetPromptText()
    {
        return $"{itemName}\nCost: {timeCost}h\nPress E to buy.";
    }
}