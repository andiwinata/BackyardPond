using UnityEngine;
using System.Collections;

public class MoneyDrop : MonoBehaviour 
{
	[SerializeField]
	private float _moneyAmount;
	public float moneyAmount 
	{
		get {return _moneyAmount;}
	}
}
