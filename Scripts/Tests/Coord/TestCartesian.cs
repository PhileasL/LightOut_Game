using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Scripts.Coord;

namespace Scripts.Tests.Coord
{
    public class TestCartesian
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestCartesianCoordMethod()
        {
            float x = 1,
                y = 2;
            Cartesian toTest = new Cartesian(x, y);

            Assert.AreEqual(toTest.Coord(0), x);
            Assert.AreEqual(toTest.Coord(1), y);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestCartesianWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
