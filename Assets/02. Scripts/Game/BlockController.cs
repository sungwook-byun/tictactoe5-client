using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    public delegate void OnBlockClicked(int row, int col);
    public OnBlockClicked onBlockClickedDelegate;

    // ��� Block�� �ʱ�ȭ
    public void InitBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].InitMarker(i, onBlockClicked:blockIndex =>
            {
                // Ư�� ���� Ŭ�� �� ���¿� ���� ó��
                var row = blockIndex / Constants.BlockColumnCount;
                var col = blockIndex % Constants.BlockColumnCount;

                onBlockClickedDelegate?.Invoke(row, col);
            });
        }
    }

    // Ư�� Block�� ��Ŀ ǥ��

    public void PlaceMarker(Block.MarekrType marekrType, int row, int col)
    {
        // row, col -> Index ��ȯ
        var blockIndex = row * Constants.BlockColumnCount + col;

        blocks[blockIndex].SetMarker(marekrType);
    }

    // Ư�� Block�� ������ ����

    public void SetBlockColor()
    {

    }
}
