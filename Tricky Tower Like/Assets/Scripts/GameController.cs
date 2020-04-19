using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private BrickController brickController;
    [SerializeField] private Brick[] bricks;

    private Brick _currentBrickEntity = null;
    private int _currentBrickIndex = -1;
    private int _nextBrickIndex = -1;

    private void Start()
    {
        _nextBrickIndex = GetNextBrickIndex();
        GenerateNextBrickEntity();
    }

    private void GenerateNextBrickEntity()
    {
        _currentBrickEntity = Instantiate(bricks[_nextBrickIndex], transform.position, Quaternion.identity);
        _currentBrickIndex = _nextBrickIndex;
        _nextBrickIndex = GetNextBrickIndex();
        
        _currentBrickEntity.OnCollide += OnCollide;
        brickController.SetBrick(_currentBrickEntity);
    }

    private int GetNextBrickIndex()
    {
        var newIndex = Random.Range(0, bricks.Length);
        
        if (newIndex == _currentBrickIndex && bricks.Length != 1)
            newIndex = (newIndex + 1) % bricks.Length;

        return newIndex;
    }
    
    private void OnCollide(Brick brick)
    {
        brick.placed = true;
        brick.OnCollide -= OnCollide;
        brick.GetComponent<Rigidbody2D>().gravityScale = 1;
        GenerateNextBrickEntity();
    }
}