using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;

    // Main scene에서 선택된 게임 모드를 저장할 변수
    private Constants.GameType _gameType;

    // Panel을 띄우기 위한 Canvas 정보
    private Canvas _canvas;

    // 게임 로직
    private GameLogic _gameLogic;


    /// <summary>
    /// Main에서 Game scene으로 전환시 호출될 메서드
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Game에서 Main scene으로 전환시 호출될 메서드
    /// </summary>
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// ConfirmPanel을 띄우는 메서드
    /// </summary>
    /// <param name="message"></param>
    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClickd onConfirmButtonClicked)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClicked);
        }
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            var blockController = FindFirstObjectByType<BlockController>();
            blockController.InitBlocks();


            // 게임 로직 생성
            if (_gameLogic != null)
            {
                // TODO: 기존 게임 로직을 소멸
            }

            _gameLogic = new GameLogic(blockController, _gameType);
        }
    }
}
