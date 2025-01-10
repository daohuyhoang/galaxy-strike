using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xClampRange = 5f;
    [SerializeField] float yClampRange = 5f;
    [SerializeField] float controlRollFactor = 20f;
    [SerializeField] float controlPitchFactor = 18f;
    [SerializeField] float rotationSpeed = 10f;
    
    Vector2 movement;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessTranslation()
    {
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xClampRange, xClampRange);
        
        float yOffset = movement.y * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yClampRange, yClampRange);
        
        transform.localPosition = new Vector3(clampedXPos,clampedYPos , 0f);
    }

    void ProcessRotation()
    {
        float roll = -movement.x * controlRollFactor;
        float pitch = -movement.y * controlPitchFactor;
        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, roll);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
}
