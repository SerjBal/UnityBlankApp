
namespace Serjbal.App.MVVM
{
    public interface IView : IInitializable
    {
        IViewable GetPage(PageName pageType);
        void DestroyPage(IViewable page);
        bool IsPageExist(string name, out IViewable page);
        void ClearCanvas();
    }
}
