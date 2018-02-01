using UnityEngine;
using System.Collections;

public class ScreenInputManager : MonoBehaviour 
{
	private static ScreenInputManager _instance;
	public static ScreenInputManager instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<ScreenInputManager>();
			return _instance;
		}
	}

	[SerializeField]
	private Camera mainCam;
	[SerializeField]
	private LayerMask layerMask;

	public bool allowDropFood = true;
	public bool allowPickMoney = true;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
				
			if (Physics.Raycast(ray, out hit, 100, layerMask))
			{	
				if (allowDropFood && hit.collider.CompareTag("Ground"))
				{
					FeedingManager.instance.DropFood(hit.point);
				}

				if (allowPickMoney && hit.collider.CompareTag("MoneyDrop"))
				{
					GameManager.instance.GetMoney(hit.collider.gameObject);
				}

			}

		}
	}
}
