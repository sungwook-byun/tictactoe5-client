
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
        // FirstPlayer���� Ȯ���ؼ� ���� UI�� ���� �� ǥ��

        // BlockController���� �ؾ� �� ���� ����
        gameLogic.blockController.onBlockClickedDelegate = (row, col) =>
        {
            // ���� ��ġ �� ������ ��ٷȴٰ� ��ġ�Ǹ� ó�� �� ��
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
