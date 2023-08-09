using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrapMenu : MonoBehaviour
{
    [SerializeField] private PlayerScript _player;
    [SerializeField] private int _repairShipCost;
    [SerializeField] private int _craftAmmoCost;
    [SerializeField] private TextMeshProUGUI _repairCost;
    [SerializeField] private TextMeshProUGUI _craftCost;

    private void Start()
    {
        _player = GetComponent<PlayerScript>();
        _craftCost.text = "Cost: " + _craftAmmoCost + " scraps";
        _repairCost.text = "Cost: " + _repairShipCost + " scraps";
    }
    public void BuyRepairShip()
    {
        if (_player.scrapCount >= _repairShipCost && _player._playerHP < _player._maxHp)
        {
            _player.DeductScraps(_repairShipCost);
            _player.Heal(1);
            _repairCost.text = "Cost: " + _repairShipCost + " scraps";
            SoundManager.Instance.audioSource.PlayOneShot(SoundManager.Instance.repairSfx);
            SoundManager.Instance.audioSource.volume = 0.2f;
        }
    }

    public void BuyCraftAmmo()
    {
        if (_player.scrapCount >= _craftAmmoCost)
        {
            _player.DeductScraps(_craftAmmoCost);
            _player.AddAmmo(5);
            _craftCost.text = "Cost: " + _craftAmmoCost + " scraps";
            SoundManager.Instance.audioSource.PlayOneShot(SoundManager.Instance.craftSfx);
        }
    }
}
