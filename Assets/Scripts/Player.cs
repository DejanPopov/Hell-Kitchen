using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed   = 10f;
    [SerializeField] private float _rotateSpeed = 10f;
    
   
    
    private void Update()
    { 
        Vector2 _inputVector = new  Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            _inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _inputVector.x = +1;
        }
        _inputVector = _inputVector.normalized;
        
        // Movement Direction Vector
        Vector3 _moveDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
        // Change position (Framerate independent => Time.deltaTime)
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        // Rotate player in the direction that player is moving
        transform.forward = Vector3.Slerp(transform.forward, _moveDirection, Time.deltaTime * _rotateSpeed);

    }
}