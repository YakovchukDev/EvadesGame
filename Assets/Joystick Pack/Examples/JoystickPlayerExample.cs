using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public static float speed=100;
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Rigidbody rb;
    public void FixedUpdate()
    {
        MoveCharacter();        
    }
    private void MoveCharacter()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
    
    
}