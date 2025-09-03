
public class PlayerState : BasePlayerState
{
    private bool _isFirstPlayer;
    private Constants.PlayerType _playerType;

    public PlayerState(bool isFirstPlayer)
    {
        _isFirstPlayer = isFirstPlayer;
        _playerType = isFirstPlayer ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }

    public override void OnEnter(GameLogic gameLogic)
    {
        // FirstPlayer인지 확인해서 게임 UI에 현재 턴 표시

        // BlockController에게 해야 할 일을 전달
        gameLogic.blockController.onBlockClickedDelegate = (row, col) =>
        {
            // 블럭이 터치 될 때까지 기다렸다가 터치되면 처리 할 일
            HandleMove(gameLogic, row, col);
        };
    }

    public override void OnExit(GameLogic gameLogic)
    {
        gameLogic.blockController.onBlockClickedDelegate = null;
    }

    public override void HandleMove(GameLogic gameLogic, int row, int col)
    {
        ProcessMove(gameLogic, _playerType, row, col);
    }

    protected override void HandleNextTurn(GameLogic gameLogic)
    {
        if (_isFirstPlayer)
        {
            // gameLogic._currentPlayerState = gameLogic.secondPlayerState;
        }
        else
        {
            // gameLogic._currentPlayerState = gameLogic.firstPlayerState;
        }
        // gameLogic._currentPlayerState.OnEnter(gameLogic);
    }
}
