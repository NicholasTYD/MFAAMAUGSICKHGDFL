using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] List<Button> upgradeButtons;
    [SerializeField] List<TextMeshProUGUI> upgradeTexts;

    [SerializeField] float healthIncrease;
    [SerializeField] float attackIncrease;
    [SerializeField] float attackSpeedIncrease;
    [SerializeField] float parryDamageBonusDurationIncrease;
    [SerializeField] float parryDamageBonusMultiplierIncrease;
    [SerializeField] float movementSpeedIncrease;

    PlayerMain playerMain;
    int numberOfUpgradeChoices = 3;
    int totalAvailableUpgrades = 6;

    private void Start()
    {
        playerMain = General.Instance.Player.GetComponent<PlayerMain>();
    }

    public void PresentUpgrades()
    {
        gameObject.SetActive(true);
        GameManager.Instance.Pause();

        bool[] alreadyChosen = new bool[totalAvailableUpgrades];

        for (int upgradeSlot = 0; upgradeSlot < numberOfUpgradeChoices; upgradeSlot++)
        {
            int chosenUpgrade = Random.Range(0, totalAvailableUpgrades);
            while (alreadyChosen[chosenUpgrade])
            {
                chosenUpgrade = Random.Range(0, totalAvailableUpgrades);
            }
            alreadyChosen[chosenUpgrade] = true;

            Button button = upgradeButtons[upgradeSlot];
            TextMeshProUGUI text = upgradeTexts[upgradeSlot];

            switch (chosenUpgrade)
            {
                case 0:
                    upgradeHealth(upgradeSlot, button, text);
                    break;
                case 1:
                    upgradeAttack(upgradeSlot, button, text);
                    break;
                case 2:
                    upgradeAttackSpeed(upgradeSlot, button, text);
                    break;
                case 3:
                    upgradeParryDamageBonusDuration(upgradeSlot, button, text);
                    break;
                case 4:
                    upgradeParryDamageBonusMultiplier(upgradeSlot, button, text);
                    break;
                case 5:
                    upgradeMovementSpeed(upgradeSlot, button, text);
                    break;
                default:
                    Debug.Log("You broke something oops");
                    break;
            }
            button.onClick.AddListener(closeAndResetMenuOnClick);
        }
    }

    public void PresentEnhancedUpgrades(int level)
    {
        gameObject.SetActive(true);
        GameManager.Instance.Pause();

        for (int upgradeSlot = 0; upgradeSlot < numberOfUpgradeChoices; upgradeSlot++)
        {
            Button button = upgradeButtons[upgradeSlot];
            TextMeshProUGUI text = upgradeTexts[upgradeSlot];

            // Fire knight
            if (level == 0)
            {
                switch (upgradeSlot)
                {
                    case 0:
                        upgradeHealthAndSpeed(upgradeSlot, button, text);
                        break;
                    case 1:
                        upgradeAttackAndAttackSpeed(upgradeSlot, button, text);
                        break;
                    case 2:
                        upgradeParryBonusDurationAndMultiplier(upgradeSlot, button, text);
                        break;
                    default:
                        Debug.Log("You broke something oops");
                        break;
                }
            }
            // Priestess
            else
            {
                switch (upgradeSlot)
                {
                    case 0:
                        unlockInfiniteComboTime(upgradeSlot, button, text);
                        break;
                    case 1:
                        unlockParryStrike(upgradeSlot, button, text);
                        break;
                    case 2:
                        upgradeAllStats(upgradeSlot, button, text);
                        break;
                    default:
                        Debug.Log("You broke something oops");
                        break;
                }
            }
            
            button.onClick.AddListener(closeAndResetMenuOnClick);
        }
    }

    // Normal Upgrades
    void upgradeHealth(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Increase max health by " + healthIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseMaxHealth(healthIncrease));
    }

    void upgradeAttack(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Increase base damage by " + attackIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseAttack(attackIncrease));
    }

    void upgradeAttackSpeed(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Increase attack speed by " + attackSpeedIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseAttackSpeed(attackSpeedIncrease));
    }

    void upgradeParryDamageBonusDuration(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Post parry damage bonus duration +" + parryDamageBonusDurationIncrease + "s.";
        button.onClick.AddListener(() => playerMain.IncreaseParryDamageBonusDuration(parryDamageBonusDurationIncrease));
    }

    void upgradeParryDamageBonusMultiplier(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Post parry damage bonus +" + parryDamageBonusMultiplierIncrease * 100 + "%.";
        button.onClick.AddListener(() => playerMain.IncreaseParryDamageBonusMultiplier(parryDamageBonusMultiplierIncrease));
    }

    void upgradeMovementSpeed(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Movement Speed +" + movementSpeedIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseMovementSpeed(movementSpeedIncrease));
    }

    // Enhanced Upgrades
    void upgradeHealthAndSpeed(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Increase max health by " + healthIncrease + ".\n" +
            "Movement Speed +" + movementSpeedIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseMaxHealth(healthIncrease));
        button.onClick.AddListener(() => playerMain.IncreaseMovementSpeed(movementSpeedIncrease));
    }

    void upgradeAttackAndAttackSpeed(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Increase base damage by " + attackIncrease + ".\n" +
            "Increase attack speed by " + attackSpeedIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseAttack(attackIncrease));
        button.onClick.AddListener(() => playerMain.IncreaseAttackSpeed(attackSpeedIncrease));
    }

    void upgradeParryBonusDurationAndMultiplier(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Post parry damage bonus duration +" + parryDamageBonusDurationIncrease + "s.\n" +
            "Post parry damage bonus +" + parryDamageBonusMultiplierIncrease * 100 + "%.";
        button.onClick.AddListener(() => playerMain.IncreaseParryDamageBonusDuration(parryDamageBonusDurationIncrease));
        button.onClick.AddListener(() => playerMain.IncreaseParryDamageBonusMultiplier(parryDamageBonusMultiplierIncrease));
    }

    // Super Enhanced Upgrades
    void unlockInfiniteComboTime(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Your attack combo will only reset if you take damage";
        button.onClick.AddListener(() => playerMain.UnlockInfiniteComboTime());
    }

    void unlockParryStrike(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Your next attack after a successful parry does massive damage.";
        button.onClick.AddListener(() => playerMain.UnlockParryStrike());
    }

    void upgradeAllStats(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Upgrade ALL your stats!";
        button.onClick.AddListener(() => playerMain.IncreaseMaxHealth(healthIncrease));
        button.onClick.AddListener(() => playerMain.IncreaseMovementSpeed(movementSpeedIncrease));
        button.onClick.AddListener(() => playerMain.IncreaseAttack(attackIncrease));
        button.onClick.AddListener(() => playerMain.IncreaseAttackSpeed(attackSpeedIncrease));
        button.onClick.AddListener(() => playerMain.IncreaseParryDamageBonusDuration(parryDamageBonusDurationIncrease));
        button.onClick.AddListener(() => playerMain.IncreaseParryDamageBonusMultiplier(parryDamageBonusMultiplierIncrease));
    }

    // Leave Shop
    void closeAndResetMenuOnClick()
    {
        WaveSpawner.Instance.UpgradesChosen = true;
        foreach (Button button in upgradeButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        this.gameObject.SetActive(false);

        GameManager.Instance.Unpause();
    }
}
