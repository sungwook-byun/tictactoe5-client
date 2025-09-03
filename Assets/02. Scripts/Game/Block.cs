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

    // ��Ŀ Ÿ��
    public enum MarekrType { None, O, X }

    // �� �ε���
    private int _blockIndex;

    // ���� ���� ������ ���� SpriteRenderer
    private SpriteRenderer _spriteRenderer;
    private Color _defalutBlockColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defalutBlockColor = _spriteRenderer.color;
    }

    // �ʱ�ȭ
    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarekrType.None);
        SetBlockColor(_defalutBlockColor);
        _onBlockClicked = onBlockClicked;
    }

    // ��Ŀ ����
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

    // �� ���� ����
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
