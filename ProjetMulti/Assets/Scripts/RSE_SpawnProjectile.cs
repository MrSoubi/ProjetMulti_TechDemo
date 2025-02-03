using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_SpawnProjectile", menuName = "Data/RSE/SpawnProjectile")]
public class RSE_SpawnProjectile : ScriptableObject
{
	/// <summary>
	///  Position, direction
	/// </summary>
	public Action<Vector2, Quaternion> TriggerEvent;
}