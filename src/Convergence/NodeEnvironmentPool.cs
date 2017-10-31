namespace Convergence
{
    public interface INodeEnvironmentPool
    {
        NodeEnvironment Get();
        void Return(NodeEnvironment item);
    }

    public class NodeEnvironmentPool : INodeEnvironmentPool
    {
        private readonly INodeConfiguration _nodeConfiguration;
        private readonly ConcurrentObjectPool<NodeEnvironment> _pool;

        public NodeEnvironmentPool(INodeConfiguration nodeConfiguration)
        {
            _nodeConfiguration = nodeConfiguration;
            _pool = new ConcurrentObjectPool<NodeEnvironment>(() =>
            {
                return new NodeEnvironment(_nodeConfiguration);
            });
        }

        public NodeEnvironment Get()
        {
            return _pool.Get();
        }

        public void Return(NodeEnvironment item)
        {
            _pool.Return(item);
        }
    }
}
