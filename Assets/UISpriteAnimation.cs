using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] spriteArray;
    public float speed = 0.02f;

    private int indexSprite;
    private Coroutine coroutineAnimation;

    private void OnEnable()
    {
        PlayUIAnimation();
    }

    public void PlayUIAnimation()
    {
        if (coroutineAnimation != null)
        {
            StopCoroutine(coroutineAnimation);
        }
        coroutineAnimation = StartCoroutine(PlayUIAnimationCoroutine());
    }

    IEnumerator PlayUIAnimationCoroutine()
    {
        while (true)
        {
            image.sprite = spriteArray[indexSprite];
            indexSprite = (indexSprite + 1) % spriteArray.Length;
            yield return new WaitForSecondsRealtime(speed);
        }
    }
}
