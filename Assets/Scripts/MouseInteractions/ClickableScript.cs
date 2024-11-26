using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool Interactable = true;
    private ALookChanger _lookChanger;

    public bool AllowLeftClick = true;
    public bool AllowRightClick = false;

    [SerializeField]
    public UnityEvent OnLeftClick;
    [SerializeField]
    public UnityEvent OnRightClick;

    private void Awake()
    {
        _lookChanger = GetComponent<ALookChanger>();
    }

    private void OnEnable()
    {
        StartCoroutine(LooksToDefaultHadrCoroutine());
    }
    private IEnumerator LooksToDefaultHadrCoroutine()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        _lookChanger?.LooksToDefault(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Interactable)
            return;

        if (eventData.button == PointerEventData.InputButton.Right && AllowRightClick)
            OnRightClick?.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Left && AllowLeftClick)
            OnLeftClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Interactable)
            return;

        _lookChanger?.LooksToHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Interactable)
            return;

        _lookChanger?.LooksToDefault();
    }
}
