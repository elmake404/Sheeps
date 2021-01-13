/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockMemberMoveExample.cs
 * 
 * Example script that moves a flock member using flockOutput vector returned by a FlockMember component.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FlockMemberMoveExample : MonoBehaviour
{
	public float maxSpeed;
	private Transform _transform;
	private FlockMember _flockMember;
	
	void Start()
	{
		_transform = transform;
		_flockMember = GetComponent<FlockMember>();
	}
	
	void Update()
	{
		if (_flockMember.flockOutput != Vector3.zero)
			Debug.DrawRay(_transform.position, _flockMember.flockOutput, Color.black);
			
		Vector3 speed = _flockMember.flockOutput;
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
