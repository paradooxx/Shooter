using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [SerializeField] private Image m_Image;
    [SerializeField] private Sprite[] m_SpriteArray;
    public float m_Speed = 0.02f;

    private int m_IndexSprite;
    private Coroutine m_CoroutineAnim;

    private void OnEnable()
    {
        Func_PlayUIAnim();
    }

    public void Func_PlayUIAnim()
    {
        if (m_CoroutineAnim != null)
        {
            StopCoroutine(m_CoroutineAnim);
        }
        m_CoroutineAnim = StartCoroutine(Func_PlayAnimUI());
    }

    IEnumerator Func_PlayAnimUI()
    {
        while (true)
        {
            m_Image.sprite = m_SpriteArray[m_IndexSprite];
            m_IndexSprite = (m_IndexSprite + 1) % m_SpriteArray.Length;
            yield return new WaitForSecondsRealtime(m_Speed);
        }
    }
}
