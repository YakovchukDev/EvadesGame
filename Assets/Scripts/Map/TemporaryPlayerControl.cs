using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlayerControl : MonoBehaviour
{
    public Rigidbody _rig;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float _v = Input.GetAxis("Vertical");
        float _h = Input.GetAxis("Horizontal");
        Vector3 v3 = new Vector3(_h, 0, _v).normalized;
        _rig.velocity = v3 * speed;
    }
}
