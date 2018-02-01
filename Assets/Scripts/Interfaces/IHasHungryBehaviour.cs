using UnityEngine;
using System.Collections;

public interface IHasHungryBehaviour 
{
	HungryBehaviour hungryBehaviour {get; set;}
	void AllowHungryBehavStart();
	void StopHungryBehav();
	void EatFood();
}
