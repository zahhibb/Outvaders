﻿using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    ScoreScript m_scoreScript;
    private float m_hiddenScore = 0;
    private int m_PlayerScore;
    public int PlayerScore
    {
        get { return m_PlayerScore; }
        set { m_PlayerScore = value; }
    }

    private bool m_IsRunning = true;
    public bool IsRunning
    {
        get { return m_IsRunning; }
        set
        {
            m_IsRunning = value;
            LoseMenuReff.SetActive(!value);
            IsPaused = true;
        }
    }

    private bool m_IsPaused = false;
    public bool IsPaused
    {
        get { return m_IsPaused; }
        set
        {
            m_IsPaused = value;
            Cursor.visible = value;
            NebulaReff.SetActive(!IsPaused);
            if (m_IsPaused)
            {
                Time.timeScale = 0;
                FindObjectOfType<Camera>().fieldOfView = OriginalFOV / 2;
            }
            else
            {
                Time.timeScale = 1;
                FindObjectOfType<Camera>().fieldOfView = OriginalFOV;
            }
        }

    }

    GameObject PauseMenuReff;
    [SerializeField] GameObject NebulaReff;
    GameObject LoseMenuReff;
    float OriginalFOV;

    void Start()
    {
        PauseMenuReff = FindObjectOfType<PauseMenu>().gameObject;
        PauseMenuReff.SetActive(false);
        LoseMenuReff = FindObjectOfType<LoseMenu>().gameObject;
        LoseMenuReff.SetActive(false);
        m_scoreScript = GameObject.FindGameObjectWithTag("Finish").GetComponent<ScoreScript>();
        //NebulaReff = FindObjectOfType<NebulaScript>().gameObject;
        OriginalFOV = FindObjectOfType<Camera>().fieldOfView;
        Time.timeScale = 1;
    }
	
    void FixedUpdate()
    {
        GainScore();
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsRunning)
        {
            PauseFunc();
        }
	}
    
    public void PauseFunc()
    {
        IsPaused = !IsPaused;
        PauseMenuReff.SetActive(IsPaused);
        
    }

    public void QuitFunc()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void RestartFunc()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HedmansScene");
    }

    void GainScore()
    {
        if (!IsPaused)
        {
            m_hiddenScore += Time.fixedDeltaTime;
            m_scoreScript.Score = Mathf.RoundToInt(m_hiddenScore);
        }
    }
}
