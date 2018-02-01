using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour 
{
	private static UIManager _instance;
	public static UIManager instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<UIManager>();
			return _instance;
		}
	}

	[SerializeField]
	private Text moneyText;

	public void UpdateTotalMoney(float money)
	{
		moneyText.text = string.Format("Money: {0}", (int)money);
	}
}
