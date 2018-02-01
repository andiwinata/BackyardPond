using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesSpawnerManager : MonoBehaviour 
{
	private static EnemiesSpawnerManager _instance;
	public static EnemiesSpawnerManager instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<EnemiesSpawnerManager>();
			return _instance;
		}
	}

	[SerializeField]
	private GameObject piranhaPrefab;

	//random alert enemy time
	[SerializeField]
	private float lowerAlertTime = 5f;
	[SerializeField]
	private float upperAlertTime = 10f;
	
	private float lowerEnemySpawnInterval = 2f;
	private float upperEnemySpawnInterval = 4f;
	
	private int totalEnemiesInWave = 5;

	void Start()
	{
		StartAlertCountdown();
	}
	
	public void StartAlertCountdown()
	{
		StartCoroutine("AlertCountingDown");
	}
	
	private IEnumerator AlertCountingDown()
	{
		yield return new WaitForSeconds(Random.Range(lowerAlertTime, upperAlertTime));
		StartCoroutine("SpawnEnemies");
	}
	
	private IEnumerator SpawnEnemies()
	{
		for (int i = 0; i < totalEnemiesInWave; i++)
		{
			List<GameObject> activeDucks = GameManager.instance.activeDucks;
			if (activeDucks.Count == 0)
				break;

			int randDuck = Random.Range(0, activeDucks.Count);
			Vector3 targetDuck = activeDucks[randDuck].transform.position;
			targetDuck.y = 0;

			//spawn enemy on that random point
			PoolingManager.instance.SpawnItem(piranhaPrefab, targetDuck, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(lowerEnemySpawnInterval, upperEnemySpawnInterval));
		}
	}
}
