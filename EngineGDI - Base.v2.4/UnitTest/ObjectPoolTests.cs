using EngineGDI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class ObjectPoolTests
    {
        private TestPoolable CreatePoolable()
        {
            return new TestPoolable();
        }

        [TestMethod]
        public void PoolCreatesCorrectAmount() //check if the pool creates the specified quantity of elements
        {
            var pool = new ObjectPool<TestPoolable>(8, CreatePoolable);

            Assert.AreEqual(8, pool.Objects.Count);
        }

        [TestMethod]
        public void GetReturnsInactiveObject() //test that the object pool correctly return an object
        {
            var pool = new ObjectPool<TestPoolable>(3, CreatePoolable);

            var obj = pool.Get();

            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void GetReturnsNullWhenAllObjectsAreActive() //test if all pool elements are active, meaning that they are unavailable then it will return null
        {
            var pool = new ObjectPool<TestPoolable>(1, CreatePoolable);

            var obj = pool.Get();

            obj.IsActive = true;

            var result = pool.Get();

            Assert.IsNull(result);
        }
    }

    public class TestPoolable : IPoolable
    {
        public bool IsActive { get; set; }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
