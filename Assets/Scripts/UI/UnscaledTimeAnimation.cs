using UnityEngine;

public class UnscaledTimeAnimation : MonoBehaviour
{
    public Animation animationComponent;
    public string animationClipName;

    private void Update()
    {
        if (animationComponent.isPlaying)
        {
            animationComponent[animationClipName].time += Time.unscaledDeltaTime;
            animationComponent.Sample();
        }
    }

    public void PlayAnimation()
    {
        animationComponent.Play(animationClipName);
        animationComponent[animationClipName].time = 0;
    }
}
