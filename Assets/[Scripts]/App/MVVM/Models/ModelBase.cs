
namespace Serjbal.App.MVVM
{
    public abstract class ModelBase
    {
        protected App _app;

        public ModelBase(App app) => _app = app;
    }
}
