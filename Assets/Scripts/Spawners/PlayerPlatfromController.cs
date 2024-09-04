using UnityEngine;

public class PlayerPlatfromController : MonoBehaviour
{
    public static bool isDragging = false;
    public static bool isMovingRight;
    private float offsetX;
    private Vector3 previousPosition;

    [SerializeField] private float sideLimit = 5f;

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            offsetX = transform.position.x - mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            //animator.Play("PlayerIdle");
        }

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            Vector3 newPosition = new Vector3(mousePosition.x + offsetX, transform.position.y, transform.position.z);

            // Clamp the new position to the defined limits
            newPosition.x = Mathf.Clamp(newPosition.x, -sideLimit, sideLimit);

            if (newPosition.x > previousPosition.x)
            {
                isMovingRight = true;
            }
            else if (newPosition.x < previousPosition.x)
            {
                isMovingRight = false;
            }

            transform.position = newPosition;
            previousPosition = newPosition;
            //PlayerWalkAnimation();dasds
        }
    }
}
