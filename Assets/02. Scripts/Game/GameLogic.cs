using UnityEngine;
using UnityEngine.Timeline;

public class GameLogic
{
    public BlockController blockController; // 블럭을 처리할 객체

    private Constants.PlayerType[,] _board; // 보드의 상태 정보

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B

    public enum GameResult { None, Win, Lose, Draw } 

    public BasePlayerState _currentPlayerState; // 현재 턴의 Player

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="blockController"></param>
    /// <param name="gameType"></param>
    public GameLogic(BlockController blockController, Constants.GameType gameType)
    {
        this.blockController = blockController;

        // 보드의 상태 정보 초기화
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // 게임 타입 초기화
        switch (gameType)
        {
            case Constants.GameType.SinglePlay:
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);

                // 게임 시작
                SetState(firstPlayerState);

                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }

    // 턴이 바뀔 때, 기존 진행하던 상태를 Exit하고 이번턴의 상태를 _currentPlayerState에 할당하고 이번턴의 상태에 Enter를 호출
    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(this); // 현재 상태 종료
        _currentPlayerState = state; // 새로운 상태로 변경
        _currentPlayerState.OnEnter(this); // 새로운 상태 시작
    }

    // _board 배열에 새로운 Marker 값을 할당
    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col)
    {
        if (_board[row, col] != Constants.PlayerType.None) return false;

        if (playerType == Constants.PlayerType.PlayerA)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarekrType.O, row, col);
            return true;
        }
        else if (playerType == Constants.PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarekrType.X, row, col);
            return true;
        }
        return false;
    }

    // 게임 오버 처리
    public void EndGame(GameResult gameResult)
    {
        SetState(null); // 상태 종료
        firstPlayerState = null; // PlayerState 해제
        secondPlayerState = null; // PlayerState 해제

        Debug.Log("### Game Over ###");
    }


    // 게임의 결과 확인
    public GameResult CheckGameResult()
    {
        if (CheckGameWin(Constants.PlayerType.PlayerA, _board))
        {
            return GameResult.Win;
        }
        else if (CheckGameWin(Constants.PlayerType.PlayerB, _board))
        {
            return GameResult.Lose;
        }

        // 무승부인지 확인
        if (CheckGameDraw(_board))
        {
            return GameResult.Draw;
        }
     
        return GameResult.None;

    }

    // 무승부 확인 함수
    public bool CheckGameDraw(Constants.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constants.PlayerType.None)
                    return false; // 빈 칸이 있으면 무승부 아님
            }
        }
        return true; // 빈 칸이 없으면 무승부
    }


    // 게임 승리 확인
    private bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        // Col 체크 후 일자면 True
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType && 
                board[row, 1] == playerType && 
                board[row, 2] == playerType)
            {
                return true;
            }
        }

        // Row 체크 후 일자면 True
        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType && 
                board[1, col] == playerType && 
                board[2, col] == playerType)
            {
                return true;
            }
        }

        // 대각선 일자면 True
        if (board[0, 0] == playerType && 
            board[1, 1] == playerType && 
            board[2, 2] == playerType)
        {
            return true;
        }

        if (board[0, 2] == playerType && 
            board[1, 1] == playerType && 
            board[2, 0] == playerType)
        {
            return true;
        }
        return false;
    }
}
