using EngineGDI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class MovementTests
    {
        [TestMethod]
        public void MoveXOk()
        {
            var speed = 10;
            var time = 2;

            //result
            var transform = new Transform();
            transform.Position = new Vector2(0, 0);

            var movement = new Movement(speed);
            var direction = new Vector2(1, 0); //move only in x axis

            movement.Move(transform, direction, time);

            //expected result
            var expectedTransform = new Transform();
            expectedTransform.Position = new Vector2(0, 0);

            expectedTransform.Position += direction * speed * time;

            Assert.AreEqual(expectedTransform.Position.X, transform.Position.X);
            Assert.AreEqual(expectedTransform.Position.Y, transform.Position.Y);
        }

        [TestMethod]
        public void MoveYOk()
        {
            var speed = 5;
            var time = 2;

            //result
            var transform = new Transform();
            transform.Position = new Vector2(0, 0);

            var movement = new Movement(speed);
            var direction = new Vector2(0, -1); //move only in y axis
            
            movement.Move(transform, direction, time);

            //expected result
            var expectedTransform = new Transform();
            expectedTransform.Position = new Vector2(0, 0);

            expectedTransform.Position += direction * speed * time;

            Assert.AreEqual(expectedTransform.Position.X, transform.Position.X);
            Assert.AreEqual(expectedTransform.Position.Y, transform.Position.Y);
        }
    }
}
