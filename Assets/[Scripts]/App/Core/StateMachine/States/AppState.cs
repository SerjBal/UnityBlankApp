namespace Serjbal.App
{
    public class AppState : IState
    {
        public virtual bool Execute() => true;
        public virtual bool Enter() => true;
        public virtual bool Exit() => true;
    }
}