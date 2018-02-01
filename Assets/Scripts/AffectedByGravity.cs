using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CharacterController))]
public class AffectedByGravity : MonoBehaviour 
{
	private float gravity = 10;
	private CharacterController charCont;

	void Awake()
	{
		charCont = GetComponent<CharacterController>();
	}

	void OnEnable()
	{
		StartCoroutine("GravityEffect");
	}

	void OnDisable()
	{
		StopCoroutine("GravityEffect");		
	}

	private IEnumerator GravityEffect()
	{
		float dur = 0;
		while (gameObject.activeInHierarchy)
		{
			dur += Time.deltaTime;

			if (!charCont.isGrounded)
				charCont.Move (Vector3.up * -gravity * Time.deltaTime);

			if (dur > 10)
				break;

			yield return null;
		}
	}
}
