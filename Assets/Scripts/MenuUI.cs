using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TextMeshProUGUI bestPlayerNameAndScore;

    private void Start()
    {
        nameInputField.onEndEdit.AddListener(DataManager.Instance.SetPlayerName);

        bestPlayerNameAndScore.text = $"BEST PLAYER: {DataManager.Instance.BestPlayer}, {DataManager.Instance.BestScore}";
    }
}
