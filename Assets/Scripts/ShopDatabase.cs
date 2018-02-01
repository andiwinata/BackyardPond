using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ShopDatabase 
{
	private static Dictionary<ItemEnum, float> priceList = new Dictionary<ItemEnum, float>();

	static ShopDatabase()
	{
		priceList.Add(ItemEnum.Duck, 500);
	}

	public static float GetPrice(ItemEnum itemType)
	{
		if (priceList.ContainsKey(itemType))
		{
			return priceList[itemType];
		}
		return 0;
	}

}
