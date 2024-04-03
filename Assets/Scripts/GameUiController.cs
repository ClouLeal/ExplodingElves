using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _endGameBtn;
    [SerializeField] private GameObject _instracutionsGo;

    void Awake()
    {
        UpdateUI(false);
    }

    public void SetUp(UnityAction startGame, UnityAction endGame)
    {
        _startGameBtn.onClick.AddListener(startGame);
        _startGameBtn.onClick.AddListener(() => UpdateUI(true));

        _endGameBtn.onClick.AddListener(endGame);
        _endGameBtn.onClick.AddListener(() => UpdateUI(false));
    }

    void UpdateUI(bool gameHasStarted){
        _startGameBtn.gameObject.SetActive (!gameHasStarted);
        _endGameBtn.gameObject.SetActive (gameHasStarted);
        _instracutionsGo.SetActive(!gameHasStarted);
    }
}
