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
				input.onKey.Invoke();
			}
			if (Input.GetKeyUp(input.keyCode))
			{
				input.onKeyUp.Invoke();
			}
            if (Input.GetKeyDown(input.keyCode))
            {
                input.onKeyDown.Invoke();
            }
        }
	}
}

[Serializable]
public struct CustomInput
{
	public KeyCode keyCode;
	public UnityEvent onKeyDown;
	public UnityEvent onKeyUp;
	public UnityEvent onKey;
}
