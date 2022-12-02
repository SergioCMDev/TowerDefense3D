using CommonInterfaces;
using Events;
using UI.Views;
using UnityEngine;

namespace Base
{
    public class Base : MonoBehaviour, IReceiveDamage
    {
        [SerializeField] private BaseHasBeenDestroyedEvent baseHasBeenDestroyedEvent;
        [SerializeField] private float health;
        [SerializeField] private SliderBarView sliderBarView;

        private float _currentHealth;
    
        public void Start()
        {
            _currentHealth = health;
            sliderBarView.SetMaxValue(_currentHealth);
        }
    
        public void ReceiveDamage(float receivedDamage)
        {
            if (_currentHealth - receivedDamage <= 0)
            {
                baseHasBeenDestroyedEvent.Fire();
                return;
            }

            _currentHealth -= receivedDamage;
            UpdateLifeBar();
        }
    
        private void UpdateLifeBar()
        {
            sliderBarView.SetValue(_currentHealth);
        }
    }
}