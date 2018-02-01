using UnityEngine;
using System.Collections;

public class HungryBehaviour : BaseBehaviour 
{
	public MeshRenderer[] meshes;
	private Material[] meshMats;
	private Color[] originalColors;
	public Color hungryTintColor = new Color (0.5f, 0.4f, 0f, 1f);

	private bool hungry = false;
	private bool isChasingFood = false;
	private	IHasHungryBehaviour ownerOfBehav;

	private float startingFullTime = 10;
	private float hungryRotSpeed = 360;
	private float lowerHungrySpeed = 1f;
	private float upperHungrySpeed = 3f;
	private float hungrySpeed;

	protected override void Awake()
	{
		base.Awake ();
		ownerOfBehav = (IHasHungryBehaviour) GetComponent(typeof(IHasHungryBehaviour));

		//get meshes material
		meshMats = new Material[meshes.Length];
		for (int h = 0; h < meshes.Length; h++)
		{
			meshMats[h] = meshes[h].material;
		}

		//store original colors
		originalColors = new Color[meshMats.Length];
		for (int i = 0; i < meshMats.Length; i++)
		{
			originalColors[i] = meshMats[i].color;
		}
	}

	void OnEnable()
	{
		FeedingManager.instance.SubscribeToFoodEvent(this);
		StartCoroutine(FullTimer(startingFullTime));
		hungry = false;
		isChasingFood = false;
	}

	void OnTriggerStay(Collider col)
	{
		if (hungry && col.CompareTag("Food"))
		{			
			//eat, stop hungry
			hungry = false; //will stop chasing
			ChangeMeshColor();
			ownerOfBehav.EatFood(); //notify owner after eating food
			
			float foodFullDuration = col.GetComponent<Food>().fullDuration;
			StartCoroutine(FullTimer(foodFullDuration));

			//disable the food and call foodeaten in manager
			col.gameObject.SetActive(false);
			FeedingManager.instance.FoodEaten(col.gameObject);
		}
	}

	public override void EnableBehaviour ()
	{
		StopCoroutine("ChasingFood"); //prevent duplicate of coroutine
		StartCoroutine("ChasingFood");
	}

	public override void DisableBehaviour ()
	{
		StopCoroutine("ChasingFood");
		isChasingFood = false;
	}

	private IEnumerator FullTimer(float duration)
	{
		yield return new WaitForSeconds(duration);

		//when duration ends, become hungry
		hungry = true;
		ChangeMeshColor();	
		CheckingFood();
	}

	//when there is food event (food drop/food eaten)
	public void CheckingFood()
	{
		if (FeedingManager.instance.foodExist && hungry) //food is exist and hungry
		{
			//ask permission to the owner to allow this behaviour started
			if (ownerOfBehav != null)
				ownerOfBehav.AllowHungryBehavStart();
		}
		else //either food doesn't exist or not hungry
		{
			if (isChasingFood)
			{
				//stop chasing food because no more food, notify the AI (the owner of this behaviour)
				if (ownerOfBehav != null)
					ownerOfBehav.StopHungryBehav();
			}
			//else dont do anything because this behaviour is in dormant
		}
	}

	private IEnumerator ChasingFood()
	{
		isChasingFood = true;
		hungrySpeed = Random.Range (lowerHungrySpeed, upperHungrySpeed);

		while (hungry)
		{
			Vector3 distToTarget = GetNearestFood().transform.position - transform.position;
			distToTarget.y = 0;
			
			Quaternion facing = Quaternion.LookRotation(distToTarget);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, hungryRotSpeed * Time.deltaTime);
			
			Vector3 movingDirection = transform.TransformDirection(Vector3.forward);
			
			base.charCont.Move(movingDirection * hungrySpeed * Time.deltaTime);
			yield return null;
		}
	}

	private GameObject GetNearestFood()
	{
		//get nearest food
		float smallestSqrDist = float.MaxValue;
		GameObject chasedFood = null;
		foreach (GameObject food in FeedingManager.instance.GetAvailableFoods())
		{
			float sqrFoodAndObjDist = (transform.position - food.transform.position).sqrMagnitude;
			if (sqrFoodAndObjDist < smallestSqrDist)
			{
				smallestSqrDist = sqrFoodAndObjDist;
				chasedFood = food;
			}
		}

		return chasedFood;
	}

	private void ChangeMeshColor()
	{
		if (hungry) //change color to addition yellow
		{
			foreach (Material mat in meshMats)
			{
				mat.color += hungryTintColor;
			}
		}
		else
		{
			for (int i = 0; i < meshMats.Length; i++)
			{
				meshMats[i].color = originalColors[i];
			}
		}

	}
}
