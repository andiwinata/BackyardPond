using UnityEngine;
using System.Collections;

public interface IHasGrowBehaviour  
{
	GrowBehaviour growBehaviour {get; set;}
	void ChangeDroppingItem (GameObject newItem);	
}
