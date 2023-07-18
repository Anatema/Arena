using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public VariableJoystick Joystick;
    public float Speed;
    public float angle;

    // Start is called before the first frame update
    private void Awake()
    {
        Joystick = InputUI.Instance.Joystick;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Joystick.Direction.x, 0, Joystick.Direction.y) * Time.deltaTime * Speed, Space.World);
        if(Joystick.Direction.x == 0 && Joystick.Direction.y == 0)
        {
            return;
        }
        angle = Vector2.SignedAngle(Joystick.Direction, new Vector2(0, 1));
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}
