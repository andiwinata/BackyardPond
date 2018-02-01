using UnityEngine;
using System.Collections;

public class DroppingBehaviour : MonoBehaviour //#LATER CHANGE BASE TO BASEBEHAV
{
	public GameObject droppedItem; 
	[SerializeField]
	private float dropInterlude = 15f;

	public void OnEnable()
	{
		StopCoroutine("DroppingItem");
		StartCoroutine("DroppingItem");
	}

	private IEnumerator DroppingItem()
	{
		while (gameObject.activeInHierarchy)
		{
			yield return new WaitForSeconds(dropInterlude);
			if (droppedItem != null)
				PoolingManager.instance.SpawnItem(droppedItem, transform.position, Quaternion.identity);
		}
	}
}
