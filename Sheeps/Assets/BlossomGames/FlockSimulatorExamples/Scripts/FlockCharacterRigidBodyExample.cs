/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockCharacterRigidBodyExample.cs
 * 
 * Example script that controls the character using a rigid body. It also debug draws flockOutput vector in the scene window.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FlockCharacterRigidBodyExample : MonoBehaviour
{
	public float force = 20f;
	
	private Transform _transform;
	private FlockMember _flockMember;
	private Rigidbody _rigidBody;
	
	void Start()
	{
		_transform = transform;
		_flockMember = GetComponent<FlockMember>();
		_rigidBody = rigidbody;
	}
	
	void Update()
	{
		if (_flockMember.flockOutput != Vector3.zero)
			Debug.DrawRay(_transform.position, _flockMember.flockOutput, Color.black);
			
		Vector3 f = Vector3.zero;
		f.x = force * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
		f.y = force * Time.smoothDeltaTime * Input.GetAxis("Vertical");
		
		_rigidBody.AddForce(f, ForceMode.Force);
	}
		
}
