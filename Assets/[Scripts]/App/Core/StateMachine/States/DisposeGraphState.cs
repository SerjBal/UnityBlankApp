using System;

namespace Serjbal.App
{
    public class DisposeGraphsState : AppState
    {
        protected App _app;

        public DisposeGraphsState(App app)
        {
            _app = app;
        }

        public override bool Execute()
        {
            //foreach (IGraph graph in _app.GraphContainer.GetAllGraphs())
            //{
            //    (graph as IDisposable)?.Dispose();
            //}

            return true;
        }

        public override bool Enter() => true;
        public override bool Exit() => true;
    }
}