using System;
using UnityEngine;

public class EducationTeleportControl : MonoBehaviour
{
    public static event Action<bool> OpenTeleportMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenTeleportMenu?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenTeleportMenu?.Invoke(false);
        }
    }
}