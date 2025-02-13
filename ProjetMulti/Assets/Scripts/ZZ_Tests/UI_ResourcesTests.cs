using NUnit.Framework;
using System;
using System.Reflection;
using TMPro;
using UnityEngine;

public class UI_ResourcesTests
{
    private GameObject _gameObject;
    private UI_Resources _uiResources;
    private RSO_ResourceCount _resourceCount;
    private TextMeshProUGUI _text;
    private MethodInfo _onEnableMethod;
    private MethodInfo _onDisableMethod;
    private MethodInfo _updateTextMethod;
    private MethodInfo _startMethod;

    [SetUp]
    public void SetUp()
    {
        _gameObject = new GameObject();
        _uiResources = _gameObject.AddComponent<UI_Resources>();
        _resourceCount = ScriptableObject.CreateInstance<RSO_ResourceCount>();
        _text = new GameObject().AddComponent<TextMeshProUGUI>();

        _uiResources.resourceCount = _resourceCount;

        // Utilisation de la réflexion pour accéder à la propriété 'text'
        var textField = typeof(UI_Resources).GetField("text", BindingFlags.NonPublic | BindingFlags.Instance);
        textField.SetValue(_uiResources, _text);

        // Utilisation de la réflexion pour accéder aux méthodes privées
        _onEnableMethod = typeof(UI_Resources).GetMethod("OnEnable", BindingFlags.NonPublic | BindingFlags.Instance);
        _onDisableMethod = typeof(UI_Resources).GetMethod("OnDisable", BindingFlags.NonPublic | BindingFlags.Instance);
        _updateTextMethod = typeof(UI_Resources).GetMethod("UpdateText", BindingFlags.NonPublic | BindingFlags.Instance);
        _startMethod = typeof(UI_Resources).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(_gameObject);
        UnityEngine.Object.DestroyImmediate(_text.gameObject);
        UnityEngine.Object.DestroyImmediate(_resourceCount);
    }

    [Test]
    public void OnEnable_ShouldSubscribeToResourceCount()
    {
        bool isSubscribed = false;
        _resourceCount.onValueChanged += (value) => isSubscribed = true;

        _onEnableMethod.Invoke(_uiResources, null);
        _resourceCount.onValueChanged.Invoke(0);

        Assert.IsTrue(isSubscribed);
    }

    [Test]
    public void OnDisable_ShouldUnsubscribeFromResourceCount()
    {
        bool isSubscribed = false;
        Action<int> action = (value) => isSubscribed = true;
        _resourceCount.onValueChanged += action;

        _onEnableMethod.Invoke(_uiResources, null);
        _onDisableMethod.Invoke(_uiResources, null);
        _resourceCount.onValueChanged -= action;
        _resourceCount.onValueChanged.Invoke(0);

        Assert.IsFalse(isSubscribed);
    }

    [Test]
    public void UpdateText_ShouldUpdateTextComponent()
    {
        _updateTextMethod.Invoke(_uiResources, new object[] { 10 });
        Assert.AreEqual("Resources: 10", _text.text);
    }

    [Test]
    public void Start_ShouldUpdateTextWithInitialValue()
    {
        _resourceCount.Value = 5;
        _startMethod.Invoke(_uiResources, null);
        Assert.AreEqual("Resources: 5", _text.text);
    }
}
