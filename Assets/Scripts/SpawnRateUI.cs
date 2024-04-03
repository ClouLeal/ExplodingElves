using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpawnRateUI : MonoBehaviour 
{   
    [SerializeField] private Canvas _spawnRateCanvas;
    [SerializeField] private TMP_InputField _spawnRateInputField;
    [SerializeField] private Button _saveButton;


    public Action<float> UpdateSpawnRate;

    private float _spawnRateFloat;

    void Awake()
    {
        _saveButton.onClick.AddListener(SaveRate);
        _saveButton.onClick.AddListener(ClosePanel);

        _spawnRateInputField.onEndEdit.AddListener(UpDateSpawnRateString);
    }

    public void OpenPanel()
    {
        _spawnRateCanvas.enabled = true;
    }

    private void ClosePanel()
    {
       _spawnRateCanvas.enabled = false;
    }

    private void UpDateSpawnRateString(string newRateString)
    {
        
        _spawnRateFloat =  float.Parse(newRateString);
    }

    void SaveRate()
    {

        UpdateSpawnRate?.Invoke(_spawnRateFloat);
    }

    internal void SetUp(float spawnRate , string name )
    {
        _spawnRateInputField.textComponent.SetText(spawnRate.ToString());
        _spawnRateInputField.textComponent.SetAllDirty();
    }
}
