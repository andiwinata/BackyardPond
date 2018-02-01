using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderBehaviour : BaseBehaviour 
{
	private Waypoint[] waypoints;
	private int currId = int.MaxValue;

	private bool behavEnabled;

	private Vector3 wanderingTarget;
	private float wanderSpeed;
	private float minSpeed = 0.5f;
	private float maxSpeed = 3f;

	protected override void Awake()
	{
		base.Awake();
		waypoints = GameObject.FindObjectsOfType<Waypoint>();
	}

	public override void EnableBehaviour ()
	{
		DisableBehaviour(); //avoid duplicate of coroutine
		behavEnabled = true;
		StartCoroutine("Wandering");
	}

	public override void DisableBehaviour ()
	{
		behavEnabled = false;
		currId = int.MaxValue;
	}

	private IEnumerator Wandering()
	{
		StartCoroutine("RandomPoint");
		StartCoroutine("SetSpeed");
		while (behavEnabled)
		{
			Vector3 distToTarget = wanderingTarget - transform.position;
			distToTarget.y = 0;

			Quaternion facing = Quaternion.LookRotation(distToTarget);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, 50 * Time.deltaTime);

			Vector3 movingDirection = transform.TransformDirection(Vector3.forward);

			base.charCont.Move(movingDirection * wanderSpeed * Time.deltaTime);
			yield return null;

		}
	}

	private IEnumerator SetSpeed()
	{
		while (behavEnabled)
		{
			//make curve for speed
			float maxCurvePoint = Random.Range(minSpeed, maxSpeed);
			float randDuration = Random.Range(2f, 5f);
			float rate = 1/randDuration;
			
			float i = 0;
			while (i < 1)
			{
				i += rate * Time.deltaTime;
				
				float j;
				if (i <= 0.5f) //upward curve before halfway
					j = i * 2;
				else //downward curve after halfway back to 0
					j = 2 - (i * 2);
				
				wanderSpeed = Mathf.SmoothStep(0, maxCurvePoint, j); //producing mountain curve 0..1..0
				yield return null;
			}

			//stay still
			yield return new WaitForSeconds(Random.Range(0f, 1f));
		}

	}

	private IEnumerator RandomPoint()
	{
		while (behavEnabled)
		{
			wanderingTarget = GetNextPoint();
			yield return new WaitForSeconds(Random.Range(1f, 5f));
		}
	}

	private Vector3 GetNextPoint()
	{
		int newId;
		//get new waypoint id which is not same as prev waypoint
		do
		{
			newId = Random.Range(0, waypoints.Length);
		}
		while (newId == currId);

		currId = newId;

		Vector3 targetPoint = waypoints[newId].GetRandomPoint();
		return new Vector3(targetPoint.x, transform.position.y, targetPoint.z);
	}

	private bool RandomChance(int percentage)
	{
		return Random.Range (0, 100) < percentage;
	}
}
