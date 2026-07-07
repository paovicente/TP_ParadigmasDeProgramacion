using EngineGDI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class ProjectileTests
    {
        [TestMethod]
        public void ProjectileDeactivatesCorrectly()
        {
            var transform = new Transform();
            var renderer = new Renderer("Bullet.png", transform);

            var projectile = new Projectile(transform, renderer, 100);

            var startPosition = new Vector2(0, 0);
            var direction = new Vector2(0, 1);

            projectile.Activate(startPosition, direction, 2, "Bullet.png");

            Assert.IsTrue(projectile.IsActive);

            projectile.Deactivate();

            Assert.IsFalse(projectile.IsActive);
        }
    }
}
