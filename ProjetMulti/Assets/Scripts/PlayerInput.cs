using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private List<CustomInput> inputs = new List<CustomInput>();

	private void Update()
	{
		foreach (CustomInput input in inputs)
		{
			if (Input.GetKey(input.keyCode))
			{
				input.callable.Invoke();
			}
		}
	}
}

[Serializable]
public struct CustomInput
{
	public KeyCode keyCode;
	public UnityEvent callable;
}
