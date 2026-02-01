using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private void Start()
    {
        // Save the original screen width and height
        originalWidth = Screen.currentResolution.width;
        originalHeight = Screen.currentResolution.height;

        // Load fullscreen setting or default to fullscreen if not set
        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        Screen.fullScreen = isFullScreen;

        // Update the toggle state based on the saved setting or default to true
        if (fullScreenToggle != null)
        {
            fullScreenToggle.isOn = isFullScreen;
            fullScreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(fullScreenToggle.isOn); });
        }

        // Initialize the quality settings dropdown
        if (qualityDropdown != null)
        {
            // Populate the dropdown with available quality levels
            qualityDropdown.ClearOptions();
            List<string> options = new List<string>(QualitySettings.names);
            qualityDropdown.AddOptions(options);

            // Load the saved quality setting or default to the highest setting
            int savedQualityLevel = PlayerPrefs.GetInt("QualityLevel", QualitySettings.names.Length - 1);
            qualityDropdown.value = savedQualityLevel;
            QualitySettings.SetQualityLevel(savedQualityLevel);

            // Add listener for dropdown value changes
            qualityDropdown.onValueChanged.AddListener(delegate { SetQualityLevel(qualityDropdown.value); });
        }
    }

    private void inputcheck()
    {
        
    }

    #region [Scene Control]

    public void playbutton()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    #endregion

    #region [Graphics Control]

    public Toggle fullScreenToggle;
    public TMP_Dropdown qualityDropdown;

    private int originalWidth;
    private int originalHeight;

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        // Save fullscreen setting
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);

        if (!isFullScreen)
        {
            // Set the resolution to a windowed mode size relative to the original resolution
            Screen.SetResolution(originalWidth / 2, originalHeight / 2, false);
        }
        else
        {
            // Restore the resolution to the original when switching back to full screen
            Screen.SetResolution(originalWidth, originalHeight, true);
        }

        // Update the toggle state (to ensure synchronization)
        if (fullScreenToggle != null && fullScreenToggle.isOn != isFullScreen)
        {
            fullScreenToggle.isOn = isFullScreen;
        }
    }

    public void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

    #endregion

}