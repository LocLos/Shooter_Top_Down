using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
using UnityEngine.SceneManagement;


public class LoadingPanel : MonoBehaviour
{
    [SerializeField] Text LoadingTxt;
    [SerializeField] Image curtain;

    GameState _gameState;
    LoadingLvl _loadingLvl;

    [Inject]
    private void Construct(GameState GameState, LoadingLvl LoadingLvl)
    {
        _gameState = GameState;
        _loadingLvl = LoadingLvl;
    }
    void Start()
    {
        _gameState.onShowCurtain += ShowCurtain;
    }

    void ShowCurtain(bool isShow)
    {
        if (isShow)
        {
            curtain.DOFade(1, 2);
            StartCoroutine(TextPrint("Loading.."));
        }
        else
        {
            curtain.DOFade(0, 2);
            LoadingTxt.text = "";
        }
    }

    IEnumerator TextPrint(string message)
    {
        foreach (char ch in message)
        {
            yield return new WaitForSeconds(0.3f);
            LoadingTxt.text += ch.ToString();
        }

        if (_loadingLvl.level < 6)
        {
            yield return new WaitForSeconds(0.1f);
            _gameState.ChangeState(State.NextLevel);

        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene("Menu");
        }
    }
}
