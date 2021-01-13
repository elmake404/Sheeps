/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockPredatorMove3DExample.cs
 * 
 * Example script that moves a predator using flockOutput vector returned by a FlockMember component (since predators also form a flock).
 * It doesn't only use the flock instinct, but also tries to chase and eat objects that run away.
 * This object moves in 3D.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FlockPredatorMove3DExample : MonoBehaviour
{
	public float maxSpeed;
	private Transform _transform;
	private FlockMember _flockMember;
	
	public FlockGroup victimsFlock;
	public float predatorFactor = 1f;
	public float distanceToEat = 1f;
	
	void Start()
	{
		_transform = transform;
		_flockMember = GetComponent<FlockMember>();
	}
	
	void Update()
	{
		if (_flockMember.flockOutput != Vector3.zero)
			Debug.DrawRay(_transform.position, _flockMember.flockOutput, Color.black);
		
		Vector3 predatorVector = Vector3.zero;
		if (victimsFlock != null)
		{
			float minDistance = Mathf.Infinity;
			Vector3 targetPosition = Vector3.zero;
			
			collectionChanged:;
			foreach (FlockMember fm in victimsFlock.members) // iterate through a victims flock
			{
				float distance = Vector3.Distance(_transform.position, fm.transform.position);
				if (distance <= distanceToEat)
				{
					fm.gameObject.SetActive(false);
					goto collectionChanged;
				}
				if (distance < minDistance)
				{
					minDistance = distance;
					targetPosition = fm.transform.position;
				}
			}
			
			if (minDistance < Mathf.Infinity)
				predatorVector = (targetPosition - _transform.position).normalized * predatorFactor;
		}
			
		if (predatorVector != Vector3.zero)
			Debug.DrawRay(_transform.position, predatorVector, Color.red);
		
		Vector3 speed = _flockMember.flockOutput;
		speed += predatorVector;
		
		if (speed != Vector3.zero)
		{
			if (speed.sqrMagnitude > maxSpeed*maxSpeed)
			{
				speed = speed.normalized*maxSpeed;
			}
		}
		
		_transform.Translate(Time.deltaTime * speed);
		if (_transform.position.magnitude > 15f)
			_transform.position = 15f * _transform.position.normalized;
	}
}
