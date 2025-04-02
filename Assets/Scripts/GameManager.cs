using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject finish;
    public GameObject start;
    public Image[] haerts;
    public Image[] haerts2;
    public Sprite ret, blak;
    public int livy = 3;
    void Start()
    {
        finish.SetActive(false);
        start.SetActive(true);
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

    public void RestartLvll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
    void NextLvl()
    {
        int currentlvl = SceneManager.GetActiveScene().buildIndex;
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
