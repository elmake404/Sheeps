/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockMemberMoveExample.cs
 * 
 * Example script that moves a flock member using flockOutput vector returned by a FlockMember component. It uses rigid body to move.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FlockMemberRigidBodyExample : MonoBehaviour
{
	public float force = 20f;
	
	private Transform _transform;
	private FlockMember _flockMember;
	private Rigidbody _rigidBody;
	
	void Start()
	{
		_transform = transform;
		_flockMember = GetComponent<FlockMember>();
		_rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update()
	{
		if (_flockMember.flockOutput != Vector3.zero)
			Debug.DrawRay(_transform.position, _flockMember.flockOutput, Color.black);
			
		Vector3 f = force * _flockMember.flockOutput;
		f.z = 0;
		
		_rigidBody.AddForce(f, ForceMode.Force);
	}
}
