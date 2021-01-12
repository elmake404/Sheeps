using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerControl : MonoBehaviour
{
    private Vector2 _startMosePos, _currentMosePos;
    private Camera _cam;

    [SerializeField]
    private float _joystickSensitivity;
    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMosePos = _cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            if (_startMosePos == Vector2.zero)
            {
                _startMosePos = _cam.ScreenToViewportPoint(Input.mousePosition);
            }

            _currentMosePos = _cam.ScreenToViewportPoint(Input.mousePosition);

            if (((_currentMosePos.x - _startMosePos.x) * _joystickSensitivity) > 1)
            {
                float xStart = ((_currentMosePos.x - _startMosePos.x) > 0 ? 1f / _joystickSensitivity : -(1f / _joystickSensitivity));
                _startMosePos.x = _currentMosePos.x - xStart;
            }

            if ((_currentMosePos - _startMosePos).magnitude > 0.01f)
            {
                Debug.Log((_currentMosePos - _startMosePos));
            }
        }
    }
}
