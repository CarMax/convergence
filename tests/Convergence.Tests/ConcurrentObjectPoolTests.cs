using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Convergence.Tests
{
    [TestClass]
    public class ConcurrentObjectPoolTests
    {
        private ConcurrentObjectPool<object> _pool;

        [TestInitialize]
        public void Init()
        {
            _pool = new ConcurrentObjectPool<object>(() =>
            {
                return DateTime.Now.ToString();
            });
        }

        [TestMethod]
        public void CanCreateNewObject()
        {
            Assert.AreEqual(0, _pool.Size);

            var item = _pool.Get();

            Assert.IsNotNull(item);

            Assert.AreEqual(1, _pool.Size);
        }

        [TestMethod]
        public void CanReturnItem()
        {
            Assert.AreEqual(0, _pool.Size);

            var item = _pool.Get();

            Assert.AreEqual(1, _pool.Size);

            _pool.Return(item);

            Assert.AreEqual(1, _pool.Size);
        }

        [TestMethod]
        public void CanReuseItem()
        {
            Assert.AreEqual(0, _pool.Size);

            var item = _pool.Get();

            var copy = item.ToString();

            _pool.Return(item);

            var item2 = _pool.Get();

            Assert.AreEqual(item2.ToString(), copy);

            Assert.AreEqual(1, _pool.Size);
        }

        [TestMethod]
        public async Task IsThreadSafe()
        {
            var threads = Enumerable.Range(0, 100).Select(s => Task.Run(() => { _pool.Get(); }));

            await Task.WhenAll(threads);

            Assert.AreEqual(100, _pool.Size);
        }
    }
}
