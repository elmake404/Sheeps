/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockGroup.cs
 * 
 * Component that represents a flock. It can be attached to a dummy object. All flock members have to
 * reference to a group to function properly.
 *
 * The component stores flock properties that describes four basic flock behaviours:
 * - alignment: steer towards the average heading of local flockmates
 * - separation: steer to avoid crowding local flockmates
 * - cohesion: steer to move toward the average position of local flockmates
 * - random: steer randomly
 * 
 * See Flock Simulator Examples when in doubt.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;



public enum FlockAlgorithm
{
	Linear, ByCurve
}



public class FlockGroup : MonoBehaviour
{
	public float quality = 1f;
	
	public bool alignment = true;
	public float alignmentWeight = 1f;
	public FlockAlgorithm alignmentAlgorithm = FlockAlgorithm.Linear;
	public AnimationCurve alignmentCurve;
	public float alignmentLinearMaxDistance = 5f;
	public float alignmentLinearFactorAt0 = 1f;
	
	public bool separation = true;
	public float separationWeight = 1f;
	public FlockAlgorithm separationAlgorithm = FlockAlgorithm.Linear;
	public AnimationCurve separationCurve;
	public float separationLinearMaxDistance = 5f;
	public float separationLinearFactorAt0 = 1f;
	
	public bool cohesion = true;
	public float cohesionWeight = 1f;
	public float cohesionDistance;
	public FlockAlgorithm cohesionAlgorithm = FlockAlgorithm.Linear;
	public AnimationCurve cohesionCurve;
	public float cohesionLinearMaxDistance = 5f;
	public float cohesionLinearFactorAt0 = 1f;
	
	public bool random = true;
	public float randomWeight = 1f;
	public float randomPeriod = 0.5f;
	private float lastRandomPeriod;

	public ReadOnlyCollection<FlockMember> members { get { return _members.AsReadOnly(); } }

	
	
	List<FlockMember> _members = new List<FlockMember>();
	
	int index;
	
	
	
	// called by FlockMember. Don't call it by hand
	public void AddMember(FlockMember fm)
	{
		_members.Add(fm);
	}
	
	// called by FlockMember. Don't call it by hand
	public void RemoveMember(FlockMember fm)
	{
		_members.Remove(fm);
	}
	
	void Start()
	{
		index = 0;
		lastRandomPeriod = randomPeriod;
	}
	
	void LateUpdate()
	{
		// lower quality means lower number of members we update in one frame
		// quality == 1 means that we update all of them every frame
		
		if (quality > 1f) // just to be sure we don't do anything stupid
			quality = 1f;
			
		for (int updateCount = (int)((float)_members.Count * quality); updateCount > 0; updateCount--)
		{
			if (_members[index] == null)
			{
				index++;
				continue;
			}
			
			Vector3 outputVector = Vector3.zero;
			Vector3 cohesionCenter = Vector3.zero;
			int cohesionCount = 0;
			
			for (int i=0; i<_members.Count; i++)
			{
				if (_members[i] == null || i == index)
					continue;
				
				float distance = Vector3.Distance(_members[index].transform.position, _members[i].transform.position);
				
				if (alignment)
				{
					if (alignmentAlgorithm == FlockAlgorithm.Linear)
					{
						if (distance < alignmentLinearMaxDistance)
						{
							outputVector += alignmentWeight * _members[i].velocity * (-(alignmentLinearFactorAt0/alignmentLinearMaxDistance)*distance + alignmentLinearFactorAt0);
						}
					}
					else
						outputVector += alignmentWeight * _members[i].velocity * alignmentCurve.Evaluate(distance);
				}
				
				if (separation)
				{
					Vector3 separationVector = _members[index].transform.position - _members[i].transform.position;
					
					if (separationAlgorithm == FlockAlgorithm.Linear)
					{
						if (distance < separationLinearMaxDistance)
						{
							outputVector += separationWeight * separationVector * (-(separationLinearFactorAt0/separationLinearMaxDistance)*distance + separationLinearFactorAt0);
						}
					}
					else
						outputVector += separationWeight * separationVector * separationCurve.Evaluate(distance);
				}
				
				if (cohesion)
				{
					if (distance <= cohesionDistance)
					{
						cohesionCount++;
						cohesionCenter += _members[i].transform.position; // used to calculate the average position
					}
				}
				
			}
		
			if (cohesion)
			{
				cohesionCenter /= (float)cohesionCount; // get average position
				Vector3 cohesionVector = cohesionCenter - _members[index].transform.position;
				float cohesionDistance = cohesionVector.magnitude;
				
				if (cohesionAlgorithm == FlockAlgorithm.Linear)
				{
					if (cohesionDistance < cohesionLinearMaxDistance)
					{
						outputVector += cohesionWeight * cohesionVector * (-(cohesionLinearFactorAt0/cohesionLinearMaxDistance)*cohesionDistance + cohesionLinearFactorAt0);
					}
				}
				else
					outputVector += cohesionWeight * cohesionVector * cohesionCurve.Evaluate(cohesionDistance);
			}
			if (random)
			{
				outputVector += randomWeight * _members[index].GetRandomVector(randomPeriod);
			}
			
			_members[index].flockOutput = outputVector;
		
			if (++index >= _members.Count)
				index = 0;
		}
		
		// check if randomPeriod was changed.
		if (lastRandomPeriod != randomPeriod)
		{
			for (int i=0; i<_members.Count; i++)
				_members[i].RandomPeriodChanged();
			
			lastRandomPeriod = randomPeriod;
		}
	}
}
