using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInShop : MonoBehaviour
{
	public ItemEnum itemEnum;

	[Header("Tick this to show price of an item, need the text UI to be inputted")]
	public bool showPrice;
	public Text priceText;

	void Start()
	{
		if (showPrice && priceText != null)
		{
			priceText.text = ShopDatabase.GetPrice(itemEnum).ToString();
		}
	}
}
