using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private GameManager gameManager;
    
    private Button difficultyButton;
    private GameObject titleText;

    public float difficultyValue = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        difficultyButton = GetComponent<Button>();
        difficultyButton.onClick.AddListener(SetDifficulty);

        gameManager = FindAnyObjectByType<GameManager>();
        titleText = GameObject.Find("TitleText").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        titleText.SetActive(false);
        gameManager.StartGame(difficultyValue);
    }
}
