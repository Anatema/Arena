using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUI : MonoBehaviour
{
    public static InputUI Instance;
    [SerializeField]
    private VariableJoystick _joystick;
    public VariableJoystick Joystick => _joystick;

    private bool _pressed = false;
    public bool Pressed => _pressed;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerDown()
    {
        _pressed = true;
    }
    public void OnPointrUp()
    {
        _pressed = false;
    }
}
