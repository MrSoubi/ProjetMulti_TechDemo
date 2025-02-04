using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_PlayerSpawn", menuName = "Data/RSE/PlayerSpawn")]
public class RSE_PlayerSpawn : ScriptableObject
{
    public Action<Transform> onTrigger;
}