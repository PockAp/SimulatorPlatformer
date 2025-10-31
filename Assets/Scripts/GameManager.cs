using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject finish;
    public GameObject start;
    public Image[] haerts;
    public TextMeshProUGUI snowmantext;
    public Sprite ret, blak;
    public int currentlvl;
    public int snowman;
    public int maxhaertslivy = 6;
    public int livy = 3;
    void Start()
    {
        currentlvl = SceneManager.GetActiveScene().buildIndex;
        finish.SetActive(false);
        start.SetActive(true);
        if(currentlvl == 0)
        {
            snowman = 0;
        }
        else
        {
            snowman = PlayerPrefs.GetInt("snowman", 0);
        }
        SnowmanTextUpdate();
        UpdateLifePanel();
    }

    public void AnimationFinish()
    {
        finish.SetActive(true);
        Invoke("NextLvl", 5);
    }

    void Finish2()
    {
        finish.SetActive(false);
    }

    public void SnowmanTextUpdate()
    {
        snowmantext.text = " :    " + snowman.ToString();
    }

    public void RestartLvll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
    void NextLvl()
    {
        PlayerPrefs.SetInt("snowman", snowman);
        PlayerPrefs.Save();
        SceneManager.LoadScene(currentlvl + 1);
    }

    void Update()
    {
        if(livy <= 0)
        {
            RestartLvll();  
        }
    }

    public void UpdateLifePanel()
    {
        for (int i = 0; i < haerts.Length; i++)
        {
            if (livy > i)
            {
                haerts[i].sprite = ret;
            }
            else
            {
                haerts[i].sprite = blak;
            }
        }
    }
}
