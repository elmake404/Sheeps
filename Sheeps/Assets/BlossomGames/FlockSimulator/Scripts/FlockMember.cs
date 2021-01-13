/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockMember.cs
 * 
 * Component that should be attached to every game object that wants to behave like a member of a flock.
 * 
 * The result of this script's execution is stored in flockOutput vector. It represents which direction
 * and how much the object wants to move because of the flock instinct. Read flockOutput in another script
 * and do whatever you want with it.
 *
 * See Flock Simulator Examples when in doubt.
 */

using UnityEngine;
using System.Collections;



public class FlockMember : MonoBehaviour
{
	public FlockGroup flockGroup;

	private Vector3 _velocity; public Vector3 velocity { get { return _velocity; } }
	public float gatherVelocityAutomaticallyPeriod = 0.1f;

	public Vector3 flockOutput;
	

	
	private FlockGroup lastFlockGroup;
	
	private Vector3 lastGlobalPosition;

	private float gatherVelocityAutomaticallyTimeCount;
	private bool gatherVelocityAutomaticallyFirstPeriod;
	
	private float lastDirectionChangeTime;
	private bool firstGetRandomVector;
	private Vector3 randomVector;
	
	private Transform _transform; // cache our transform for optimisation
	
	
	
	void OnEnable()
	{
		TrackFlockGroup();

		_transform = transform;
		lastGlobalPosition = _transform.position;
		
		// we do that to start with a random time count so that every member won't update its velocity at the same time
		// to prevent uneven frame execution times
		gatherVelocityAutomaticallyTimeCount = Random.Range(0f, gatherVelocityAutomaticallyPeriod);
		gatherVelocityAutomaticallyFirstPeriod = true;
		firstGetRandomVector = true;
	}
	
	void OnDisable()
	{
		FlockGroup backup = flockGroup;
		flockGroup = null;
		TrackFlockGroup();
		flockGroup = backup;
	}
	
	void TrackFlockGroup()
	{
		if (flockGroup == lastFlockGroup)
			return;
		
		if (lastFlockGroup != null)
			lastFlockGroup.RemoveMember(this);
			
		if (flockGroup != null)
			flockGroup.AddMember(this);
			
		lastFlockGroup = flockGroup;
	}
	
	void LateUpdate()
	{
		TrackFlockGroup();
		
		gatherVelocityAutomaticallyTimeCount += Time.deltaTime;
		if (gatherVelocityAutomaticallyTimeCount >= gatherVelocityAutomaticallyPeriod)
		{
			if (gatherVelocityAutomaticallyFirstPeriod == false)
				_velocity = (_transform.position - lastGlobalPosition) / gatherVelocityAutomaticallyTimeCount;
			gatherVelocityAutomaticallyTimeCount = 0f;
			gatherVelocityAutomaticallyFirstPeriod = false;
			lastGlobalPosition = _transform.position;
		}
	}
	
	// that's used by FlockGroup
	public Vector3 GetRandomVector(float period)
	{
		if (firstGetRandomVector)
		{
			firstGetRandomVector = false;
			lastDirectionChangeTime = Time.timeSinceLevelLoad - Random.Range(0f, period);
			randomVector.x = Random.value - 0.5f;
			randomVector.y = Random.value - 0.5f;
			randomVector.z = Random.value - 0.5f;
		}
		
		if (Time.timeSinceLevelLoad - lastDirectionChangeTime > period)
		{
			lastDirectionChangeTime = Time.timeSinceLevelLoad;
			randomVector.x = Random.value - 0.5f;
			randomVector.y = Random.value - 0.5f;
			randomVector.z = Random.value - 0.5f;
		}
		
		return randomVector;
	}
	
	// don't call it by hand unless you know what you're doing
	public void RandomPeriodChanged()
	{
		firstGetRandomVector = true;
	}
}
