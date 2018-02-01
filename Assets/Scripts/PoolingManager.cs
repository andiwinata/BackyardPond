using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolingManager : MonoBehaviour 
{
	private static PoolingManager _instance;
	public static PoolingManager instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<PoolingManager>();
			return _instance;
		}
	}
	
	private Dictionary<string, List<GameObject>> pooledItems = new Dictionary<string, List<GameObject>>();

	public GameObject SpawnItem(GameObject go, Vector3 position, Quaternion rotation)
	{
		GameObject item = SpawnItem(go);
		item.transform.position = position;
		item.transform.rotation = rotation;
		return item;
	}

	public GameObject SpawnItem(GameObject objectType)
	{
		//search from list if it contains the item
		if (pooledItems.ContainsKey(objectType.name))
		{
			//get inactive object from the list
			foreach (GameObject item in pooledItems[objectType.name])
			{
				if (!item.activeInHierarchy)
				{
					item.SetActive(true);
					item.transform.position = Vector3.zero;
					item.transform.rotation = Quaternion.identity;
					return item;
				}
			}

		}
		else //if the key doesnt exist, make new one
		{
			pooledItems.Add(objectType.name, new List<GameObject>());
		}
			
		//if there is no inactive object or just created new key, spawn new obj and add it to list
		GameObject newObj = Instantiate(objectType) as GameObject;
		newObj.SetActive(true);
		objectType.transform.position = Vector3.zero;
		objectType.transform.rotation = Quaternion.identity;
		pooledItems[objectType.name].Add(newObj);
		return newObj;
	}

}
