using UnityEngine;

public class GameUIController : MonoBehaviour
{

    public void OnClickBackButton()
    {
        GameManager.Instance.OpenConfirmPanel(message:"������ �����Ͻðڽ��ϱ�?", 
            onConfirmButtonClicked:() =>
            {
                GameManager.Instance.ChangeToMainScene();
            });
    }
}
