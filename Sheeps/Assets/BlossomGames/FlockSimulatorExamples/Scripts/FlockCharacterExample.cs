/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockCharacterExample.cs
 * 
 * Example script that controls the character. It also debug draws flockOutput vector in the scene window.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FlockCharacterExample : MonoBehaviour
{
	private Vector3 speed;
	public float maxSpeed = 5f;
	
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
			
		speed.x += 20f * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
		speed.y += 20f * Time.smoothDeltaTime * Input.GetAxis("Vertical");
		
		speed *= 1f - 0.1f * Mathf.Exp(Time.smoothDeltaTime);
		
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
