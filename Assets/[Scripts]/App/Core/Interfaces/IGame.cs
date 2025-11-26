namespace Serjbal.App
{
    public interface IGame : IInitializable
    {
        void GameStart();
        void GameWin();
        void GameLoose();
        void GameStop();
    }
}
