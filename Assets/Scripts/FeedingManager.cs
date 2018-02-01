using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeedingManager : MonoBehaviour 
{
	private static FeedingManager _instance;
	public static FeedingManager instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<FeedingManager>();
			return _instance;
		}
	}


	public GameObject food;

	public bool foodExist {get; private set;}

	private List<GameObject> availableFoods = new List<GameObject>();
	private List<HungryBehaviour> hungryBehaviours = new List<HungryBehaviour>();

	public void DropFood(Vector3 position)
	{
		GameObject f = PoolingManager.instance.SpawnItem(food);
		f.transform.position = position + Vector3.up * 5;
		availableFoods.Add(f);

		foodExist = true;

		//inform all hungry behaviours to food check
		NotifyFoodEvent();
	}
	
	public void SubscribeToFoodEvent(HungryBehaviour behav)
	{
		hungryBehaviours.Add(behav);
	}

	public void NotifyFoodEvent()
	{
		foreach (HungryBehaviour behav in hungryBehaviours)
		{
			behav.CheckingFood();
		}
	}

	public IList<GameObject> GetAvailableFoods()
	{
		return availableFoods.AsReadOnly();
	}

	public void FoodEaten(GameObject eatenFood)
	{
		availableFoods.Remove(eatenFood);

		if (availableFoods.Count == 0)
			foodExist = false;

		//inform all hungry behaviours to food check
		NotifyFoodEvent();
	}
}
