using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;


    // Confirm ��ư Ŭ���� ȣ��� ��������Ʈ
    public delegate void OnConfirmButtonClickd();
    private OnConfirmButtonClickd _onConfirmButtonClickd;

    public void Show(string message, OnConfirmButtonClickd onConfirmButtonClickd)
    {
        messageText.text = message;
        _onConfirmButtonClickd = onConfirmButtonClickd;
        base.Show(); // �� �� ������ �ֱ� ������ base.�� �ٿ��� �θ��� Show()�� ȣ���Ѵ�.
    }


    /// <summary>
    /// Ȯ�� ��ư Ŭ���� ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickConfirmButton()
    {
        Hide(() =>
        {
            _onConfirmButtonClickd?.Invoke(); // ��������Ʈ�� null�� �ƴ� ���� ȣ��
        });
    }


    /// <summary>
    /// X ��ư Ŭ���� ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickCloseButton()
    {
        Hide();
    }
}
