using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    public delegate void OnBlockClicked(int row, int col);
    public OnBlockClicked onBlockClickedDelegate;

    // 모든 Block을 초기화
    public void InitBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].InitMarker(i, onBlockClicked:blockIndex =>
            {
                // 특정 블럭이 클릭 된 상태에 대한 처리
                var row = blockIndex / Constants.BlockColumnCount;
                var col = blockIndex % Constants.BlockColumnCount;

                onBlockClickedDelegate?.Invoke(row, col);
            });
        }
    }

    // 특정 Block에 마커 표시

    public void PlaceMarker(Block.MarekrType marekrType, int row, int col)
    {
        // row, col -> Index 변환
        var blockIndex = row * Constants.BlockColumnCount + col;

        blocks[blockIndex].SetMarker(marekrType);
    }

    // 특정 Block의 배경색을 설정

    public void SetBlockColor()
    {

    }
}
