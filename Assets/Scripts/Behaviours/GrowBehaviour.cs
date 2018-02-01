using UnityEngine;
using System.Collections;

public class GrowBehaviour : MonoBehaviour //LATER CHANGE TO BASE BEHAVIOUR 
{

	[System.Serializable]
	public class GrowUpgrade
	{
		public float scaleSize;
		public GameObject itemDropped;
	}
	[SerializeField]
	private GameObject changedMesh;

	[SerializeField]
	private int totalFoodInGrowthStage = 3;

	[SerializeField]
	[Tooltip("Put total number as total growing stages, and info for each growth stage")]
	private GrowUpgrade[] growthStages;

	//progress
	private int foodEatenInGrowthStage;
	private int currentGrowthStage;

	//saved beginning size
	private Vector3 initialScale;
	private IHasGrowBehaviour ownerHasGrow;

	void Awake()
	{
		ownerHasGrow = (IHasGrowBehaviour) GetComponent(typeof(IHasGrowBehaviour));
	}

	void OnEnable()
	{
		foodEatenInGrowthStage = 0;
		currentGrowthStage = 0;

		//reset to starting size
		changedMesh.transform.localScale = growthStages[0].scaleSize * Vector3.one;
		ownerHasGrow.ChangeDroppingItem(growthStages[0].itemDropped);
	}

	public void EatAndGrow()
	{
		if (currentGrowthStage == growthStages.Length - 1) //is the last index, no more growth changes
			return;

		//eat
		foodEatenInGrowthStage++;

		//check if can go to new stage
		if (foodEatenInGrowthStage >= totalFoodInGrowthStage)
		{
			currentGrowthStage++; //go to next stage
			foodEatenInGrowthStage = 0; //reset in the next stage

			//update the scale of mesh
			changedMesh.transform.localScale = growthStages[currentGrowthStage].scaleSize * Vector3.one;
			//notify the parent to change item dropped
			ownerHasGrow.ChangeDroppingItem(growthStages[currentGrowthStage].itemDropped);
		}
	}

}
