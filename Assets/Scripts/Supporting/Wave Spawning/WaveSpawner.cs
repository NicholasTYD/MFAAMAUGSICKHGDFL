using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour, ISavable
{
    public static WaveSpawner Instance;

    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] float waveTextAppearDuration;
    [SerializeField] List<Wave> waves;
    [SerializeField] UpgradeMenu upgradeMenu;
    [SerializeField] VictoryScreen victoryScreen;
    [SerializeField] PlayerHealth playerHealth;

    float waveTextTimer;
    float INTERVAL_BEFORE_POST_WAVE_MENU_APPEARS = 3;

    public bool WaveCompleted { get; set; }
    public bool UpgradesChosen { get; set; }
    public int CurrentWave { get; set; }

    public int CurrentEnemyCount { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        WaveCompleted = true;
        UpgradesChosen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (WaveCompleted && UpgradesChosen && !GameManager.Instance.isGamePaused)
            {
                initiateWave();
            }
            else if (!WaveCompleted)
            {
                setWaveText("You can't start a wave while its still in progress!");
            } 
            else if (!UpgradesChosen)
            {
                setWaveText("Choose an upgrade first!");
            }
        }

        if (waveTextTimer >= 0)
        {
            waveTextTimer -= Time.deltaTime;
        } 
        else if (waveText.gameObject.activeInHierarchy)
        {
            waveText.gameObject.SetActive(false);
        }
    }
    public bool gotEnemiesRemaining()
    {
        return CurrentEnemyCount > 0;
    }

    void initiateWave()
    {
        if (CurrentWave >= waves.Count)
        {
            Debug.Log("the end");
            return;
        }

        string text = waves[CurrentWave].GetWaveName();
        setWaveText(text);
        waves[CurrentWave].StartWave();
        WaveCompleted = false;
        UpgradesChosen = false;
        playWaveBGM(waves[CurrentWave]);
    }

    void playWaveBGM(Wave wave)
    {
        AudioClip waveBGM = wave.GetWaveBGM();
        if (wave.GetWaveBGM() != null)
        {
            MusicPlayer.Instance.PlayClip(waveBGM);
        }
    }

    public void ConcludeWave()
    {
        WaveCompleted = true;
        string text = waves[CurrentWave].GetWaveName() + " Complete!";
        setWaveText(text);
        StartCoroutine(PresentUpgrades());
    }

    public void ConcludeBossWave()
    {
        WaveCompleted = true;
        string text = "Boss Wave Complete!";
        setWaveText(text);
        MusicPlayer.Instance.RevertBackToNormalBGM();

        StartCoroutine(PresentSpecialUpgrades());
    }

    public void ConcludeGame()
    {
        WaveCompleted = true;
        UpgradesChosen = true;
        string text = "Final Wave Complete!";
        setWaveText(text);
        MusicPlayer.Instance.RevertBackToNormalBGM();

        StartCoroutine(PresentVictoryScreen());
    }

    IEnumerator PresentUpgrades()
    {
        yield return new WaitForSeconds(INTERVAL_BEFORE_POST_WAVE_MENU_APPEARS);
        upgradeMenu.PresentUpgrades();

        StartCoroutine(IncrementWaveAndSave());
    }

    IEnumerator PresentSpecialUpgrades()
    {
        yield return new WaitForSeconds(INTERVAL_BEFORE_POST_WAVE_MENU_APPEARS);

        // Fire knight
        if (CurrentWave == 4)
        {
            upgradeMenu.PresentEnhancedUpgrades(0);
        } 
        // Priestess
        else
        {
            upgradeMenu.PresentEnhancedUpgrades(1);
        }

        StartCoroutine(IncrementWaveAndSave());
    }

    IEnumerator PresentVictoryScreen()
    {
        yield return new WaitForSeconds(INTERVAL_BEFORE_POST_WAVE_MENU_APPEARS);

        victoryScreen.EnableVictoryScreen();
    }

    IEnumerator IncrementWaveAndSave()
    {
        yield return new WaitForSeconds(0.01f);
        CurrentWave++;
        CombatMechanics.Instance.Heal(General.Instance.Player, 999);
        GameManager.Instance.SaveGameData();
    }

    void setWaveText(string text)
    {
        waveText.text = text;
        waveText.gameObject.SetActive(true);
        waveTextTimer = waveTextAppearDuration;
    }

    public void SaveData(SaveData saveData)
    {
        saveData.CurrentWave = WaveSpawner.Instance.CurrentWave;
    }

    public void LoadData(SaveData saveData)
    {
        CurrentWave = saveData.CurrentWave;
        WaveCompleted = true;
        UpgradesChosen = true;
    }
}
