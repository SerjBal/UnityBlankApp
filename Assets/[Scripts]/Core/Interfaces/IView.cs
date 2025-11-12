
namespace Serjbal.App.UI
{
    public interface IView : IInitializable
    {
        IViewable GetPage(PageType pageType);
        void DestroyPage(IViewable page);
        bool IsPageExist(string name, out IViewable page);
        void ClearCanvas();
    }
}
