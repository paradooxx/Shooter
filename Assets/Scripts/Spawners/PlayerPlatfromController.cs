using UnityEngine;

public class PlayerPlatfromController : MonoBehaviour
{
    public static bool isDragging = false;
    public static bool isMovingRight;
    private float offsetX;
    private Vector3 previousPosition;

    [SerializeField] private float sideLimit = 5f;

    private Transform rightMostPlayer;
    private Transform leftMostPlayer;

    private void Update()
    {
        if(GameStateManager.CurrentGameState == GameState.Game)
        {
            FindLeftAndRightMostPlayer();
            if(leftMostPlayer && rightMostPlayer)
            {
                MovePlatform();
            }
        }
    }

    private void FindLeftAndRightMostPlayer()
    {
        if(transform.childCount > 0)
        {
            leftMostPlayer = transform.GetChild(0);
            rightMostPlayer = transform.GetChild(0);

            foreach (Transform child in transform)
            {
                if (child.position.x < leftMostPlayer.position.x)
                {
                    leftMostPlayer = child;
                }

                if (child.position.x > rightMostPlayer.position.x)
                {
                    rightMostPlayer = child;
                }
            }
        }
        else
        {
            leftMostPlayer = null;
            rightMostPlayer = null;
        }
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
        }

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            Vector3 newPosition = new Vector3(mousePosition.x + offsetX, transform.position.y, transform.position.z);

            if (leftMostPlayer == null || rightMostPlayer == null)
            {
                return;
            }

            if (leftMostPlayer.position.x <= -sideLimit && newPosition.x < previousPosition.x)
            {
                newPosition.x = previousPosition.x;
            }
            else if (rightMostPlayer.position.x >= sideLimit && newPosition.x > previousPosition.x)
            {
                newPosition.x = previousPosition.x;
            }

            transform.position = newPosition;
            previousPosition = newPosition;
        }
    }
}
