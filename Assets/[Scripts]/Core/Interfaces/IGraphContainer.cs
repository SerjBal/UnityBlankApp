using System.Collections.Generic;

namespace Serjbal.App
{
    public interface IGraphContainer
    {
        IEnumerable<IGraph> GetAllGraphs();
    }
}