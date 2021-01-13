/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * Flock3DCameraExample.cs
 * 
 * Camera movement for a 3D Flock example.
 */

using UnityEngine;
using System.Collections;



public class Flock3DCameraExample : MonoBehaviour
{
	private float fi;
	private float psi;
	public float r = 35f;
	
	private Transform _transform;
	
	void Start()
	{
		_transform = transform;
	}
	
	void Update ()
	{
		fi += 10f * Time.smoothDeltaTime * Input.GetAxis("Vertical");
		psi += 10f * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
		
		Vector3 newPosition;
		newPosition.z = r*Mathf.Cos(fi) * Mathf.Cos(psi);
		newPosition.x = r*Mathf.Cos(fi) * Mathf.Sin(psi);
		newPosition.y = r*Mathf.Sin(fi);
		
		_transform.localPosition = newPosition;
		
		_transform.LookAt(Vector3.zero, Vector3.up);
	}
}
