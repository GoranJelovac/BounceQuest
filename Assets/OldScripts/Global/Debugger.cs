using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    [SerializeField] private GameObject _debugTextPrefab;
    [SerializeField] private GameObject _debugTextParent;

    private void OnEnable()
    {
        Events.Instance.DebugEvent += ShowText;
    }

    private void OnDisable()
    {
        Events.Instance.DebugEvent -= ShowText;
    }

    private void ShowText(string text)
    {
        GameObject debugText = Instantiate(_debugTextPrefab);
        debugText.transform.SetParent(_debugTextParent.transform);
        Text t = debugText.GetComponent<Text>();
        t.text = text;
    }

}
