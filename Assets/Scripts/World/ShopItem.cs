using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public float timeCost = 2f;

    public void BuyItem()
    {
        Debug.Log("Bought " + itemName);


    }
}