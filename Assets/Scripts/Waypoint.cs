using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour 
{
	private float halfWidth;
	private float halfLength;

	void Awake () 
	{
		halfWidth = transform.localScale.x / 2;
		halfLength = transform.localScale.z / 2;
	}
	
	public Vector3 GetRandomPoint()
	{
		float randomX = Random.Range(transform.position.x - halfWidth, transform.position.x + halfWidth);
		float randomZ = Random.Range(transform.position.z - halfLength, transform.position.z - halfLength);
		return new Vector3 (randomX, 0, randomZ);
	}
}
