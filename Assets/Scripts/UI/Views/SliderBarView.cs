using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class SliderBarView : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;

        public void SetMaxValue(float maxValue)
        {
            slider.maxValue = maxValue;
            gradient.Evaluate(1f);
            SetValue(maxValue);
        }

        public void SetValue(float value)
        {
            slider.value = value;

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}