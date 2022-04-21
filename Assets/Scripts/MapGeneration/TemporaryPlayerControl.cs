using UnityEngine;

public class TemporaryPlayerControl : MonoBehaviour
{
    public Rigidbody _rig;
    public int speed;
    
    void FixedUpdate()
    {
        float _v = Input.GetAxis("Vertical");
        float _h = Input.GetAxis("Horizontal");
        Vector3 v3 = new Vector3(_h, 0, _v).normalized;
        _rig.velocity = v3 * speed;
    }
}
