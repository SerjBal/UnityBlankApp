
namespace Serjbal.App
{
    public class AppEnter : MonoApp
    {
        private void Start()
        {
            _app.StateMachine.GetCurrentState().Execute();
        }
    }
}
