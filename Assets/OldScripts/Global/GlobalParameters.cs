using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParameters : MonoBehaviour
{
    private static GlobalParameters _instans;
    public static GlobalParameters Instance { get => _instans; }

    public const float minWidth = 0.11f;
    public const float deltaWidth = 0.15f;

    public float precision = 1f;
    public float ballRadius = 0.35f;
    public int fieldsLayerMask = 1 << 6;

    private float _screenWidthWorld;
    public float ScreenWidthWorld { get => _screenWidthWorld; }

    private float _screenHeightWorld;
    public float ScreenHeightWorld { get => _screenHeightWorld; }

    private float _camSize;
    public float CamSize { get => _camSize; }

    private float _camAsp;
    public float CamAsp { get => _camAsp; }

    private void Awake()
    {
        CheckForInstance();
        GetCamWidthAndHeight();
    }

    private void CheckForInstance()
    {
        if (_instans == null)
        {
            _instans = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void GetCamWidthAndHeight()
    {
        _camSize = Camera.main.orthographicSize;
        _camAsp = Camera.main.aspect;
        _screenWidthWorld = _camAsp * _camSize * 2;
        _screenHeightWorld = _camSize * 2;
    }

    public int GetDirectionId(Vector2 dir)
    {
        float angle = Utils.GetDegree(dir);

        if (angle == 0)
        {
            return 0;
        }

        return Mathf.RoundToInt(angle / precision);
    }
}
