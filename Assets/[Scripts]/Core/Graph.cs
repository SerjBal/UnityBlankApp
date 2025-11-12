using System;
using System.Collections.Generic;

namespace Serjbal.App
{
    public class Graph : ObservableDictionary<Guid, INode>, IGraph
    {
        protected Guid _guid;

        public Guid Guid => _guid;

        public Graph() => _guid = new Guid();

        public Graph(Guid guid) => _guid = guid;

        public void AddNode(INode node) => Add(node.Guid, node);

        public void RemoveNode(Guid nodeGuid) => Remove(nodeGuid);

        public INode GetNode(Guid nodeGuid) => this[nodeGuid];

        public bool ContainsNodeGuid(Guid nodeGuid) => ContainsKey(nodeGuid);

        public ICollection<INode> GetAllNodes() => Values;
    }
}
