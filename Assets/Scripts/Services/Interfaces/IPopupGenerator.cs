using Services.PopupGenerator;

namespace Services.Interfaces
{
    public interface IPopupGenerator
    {
        T InstantiatePopup<T>(PopupType popupType);
    }
}