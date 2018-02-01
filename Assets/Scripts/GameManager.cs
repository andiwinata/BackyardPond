using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemEnum
{
	Duck,

}

public class GameManager : MonoBehaviour 
{
	private static GameManager _instance;
	public static GameManager instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<GameManager>();
			return _instance;
		}
	}

	public Transform[] spawnPoints;

	public float totalMoney {get; private set;}
	public List<GameObject> activeDucks {get; private set;}

	void Awake()
	{
		activeDucks = new List<GameObject>();
	}

	void Start () 
	{
		totalMoney = 100000f;
		UIManager.instance.UpdateTotalMoney(totalMoney);
	}

	public void RegisterDuck(GameObject go)
	{
		activeDucks.Add(go);
	}

	public void UnregisterDuck(GameObject go)
	{
		activeDucks.Remove(go);
	}

	public void GetMoney(GameObject money)
	{
		MoneyDrop moneyDrop = money.GetComponent<MoneyDrop>();
		if (moneyDrop != null)
		{
			totalMoney += moneyDrop.moneyAmount;
			money.SetActive(false);
			UIManager.instance.UpdateTotalMoney(totalMoney);		
		}
	}

	public void BuyItem(ItemInShop itemIdentifier)
	{
		BuyItem (itemIdentifier.itemEnum);
	}

	public void BuyItem(ItemEnum itemType)
	{
		float price = ShopDatabase.GetPrice(itemType);
		if (totalMoney >= price)
			totalMoney -= price;

		UIManager.instance.UpdateTotalMoney(totalMoney);
		SpawnAtRandomPoint( PoolingManager.instance.SpawnItem (EnumPrefabInterpreter.instance.GetPrefab(itemType)) );
	}

	private void SpawnAtRandomPoint(GameObject obj)
	{
		obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
	}

}
