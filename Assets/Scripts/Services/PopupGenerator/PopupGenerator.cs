using System;
using Services.Interfaces;
using Services.Utils;
using UnityEngine;

namespace Services.PopupGenerator
{
    [Serializable]
    public struct PopupGetter
    {
        public PopupType PopupType;
        public GameObject Prefab;
    }

    public enum PopupType
    {
        Empty, LoseGame, WinGame
    }

    [CreateAssetMenu(fileName = "PopupGenerator", menuName = "Services/PopupGenerator")]
    public class PopupGenerator : LoadableComponent, IPopupGenerator
    {
        [SerializeField] private PopupList popupList;
        private GameObject _currentOpenedPopup;
        private Camera _camera;
        private int _currentSortingOrder;
        private Transform _positionWhereSpawn;


        public T InstantiatePopup<T>(PopupType popupType)
        {
            _currentOpenedPopup = InstantiatePopup(popupType);
            return _currentOpenedPopup.GetComponent<T>();
        }

        private GameObject InstantiatePopup(PopupType popupType)
        {
            var prefab = popupList.GetPrefabByType(popupType);

            _currentOpenedPopup = Instantiate(prefab, _positionWhereSpawn, false);
            _currentOpenedPopup.gameObject.SetActive(false);
            var canvas = _currentOpenedPopup.GetComponentInChildren<Canvas>();
            if (canvas.sortingOrder <= _currentSortingOrder)
            {
                canvas.sortingOrder = _currentSortingOrder + 1;
            }

            canvas.worldCamera = _camera;
            _currentSortingOrder = canvas.sortingOrder;
            return _currentOpenedPopup;
        }

        public override void Execute()
        {
            _camera = Camera.main;
            _positionWhereSpawn = new GameObject().transform;
            _positionWhereSpawn.name = "PopupsContainer";
        }
    }
}