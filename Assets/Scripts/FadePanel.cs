using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class FadePanel : MonoBehaviour
{
    [SerializeField] private float _transitionDuration = 1f;
    [SerializeField] private Image _image;
    
    public void TurnOn(TweenCallback onComplete)
    {
        _image.DOFade(1f, _transitionDuration).OnComplete(onComplete);
    }

    public void TurnOff(TweenCallback onComplete)
    {
        _image.DOFade(0f, _transitionDuration).OnComplete(onComplete);
    }
}
