using EngineGDI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class Vector2Tests
    {

        [TestMethod]
        public void AddVectorsOk()
        {

            var a = new Vector2(2, 3);
            var b = new Vector2(4, 5);

            var result = a + b; //in vector 2 class we use operator + not a void, that is the reason because here we use + instead of add(a,b)

            Assert.AreEqual(6, result.X);
            Assert.AreEqual(8, result.Y);
        }

        [TestMethod]
        public void SubtractVectorsOk()
        {
            var a = new Vector2(10, 8);
            var b = new Vector2(4, 3);

            var result = a - b; //in vector 2 class we use operator - not a void, that is the reason because here we use - instead of substract(a,b)

            Assert.AreEqual(6, result.X);
            Assert.AreEqual(5, result.Y);
        }

        [TestMethod]
        public void MultiplyVectorByScalarOk()
        {
            var vector = new Vector2(2, 3);
            float scalar = 2;

            var result = vector * scalar; //in vector 2 class we use operator * not a void, that is the reason because here we use * instead of multiply(vector,scalar)

            Assert.AreEqual(4, result.X);
            Assert.AreEqual(6, result.Y);
        }

        [TestMethod]
        public void MagnitudeOk()
        {
            var vector = new Vector2(3, 4);

            var result = vector.Magnitude();

            var expectedResult = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void NormalizeOk()
        {
            var vector = new Vector2(3, 4);

            var result = vector.Normalize();

            var mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            var expectedResult = new Vector2(vector.X / mag, vector.Y / mag);

            Assert.AreEqual(expectedResult.X, result.X);
            Assert.AreEqual(expectedResult.Y, result.Y);
        }

        [TestMethod]
        public void DistanceOk()
        {
            var a = new Vector2(1, 1);
            var b = new Vector2(4, 5);

            var result = Vector2.Distance(a, b);

            var substract = a - b;
            var expectedResult = (float)Math.Sqrt(substract.X * substract.X + substract.Y * substract.Y);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
