using System;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private float brickRotationDegrees = 90.0F;

    [SerializeField] private float brickMoveStep = 0.5F;
    [SerializeField] private float brickNudgeStep = 1.0F;

    [Header("Brick Drop Settings")] 
    [SerializeField] private float brickDropAcceleration = 60.0F;
    [SerializeField] private float brickDropDeceleration = 100.0F;
    [SerializeField] private float brickMinDropBonusSpeed;
    [SerializeField] private float brickMaxDropBonusSpeed = 60F;
    [SerializeField] private float brickDefaultDropSpeed = 60F;
    private float _brickDropBonusSpeed;

    [Header("Brick Move Settings")] 
    [SerializeField] private float brickMoveDelayMax = 0.2F;
    [SerializeField] private float brickMoveDelayMin = 0.05F;
    [SerializeField] private float brickMoveDelayFactor = 0.1F;
    private float _brickMoveDelay;
    private float _brickMoveCooldown;

    private const float _BRICK_MAX_DROP_SPEED = 60.0F;
    private const float _BRICK_NUDGE_COOLDOWN = 0.0F;

    private Brick _brick;
    private Rigidbody2D _rigidbody2D;
    private bool _correctBrick = true;
    private bool _paused = false;

    public void SetBrick(Brick brick)
    {
        _brick = brick;
        _correctBrick = brick != null;
        _rigidbody2D = brick.GetComponent<Rigidbody2D>();
        _brickMoveDelay = brickMoveDelayMax;
        _brickDropBonusSpeed = brickMinDropBonusSpeed;
    }

    public void Update()
    {
        if (_paused)
            return;

        if (!_correctBrick)
            return;

        if (_brick.placed)
            return;

        UpdateHorizontalMovement();
        UpdateDownMovement();
        UpdateRotation();
    }
    
    private void UpdateHorizontalMovement()
    {
        _brickMoveCooldown += Time.deltaTime;
        if (_brickMoveCooldown < _brickMoveDelay)
            return;

        var value = Mathf.FloorToInt(Input.GetAxisRaw("Horizontal"));
        _brick.transform.Translate(Vector3.right * (value * brickMoveStep), Space.World);
        
        if (value == 0)
        {
            _brickMoveDelay = brickMoveDelayMax;
        }
        else
        {
            _brickMoveDelay = MathUtils.DecreaseTimer(_brickMoveDelay, brickMoveDelayMin,
                brickMoveDelayFactor * Time.deltaTime);
            _brickMoveCooldown = 0.0F;
        }
    }

    private void UpdateDownMovement()
    {
        var value = Input.GetAxis("Vertical");

        if (value < 0)
        {
            _brickDropBonusSpeed = MathUtils.IncreaseTimer(_brickDropBonusSpeed, brickMaxDropBonusSpeed,
                brickDropAcceleration * Time.deltaTime);
        }
        else if (value >= 0)
        {
            value = 0;
            _brickDropBonusSpeed = MathUtils.DecreaseTimer(_brickDropBonusSpeed, brickMinDropBonusSpeed,
                brickDropDeceleration * Time.deltaTime);
        }
        
        _rigidbody2D.velocity = Vector3.down * ((brickDefaultDropSpeed + (_brickDropBonusSpeed * Mathf.Abs(value))) * Time.deltaTime);

    }

    private void UpdateRotation()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            _brick.transform.Rotate(new Vector3(0,0, brickRotationDegrees));
        }
    }
}