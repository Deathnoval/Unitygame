using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.onClick.AddListener(StartNewGame);
    }
    private void StartNewGame()
    {
        ScenesManager.Instance.LoadMainMenu();
    }
}
