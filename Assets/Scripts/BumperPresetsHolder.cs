using System.Collections.Generic;
using UnityEngine;

public class BumperPresetsHolder : MonoBehaviour
{
    [SerializeField] private List<GameObject> presets;

    public void ActivateRandomPreset()
    {
        foreach (var preset in presets)
        {
            preset.SetActive(false);
        }
        
        int randomPresetIdx = Random.Range(0, presets.Count);
        presets[randomPresetIdx].SetActive(true);
    }

    public void ActivatePresetForLoop(int loop)
    {
        foreach (var preset in presets)
        {
            preset.SetActive(false);
        }
        
        presets[loop].SetActive(true);
    }
}