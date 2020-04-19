

using System.Collections.Generic;
using UnityEngine;

public class BasePart
{
    protected float _scale = 1F;
    protected float _initialMass = 1F;
    protected List<GameObject> _childGameObjects = new List<GameObject>();
    protected Transform _gameWorldTransform;
    protected GameObject _graphic;
    protected bool _hasGraphic;
    protected Rigidbody2D _rigidBody;
    protected Material _material;

    public Collider2D[] Colliders { get; private set; }
    
    public float Scale
    {
        get => _scale;
        set
        {
            _SetScale(value);
        }
    }
    
    protected void _SetScale(float value)
    {
        this._scale = value;
    }
}