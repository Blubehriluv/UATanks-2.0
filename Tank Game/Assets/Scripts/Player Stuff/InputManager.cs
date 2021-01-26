using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TankMotor motor;
    public static float playerForwardSpeed;
    public static float playerBackSpeed;
    public static float playerTurnSpeed;
    public enum InputScheme { WASD, arrowKeys };
    public InputScheme input = InputScheme.WASD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (input)
        {
            case InputScheme.arrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.Move(playerForwardSpeed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.Move(-playerBackSpeed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Rotate(playerTurnSpeed);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(-playerTurnSpeed);
                }
                break;

            case InputScheme.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    motor.Move(playerForwardSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    motor.Move(-playerBackSpeed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    motor.Rotate(playerTurnSpeed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(-playerTurnSpeed);
                }
                break;
        }
    }
}
