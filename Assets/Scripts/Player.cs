using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed  = 10f;
    [SerializeField] private float rotateSpeed = 10f;
    
    [SerializeField] private GameInput gameInput;
    
    private bool  _isWalking;
    private float _playerHeight = 2f;
    private float _playerRadius = .7f;
    private void Update()
    { 
        // Handle Player Move inputs
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        // Movement Direction Vector
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        
        /*Change position (Framerate independent => Time.deltaTime)
         CapsuleCast is one way of applying collision*/
        float moveDistance = _moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, 
                                            transform.position + Vector3.up * _playerHeight, 
                                            _playerRadius,
                                            moveDirection,
                                            moveDistance
                                            );
        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, 
                _playerRadius,
                moveDirectionX,
                moveDistance
            );

            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, 
                    _playerRadius,
                    moveDirectionZ,
                    moveDistance
                );

                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }
        
        if (canMove) { transform.position += moveDirection * moveDistance; }
        
        
        _isWalking = moveDirection != Vector3.zero;
        // Rotate player in the direction that player is moving
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking; 
    }
}