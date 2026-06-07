using DG.Tweening;
using UnityEngine;

public static class TweenUtils
{
    public static void PopScale(Transform t, float scale = 1.15f, float duration = 0.15f)
    {
        t.localScale = Vector3.one;
        t.DOPunchScale(Vector3.one * (scale - 1f), duration, 1, 0.5f);
    }
}
