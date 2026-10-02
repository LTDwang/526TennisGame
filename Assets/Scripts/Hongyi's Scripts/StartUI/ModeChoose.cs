using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameType
{
    PVE,
    PVP
}
public class ModeChoose : MonoBehaviour
{
    public GameType gameType = GameType.PVE;
    static ModeChoose instance;
    [SerializeField]
    private GameObject UIpanel;
    public static ModeChoose Instance {  get { return instance; } }
    public void ChooseAI()
    {
        gameType = GameType.PVE;
        UIpanel.SetActive(false);
        SceneManager.LoadScene("OurScene");
    }
    public void ChooseFirend()
    {
        gameType = GameType.PVP;
        UIpanel.SetActive(false);
        SceneManager.LoadScene("OurScene");
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    public void BackToStart()
    {
        UIpanel.SetActive(true);
    }
}
