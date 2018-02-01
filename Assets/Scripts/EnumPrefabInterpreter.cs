using UnityEngine;
using System.Collections;

public class EnumPrefabInterpreter : MonoBehaviour 
{
	private static EnumPrefabInterpreter _instance;
	public static EnumPrefabInterpreter instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<EnumPrefabInterpreter>();
			return _instance;
		}
	}

	[System.Serializable]
	public class EnumPrefabPair
	{
		public ItemEnum itemEnum;
		public GameObject prefab;
	}

	public EnumPrefabPair[] pairs;

	public GameObject GetPrefab(ItemEnum itemType)
	{
		foreach (EnumPrefabPair pair in pairs)
		{
			if (pair.itemEnum == itemType)
				return pair.prefab;
		}

		return null;
	}
}
