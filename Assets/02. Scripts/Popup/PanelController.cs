using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class PanelController : MonoBehaviour
{
    // 팝업창 RectTransform
    [SerializeField] private RectTransform panelRectTransform;

    private CanvasGroup _backgroundCanvasGroup;

    // Panel이 hide 될 때 해야 할 동작
    public delegate void PanelControlloerHideDelegate();

    private void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }


    public void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(endValue:1, duration:0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(endValue:1, duration:0.3f).SetEase(Ease.OutBack);
    }

    public void Hide(PanelControlloerHideDelegate hideDelegate = null)
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        _backgroundCanvasGroup.DOFade(endValue: 0, duration: 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(endValue: 0, duration: 0.3f).SetEase(Ease.InBack).
            OnComplete(() =>
            {
                hideDelegate?.Invoke();
                Destroy(gameObject);
            });
    }

}
