using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSO_PlayerPosition", menuName = "Data/RSO/PlayerPosition")]
public class RSO_PlayerPosition : ScriptableObject
{
    public Action<Vector3> onValueChanged;

    [ShowInInspector]
    private Vector3 _value;

    public Vector3 Value
    {
        get => _value;
        set
        {
            if (_value == value) return;

            _value = value;
            onValueChanged?.Invoke(_value);
        }
    }
}