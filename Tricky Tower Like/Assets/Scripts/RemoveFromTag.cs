using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFromTag : MonoBehaviour
{
    [SerializeField] private string tag = "Untagged";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tag))
            Destroy(other.gameObject);
    }
}
