using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

public class T_HealthComponent
{
    [Test]
    public void TestTestTest()
    {
        Assert.AreEqual(10, 10);
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator T_HealthComponentWithEnumeratorPasses()
    {
        GameObject go = new GameObject();
        HealthComponent healthComponent = go.AddComponent<HealthComponent>();

        // Reflection
        Type type = healthComponent.GetType();

        // Properties
        BindingFlags propertyFlags = BindingFlags.NonPublic
            | BindingFlags.Instance
            | BindingFlags.SetField
            | BindingFlags.SetProperty;

        FieldInfo currentHealth = type.GetField("currentHealth", propertyFlags);
        FieldInfo maxHealth = type.GetField("maxHealth", propertyFlags);

        maxHealth.SetValue(healthComponent, 50);

        yield return null;

        healthComponent.TakeDamage(20);

        // Assert
        Assert.AreEqual(30, currentHealth.GetValue(healthComponent));
    }
}
