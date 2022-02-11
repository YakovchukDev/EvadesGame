
using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractionWithTag : MonoBehaviour
{
    private int Level = 1;
    [SerializeField] private GameObject Father;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Next lvl")
        {
            LevelSystem();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.tag == "Slower")
        {
            JoystickPlayerExample.speed /= 2;
        }
        if (other.gameObject.tag == "Faster")
        {
            JoystickPlayerExample.speed *= 2;
        }
        if (other.gameObject.tag == "GrowUp")
        {
            Father.transform.localScale=new Vector3(2,2,2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slower")
        {
            JoystickPlayerExample.speed *= 2;
        }
        if (other.gameObject.tag == "Faster")
        {
            JoystickPlayerExample.speed /= 2;
        }
        if (other.gameObject.tag == "GrowUp")
        {
            if (other.gameObject.tag == "GrowUp")
            {
                Father.transform.localScale = new Vector3(1,1,1);
            }
        }
    }
    private void LevelSystem()
    {
        Level++;

    }
    private void PressForTeleport()
    {
       /* Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
       transform.position=new Vector3()*/

    }
}
