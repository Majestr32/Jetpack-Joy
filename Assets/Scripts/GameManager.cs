using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;
    public static GameManager Instance { get; private set; }

    private int _coinsCount;
    public int coinsCount { get => _coinsCount; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void pickCoin()
    {
        _coinsCount++;
        _textMesh.text = "Coins: " + _coinsCount.ToString();
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
