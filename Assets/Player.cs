using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private readonly float moveSpeed = 8;
    private Vector2 vector;
    private Vector3 moveDirection;
    
    void Update()
    {
        moveDirection = new(vector.x, 0f ,vector.y);
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);
    }

    void OnMove(InputValue value) => vector = value.Get<Vector2>();
}
