using System;
using System.Collections.Generic;
using Events;
using Services.Interfaces;
using Services.PopupGenerator;
using TMPro;
using Turrets;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;
using Utils;

[Serializable]
public struct BuyButtonData
{
    public TurretType turretType;
    public Button button;
}

namespace UI.Managers
{
    public class CanvasPresenter : MonoBehaviour
    {
        [SerializeField] private List<BuyButtonData> buttons;
        [SerializeField] private TextMeshProUGUI coinsQuantity;
        [SerializeField] private SpawnTurretEvent spawnTurretEvent;
        
        private IPopupGenerator _popupGenerator;
        private IEconomy _economyManagerService;
        private ITurretLoader _turretLoaderService;
        
        private void Awake()
        {
            foreach (var button in buttons)
            {
                button.button.onClick.AddListener(() => CheckIfPlayerCanBuyTurret(button.turretType));
            }

            _popupGenerator = ServiceLocator.Instance.GetService<IPopupGenerator>();
            _economyManagerService = ServiceLocator.Instance.GetService<IEconomy>();
            _turretLoaderService = ServiceLocator.Instance.GetService<ITurretLoader>();
            UpdateCoins(null);
        }

        public void UpdateCoins(UpdateCoinsEvent updateCoinsEvent)
        {
            coinsQuantity.SetText($"Coins {_economyManagerService.Coins.ToString()}");

            SetStatusBuyButtons(_economyManagerService.Coins >= Constansts.MINIMUM_TURRET_COST);
        }

        public void BaseHasBeenDestroyed(BaseHasBeenDestroyedEvent baseHasBeenDestroyedEvent)
        {
            var popup = _popupGenerator.InstantiatePopup<LosePopup>(PopupType.LoseGame);
            popup.gameObject.SetActive(true);
        }

        public void PlayerHasWon(PlayerHasWonGameEvent baseHasBeenDestroyedEvent)
        {
            var popup = _popupGenerator.InstantiatePopup<VictoryPopup>(PopupType.WinGame);
            popup.gameObject.SetActive(true);
        }

        public void TurretIsBeingMovedEvent(OnTurretIsBeingMovedEvent turretEvent)
        {
            SetStatusBuyButtons(false);
        }

        public void TurretStopBeingMovedEvent(OnTurretStopsBeingMovedEvent turretEvent)
        {
            SetStatusBuyButtons(_economyManagerService.Coins >= Constansts.MINIMUM_TURRET_COST);
        }

        private void SetStatusBuyButtons(bool status)
        {
            foreach (var button in buttons)
            {
                button.button.interactable = status;
            }
        }

        private void CheckIfPlayerCanBuyTurret(TurretType turretType)
        {
            //I Could fired an event to do this block in TurretSpawnerManager
            //but I prefer to have this here. It easier to show a panel or something if player can't buy a turret.

            var playerCoins = _economyManagerService.Coins;
            var turretData = _turretLoaderService.GetTurretByType(turretType);
            var turretCost = turretData.cost;
            if (playerCoins < turretCost)
            {
                Debug.Log($"Player can't buy this turret {turretType}");
                return;
            }

            spawnTurretEvent.turretToSpawn = turretData.prefab;
            spawnTurretEvent.turretCost = turretData.cost;
            spawnTurretEvent.Fire();
        }
    }
}