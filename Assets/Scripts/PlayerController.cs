using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // the movement speed of the player
    public float movementSpeed;
    // the turning speed of the player
    public float turnSpeed;
    // determines the maximum turning speed for the player, to somewhat reduce choppiness
    public float maxTurnDistance;

    // charactercontroller component
    private CharacterController characterController;
    // vector of movement, determined by player input
    private Vector2 movementVector;
    // vector for looking around, determined by player input
    private Vector2 lookingVector;
    // strength of gravity, which is later multiplied by player speed
    private float gravity = -1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // cache the character controller
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the player to look left and right
        transform.Rotate(Time.deltaTime * turnSpeed * new Vector3(0, Mathf.Clamp(lookingVector.x, -maxTurnDistance, maxTurnDistance), 0));

        // change the movement vector to be relative to the player's forward facing direction
        Vector3 relMovementVector = transform.TransformDirection(new Vector3(movementVector.x, gravity, movementVector.y));
        // move the player via the character controller using the relative movement vector while applying a downwards force
        characterController.Move(movementSpeed * Time.deltaTime * relMovementVector);
    }

    void OnMove(InputValue movementValue)
    {
        // store player input in a vector
        movementVector = movementValue.Get<Vector2>();
    }

    void OnLook(InputValue lookValue)
    {
        lookingVector = lookValue.Get<Vector2>();
    }

}
