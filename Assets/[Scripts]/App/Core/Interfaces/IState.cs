namespace Serjbal.App
{
    public interface IState : IExecutable
    {
        public bool Enter();
        public bool Exit();
    }
}