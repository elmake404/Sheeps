/*
 * Flock Simulator
 * (c) 2013 by Blossom Games -- http://blossom-games.com/
 * 
 * 
 * 
 * FlockGroupEditor.cs
 * 
 * Editor class for FlockGroup. We just make it look prettier.
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;



[CanEditMultipleObjects]
[CustomEditor(typeof(FlockGroup))]
public class FlockGroupEditor : Editor
{
	SerializedProperty pQuality;
	
	SerializedProperty pAlignment;
	SerializedProperty pAlignmentWeight;
	SerializedProperty pAlignmentAlgorithm;
	SerializedProperty pAlignmentCurve;
	SerializedProperty pAlignmentLinearMaxDistance;
	SerializedProperty pAlignmentLinearFactorAt0;
	
	SerializedProperty pSeparation;
	SerializedProperty pSeparationWeight;
	SerializedProperty pSeparationAlgorithm;
	SerializedProperty pSeparationCurve;
	SerializedProperty pSeparationLinearMaxDistance;
	SerializedProperty pSeparationLinearFactorAt0;
	
	SerializedProperty pCohesion;
	SerializedProperty pCohesionDistance;
	SerializedProperty pCohesionWeight;
	SerializedProperty pCohesionAlgorithm;
	SerializedProperty pCohesionCurve;
	SerializedProperty pCohesionLinearMaxDistance;
	SerializedProperty pCohesionLinearFactorAt0;
	
	SerializedProperty pRandom;
	SerializedProperty pRandomWeight;
	SerializedProperty pRandomPeriod;
	
	void OnEnable()
	{
		pQuality = serializedObject.FindProperty("quality");
		
		pAlignment = serializedObject.FindProperty("alignment");
		pAlignmentWeight = serializedObject.FindProperty("alignmentWeight");
		pAlignmentAlgorithm = serializedObject.FindProperty("alignmentAlgorithm");
		pAlignmentCurve = serializedObject.FindProperty("alignmentCurve");
		pAlignmentLinearMaxDistance = serializedObject.FindProperty("alignmentLinearMaxDistance");
		pAlignmentLinearFactorAt0 = serializedObject.FindProperty("alignmentLinearFactorAt0");
		
		pSeparation = serializedObject.FindProperty("separation");
		pSeparationWeight = serializedObject.FindProperty("separationWeight");
		pSeparationAlgorithm = serializedObject.FindProperty("separationAlgorithm");
		pSeparationCurve = serializedObject.FindProperty("separationCurve");
		pSeparationLinearMaxDistance = serializedObject.FindProperty("separationLinearMaxDistance");
		pSeparationLinearFactorAt0 = serializedObject.FindProperty("separationLinearFactorAt0");
		
		pCohesion = serializedObject.FindProperty("cohesion");
		pCohesionDistance = serializedObject.FindProperty("cohesionDistance");
		pCohesionWeight = serializedObject.FindProperty("cohesionWeight");
		pCohesionAlgorithm = serializedObject.FindProperty("cohesionAlgorithm");
		pCohesionCurve = serializedObject.FindProperty("cohesionCurve");
		pCohesionLinearMaxDistance = serializedObject.FindProperty("cohesionLinearMaxDistance");
		pCohesionLinearFactorAt0 = serializedObject.FindProperty("cohesionLinearFactorAt0");
		
		pRandom = serializedObject.FindProperty("random");
		pRandomWeight = serializedObject.FindProperty("randomWeight");
		pRandomPeriod = serializedObject.FindProperty("randomPeriod");
	}
	
	void HorizontalLine()
	{
		EditorGUILayout.Separator();
		GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(5f));
		EditorGUILayout.Separator();
	}
	
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		
		EditorGUILayout.PropertyField(pQuality);
		HorizontalLine();

		#region Alignment
		EditorGUILayout.PropertyField(pAlignment);
		if (pAlignment.boolValue == true || pAlignment.hasMultipleDifferentValues)
		{
			EditorGUILayout.PropertyField(pAlignmentWeight);
			EditorGUILayout.PropertyField(pAlignmentAlgorithm);
			if (pAlignmentAlgorithm.enumValueIndex == (int)FlockAlgorithm.Linear || pAlignmentAlgorithm.hasMultipleDifferentValues)
			{
				EditorGUILayout.LabelField("Linear parameters:");
				
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(20f);
				EditorGUILayout.BeginVertical();
				EditorGUILayout.PropertyField(pAlignmentLinearMaxDistance, new GUIContent("Value 0 at distance"));
				EditorGUILayout.PropertyField(pAlignmentLinearFactorAt0, new GUIContent("Value at 0"));
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
			if (pAlignmentAlgorithm.enumValueIndex == (int)FlockAlgorithm.ByCurve || pAlignmentAlgorithm.hasMultipleDifferentValues)
			{
				EditorGUILayout.LabelField("By Curve parameters:");
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(20f);
				EditorGUILayout.BeginVertical();
				
				EditorGUILayout.PropertyField(pAlignmentCurve, new GUIContent("Value at distance"), GUILayout.Height(200f));
				
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
			
		}
		HorizontalLine();
		#endregion
		
		#region Separation
		EditorGUILayout.PropertyField(pSeparation);
		if (pSeparation.boolValue == true || pSeparation.hasMultipleDifferentValues)
		{
			EditorGUILayout.PropertyField(pSeparationWeight);
			EditorGUILayout.PropertyField(pSeparationAlgorithm);
			if (pSeparationAlgorithm.enumValueIndex == (int)FlockAlgorithm.Linear || pSeparationAlgorithm.hasMultipleDifferentValues)
			{
				EditorGUILayout.LabelField("Linear parameters:");
				
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(20f);
				EditorGUILayout.BeginVertical();
				EditorGUILayout.PropertyField(pSeparationLinearMaxDistance, new GUIContent("Value 0 at distance"));
				EditorGUILayout.PropertyField(pSeparationLinearFactorAt0, new GUIContent("Value at 0"));
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
			if (pSeparationAlgorithm.enumValueIndex == (int)FlockAlgorithm.ByCurve || pSeparationAlgorithm.hasMultipleDifferentValues)
			{
				EditorGUILayout.LabelField("By Curve parameters:");
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(20f);
				EditorGUILayout.BeginVertical();
				
				EditorGUILayout.PropertyField(pSeparationCurve, new GUIContent("Value at distance"), GUILayout.Height(200f));
				
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
			
		}
		HorizontalLine();
		#endregion
		
		#region Cohesion
		EditorGUILayout.PropertyField(pCohesion);
		if (pCohesion.boolValue == true || pCohesion.hasMultipleDifferentValues)
		{
			EditorGUILayout.PropertyField(pCohesionWeight);
			EditorGUILayout.PropertyField(pCohesionDistance, new GUIContent("Cohesion Max. Distance"));
			EditorGUILayout.PropertyField(pCohesionAlgorithm);
			if (pCohesionAlgorithm.enumValueIndex == (int)FlockAlgorithm.Linear || pCohesionAlgorithm.hasMultipleDifferentValues)
			{
				EditorGUILayout.LabelField("Linear parameters:");
				
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(20f);
				EditorGUILayout.BeginVertical();
				EditorGUILayout.PropertyField(pCohesionLinearMaxDistance, new GUIContent("Value 0 at distance"));
				EditorGUILayout.PropertyField(pCohesionLinearFactorAt0, new GUIContent("Value at 0"));
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
			if (pCohesionAlgorithm.enumValueIndex == (int)FlockAlgorithm.ByCurve || pCohesionAlgorithm.hasMultipleDifferentValues)
			{
				EditorGUILayout.LabelField("By Curve parameters:");
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(20f);
				EditorGUILayout.BeginVertical();
				
				EditorGUILayout.PropertyField(pCohesionCurve, new GUIContent("Value at distance"), GUILayout.Height(200f));
				
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
			
		}
		HorizontalLine();
		#endregion
		
		#region Random
		EditorGUILayout.PropertyField(pRandom);
		if (pRandom.boolValue == true || pRandom.hasMultipleDifferentValues)
		{
			EditorGUILayout.PropertyField(pRandomWeight);
			EditorGUILayout.PropertyField(pRandomPeriod);
		}
		#endregion
		
		serializedObject.ApplyModifiedProperties();
	}
}
