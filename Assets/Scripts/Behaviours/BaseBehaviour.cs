using UnityEngine;
using System.Collections;

public abstract class BaseBehaviour : MonoBehaviour 
{
	protected CharacterController charCont;

	public abstract void EnableBehaviour();
	public abstract void DisableBehaviour();

	protected virtual void Awake()
	{
		charCont = GetComponent<CharacterController>();
	}
}

