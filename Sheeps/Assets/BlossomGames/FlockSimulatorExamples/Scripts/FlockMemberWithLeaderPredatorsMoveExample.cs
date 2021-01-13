/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockMemberWithLeaderPredatorsMoveExample.cs
 * 
 * Example script that moves a flock member using flockOutput vector returned by a FlockMember component.
 * It doesn't only use the flock instinct, but also tries to run away from predators and follow the leader.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FlockMemberWithLeaderPredatorsMoveExample : MonoBehaviour
{
	public float maxSpeed;
	private Transform _transform;
	private FlockMember _flockMember;
	
	public FlockGroup predatorsFlock;
	public float runAwayFactor = 1f;
	public float runAwayDistance = 10f;
	
	public Transform leader;
	public float leaderFactor = 0.2f;
	public float leaderDistance = 10f;
	
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
			Vector3 targetPosition = Vector3.zero;
			
			foreach (FlockMember fm in predatorsFlock.members) // iterate through the predators
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
		
		Vector3 leaderVector = Vector3.zero;
		if (leader != null && leader.gameObject.activeSelf)
		{
			float distance = Vector3.Distance(_transform.position, leader.position);
			if (distance < leaderDistance)
				 leaderVector = (leader.position - _transform.position).normalized * leaderFactor;
		}
		
		if (leaderVector != Vector3.zero)
			Debug.DrawRay(_transform.position, leaderVector, Color.blue);
		
			
		Vector3 speed = _flockMember.flockOutput;
		speed += runAwayVector;
		speed += leaderVector;
		speed.z = 0f;
		
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
