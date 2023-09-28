using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private double coinsCount;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private double currentMeleePrice;
    [SerializeField] private double currentRangePrice;
    [SerializeField] private TextMeshProUGUI costMeleeText;
    [SerializeField] private TextMeshProUGUI costRangeText;

    private MobsSpawner _mobsSpawner;

    private void Start()
    {
        InitNewPrices();
        UpdateText();
        _mobsSpawner = MobsSpawner.instance;
    }

    private void InitNewPrices()
    {
        currentMeleePrice = coinsCount / 9 + currentMeleePrice * 0.5f;
        currentRangePrice = coinsCount / 9 + currentRangePrice * 0.6f;
    }

    public void BuyMeleeMob()
    {
        if (currentMeleePrice <= coinsCount)
        {
            RemoveCoins(currentMeleePrice);
            _mobsSpawner.SpawnMeleeMob();
            UpgradeMeleePrice();
        }
    }

    public void BuyRangeMob()
    {
        if (currentRangePrice <= coinsCount)
        {
            RemoveCoins(currentRangePrice);
            _mobsSpawner.SpawnRangeMob();
            UpgradeRangePrice();
        }
    }

    private void AddCoins(double count)
    {
        coinsCount += count;
        UpdateText();
    }

    private void RemoveCoins(double count)
    {
        coinsCount -= count;
        UpdateText();
    }

    private void UpdateText()
    {
        coinsText.text = FomatCoinsCount.FormatCount(coinsCount);
        costMeleeText.text = FomatCoinsCount.FormatCount(currentMeleePrice);
        costRangeText.text = FomatCoinsCount.FormatCount(currentRangePrice);
    }

    private void UpgradeMeleePrice()
    {
        currentMeleePrice = Mathf.Round((float)(currentMeleePrice * 1.15f));
        UpdateText();
    }
    
    private void UpgradeRangePrice()
    {
        currentRangePrice = Mathf.Round((float)(currentRangePrice * 1.15f));
        UpdateText();
    }
}