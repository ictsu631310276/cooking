using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtomInMenu : MonoBehaviour
{
    [SerializeField] private GameObject playUI;
    public void PlayButtom()
    {
        playUI.SetActive(true);
    }
    public void BackButtom()
    {
        playUI.SetActive(false);
    }
    public void OnePlayerButtom()
    {
        UIManagerScript.numOfPlayer = 1;
        CutScene();
    }
    public void TwoPlayerButtom()
    {
        UIManagerScript.numOfPlayer = 2;
        CutScene();
    }
    private void CutScene()
    {
        SceneManager.LoadScene("CutScene");
    }
    public void SkipButtom()
    {
        SceneManager.LoadScene(UIManagerScript.dayInGame);
    }
    public void ExitButtom()
    {
        Application.Quit();
    }
    private void Start()
    {
        if (playUI != null)
        {
            playUI.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
