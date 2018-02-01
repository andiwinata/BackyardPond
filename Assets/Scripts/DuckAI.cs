using UnityEngine;
using System.Collections;

public class DuckAI : MonoBehaviour, IHasWanderBehaviour, IHasHungryBehaviour, IHasGrowBehaviour
{
	//interafaces variables
	public HungryBehaviour hungryBehaviour {get; set;}
	public WanderBehaviour wanderBehaviour {get; set;}
	public GrowBehaviour growBehaviour {get; set;}

	//not interface
	public DroppingBehaviour droppingBehaviour {get; set;}

	void Awake () 
	{
		hungryBehaviour = GetComponent<HungryBehaviour>();
		wanderBehaviour = GetComponent<WanderBehaviour>();
		growBehaviour = GetComponent<GrowBehaviour>();

		droppingBehaviour = GetComponent<DroppingBehaviour>();
	}

	void OnEnable()
	{
		GameManager.instance.RegisterDuck(this.gameObject);
		wanderBehaviour.EnableBehaviour();
	}

	void OnDisable()
	{
		if (GameManager.instance != null)
			GameManager.instance.UnregisterDuck(this.gameObject);
	}

	//HasHungryBehaviour Interface
	public void AllowHungryBehavStart ()
	{
		wanderBehaviour.DisableBehaviour();
		hungryBehaviour.EnableBehaviour();
	}

	public void StopHungryBehav ()
	{
		hungryBehaviour.DisableBehaviour();
		wanderBehaviour.EnableBehaviour();
	}
	
	public void EatFood ()
	{
		//notify grow behaviour that it just ate food
		growBehaviour.EatAndGrow();
	}

	//GrowBehaviour Interface
	public void ChangeDroppingItem (GameObject newItem)
	{
		droppingBehaviour.droppedItem = newItem;
		droppingBehaviour.OnEnable(); //reset the dropping time eachtime the item drop changes
	}
}
