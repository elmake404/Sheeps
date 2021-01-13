/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockMemberWithPredatorsMove3DExample.cs
 * 
 * Example script that moves a flock member using flockOutput vector returned by a FlockMember component.
 * It doesn't only use the flock instinct, but also tries to run away from predators.
 * This object moves in 3D.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FlockMemberWithPredatorsMove3DExample : MonoBehaviour
{
	public float maxSpeed;
	private Transform _transform;
	private FlockMember _flockMember;
	
	public FlockGroup predatorsFlock;
	public float runAwayFactor = 1f;
	public float runAwayDistance = 10f;
	
	void Start()
	{
		_transform = transform;
		_flockMember = GetComponent<FlockMember>();
	}
	
	void Update()
	{
		if (_flockMember.flockOutput != Vector3.zero)
			Debug.DrawRay(_transform.position, _flockMember.flockOutput, Color.black);
			
		Vector3 runAwayVector = Vector3.zero;
		if (predatorsFlock != null)
		{
			float minDistance = runAwayDistance;
			Vector3 targetPosition;
			
			foreach (FlockMember fm in predatorsFlock.members)
			{
				float distance = Vector3.Distance(_transform.position, fm.transform.position);
				if (distance < minDistance)
				{
					minDistance = distance;
					targetPosition = fm.transform.position;
				}
			}
			
			if (minDistance < runAwayDistance)
				runAwayVector = -(targetPosition - _transform.position).normalized * runAwayFactor;
		}
			
		if (runAwayVector != Vector3.zero)
			Debug.DrawRay(_transform.position, runAwayVector, Color.magenta);
		
		Vector3 speed = _flockMember.flockOutput;
		speed += runAwayVector;
		
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
