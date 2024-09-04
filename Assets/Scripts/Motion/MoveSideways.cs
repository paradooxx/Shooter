using System.Collections;
using UnityEngine;

public class MoveSideways : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float moveDuration = 2f; // Duration for each movement direction
    [SerializeField] private bool startMovingLeft = true; // Set initial direction

    private bool movingLeft;

    private void Start()
    {
        movingLeft = startMovingLeft;
        StartCoroutine(MoveLeftRight());
    }

    private IEnumerator MoveLeftRight()
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                float direction = movingLeft ? -1f : 1f;
                transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            movingLeft = !movingLeft;
        }
    }
}
