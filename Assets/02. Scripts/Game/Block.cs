using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]

public class Block : MonoBehaviour
{
    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSpriteRenderer;

    public delegate void OnBlockClicked(int index);
    private OnBlockClicked _onBlockClicked;

    // 마커 타입
    public enum MarekrType { None, O, X }

    // 블럭 인덱스
    private int _blockIndex;

    // 블럭의 색상 변경을 위한 SpriteRenderer
    private SpriteRenderer _spriteRenderer;
    private Color _defalutBlockColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defalutBlockColor = _spriteRenderer.color;
    }

    // 초기화
    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarekrType.None);
        SetBlockColor(_defalutBlockColor);
        _onBlockClicked = onBlockClicked;
    }

    // 마커 설정
    public void SetMarker(MarekrType marekrType)
    {
        switch (marekrType)
        {
            case MarekrType.None:
                markerSpriteRenderer.sprite = null;
                break;
            case MarekrType.O:
                markerSpriteRenderer.sprite = oSprite;
                break;
            case MarekrType.X:
                markerSpriteRenderer.sprite = xSprite;
                break;
        }
    }

    // 블럭 색상 변경
    public void SetBlockColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    //
    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Debug.Log("Selected Block : " + _blockIndex);

        _onBlockClicked?.Invoke(_blockIndex);
    }
}
