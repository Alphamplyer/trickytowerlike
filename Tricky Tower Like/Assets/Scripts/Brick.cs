using System;
using UnityEngine;


public class Brick : MonoBehaviour
{
    public event Action<Brick> OnCollide;

    public bool placed = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("BrickAndGround"))
            return;
        
        OnCollide?.Invoke(this);
    }
}