using System.Collections;
using UnityEngine;

public class CounterAnimation : MonoBehaviour
{
    [SerializeField] private float animationSpeed = 1f;
    [SerializeField] private Vector3 targetScale;

    private bool isAnimating = false;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private IEnumerator AnimateCorutine()
    {
        isAnimating = true;
        while(transform.localScale != originalScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originalScale, 0.5f * animationSpeed);
            yield return null;
        }
        isAnimating = false;
    }

    public void Animate()
    {
        if (isAnimating) return;
        transform.localScale = targetScale;
        StartCoroutine(AnimateCorutine());
    }
}
