using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float moveSpeed = 10;
    private Vector2 vector;
    private Vector3 moveDirection;
    
    void Update()
    {
        moveDirection = new(vector.x, 0f ,vector.y);
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);
    }
    //dd

    void OnMove(InputValue value) => vector = value.Get<Vector2>();
}