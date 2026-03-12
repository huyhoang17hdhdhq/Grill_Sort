using UnityEngine;
using DG.Tweening;
using System;

public class FoodTween : MonoBehaviour
{
    public void FlyTo(Vector3 target, Action onComplete)
    {
        transform.DOMove(target, 0.4f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }
    public void MoveToSlot(Transform target)
    {
        transform.DOMove(target.position, 0.5f)
            .SetEase(Ease.OutBack);

        transform.DOScale(Vector3.one, 0.5f);
    }
}