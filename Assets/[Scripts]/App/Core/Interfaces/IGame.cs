namespace Serjbal.App
{
    public interface IGame : IInitializable
    {
        void Init();
        void GameStart();
        void GameWin();
        void GameLoose();
        void GameStop();
    }
}
