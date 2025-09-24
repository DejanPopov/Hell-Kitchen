using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed   = 10f;
    [SerializeField] private float rotateSpeed = 10f;
    
    [SerializeField] private GameInput gameInput;
    
    private bool _isWalking;
    private void Update()
    { 
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // Movement Direction Vector
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        // Change position (Framerate independent => Time.deltaTime)
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        _isWalking = moveDirection != Vector3.zero;
        // Rotate player in the direction that player is moving
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking; 
    }
}