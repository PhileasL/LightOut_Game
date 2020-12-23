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
        // TestCartesianCoordMethod tests the Coord method of Cartesian class
        [Test]
        public void TestCartesianCoordMethod()
        {
            float x = 1,
                y = 2;
            Cartesian toTest = new Cartesian(x, y);

            Assert.AreEqual(toTest.Coord(0), x, "x expected to be equals to toTest.Coord(0), x = " + x.ToString() + " toTest.Coord(0) = " + toTest.Coord(0).ToString());
            Assert.AreEqual(toTest.Coord(1), y, "y expected to be equals to toTest.Coord(1), y = " + y.ToString() + " toTest.Coord(0) = " + toTest.Coord(1).ToString());
        }

        // TestCartesianStringMethod tests the String method of Cartesian class
        [Test]
        public void TestCartesianStringMethod()
        {
            float x = 1,
                y = 2;
            Cartesian toTest = new Cartesian(x, y);

            Assert.AreEqual(toTest.String(), "x: 1 y: 2", "expected output: \"x: 1 y: 2\" but got \"" + toTest.String() + "\"");
        }

        // TestCartesianEqualsMethod tests the Equals method of Cartesian class
        [Test]
        public void TestCartesianEqualsMethod()
        {
            float x = 1,
                y = 2,
                z = 3;
            Cartesian toTest = new Cartesian(x, y);
            Cartesian toTest1 = new Cartesian(x, y);
            Cartesian toTest2 = new Cartesian(x, z);

            Assert.IsTrue(toTest.Equals(toTest1), "expected toTest to be equals to toTest1 but it's not");
            Assert.IsFalse(toTest.Equals(toTest2), "expected toTest not to be equals to toTest2 but it's not");
        }

        // TestCartesianGetHashCodeMethod tests the HashCodeASsignation method of Cartesian class to be different on each Cartesian object of a 10x10 board
        [Test]
        public void TestCartesianGetHashCodeMethod()
        {
            List<Cartesian> listCartesian = new List<Cartesian>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    listCartesian.Add(new Cartesian((float)i, (float)j));
                }
            }

            for (int i = 0; i < listCartesian.Count; i++)
            {
                for (int j = 0; j < listCartesian.Count; j++)
                {
                    if (!listCartesian[i].Equals(listCartesian[j]))
                    {
                        Assert.AreNotEqual(listCartesian[i].GetHashCode(), listCartesian[j].GetHashCode(), "expected all the hash code to be different but it's not, " +
                            "here Cartesian description: " + listCartesian[i].String() + " have the same hash code as Cartesian description:" + listCartesian[j].String() +
                            " hash code:" + listCartesian[i].GetHashCode().ToString());
                    }
                }
            }
        }

        // TestCartesianDistanceBetween2CoordsMethod tests the DistanceBetween2Coords method of Cartesian class
        [Test]
        public void TestCartesianDistanceBetween2CoordsMethod()
        {
            float x = 1,
                y = 2;
            Cartesian toTest = new Cartesian(x, x);
            Cartesian toTest1 = new Cartesian(x, y);
            Cartesian toTest2 = new Cartesian(y, y);

            Assert.IsTrue(System.Math.Abs(1.0f - Cartesian.DistanceBetween2Coords(toTest, toTest1)) < 0.0001f, "expected Cartesian.DistanceBetween2Coords(toTest, toTest1) to be near from 1.0," +
                " the value is " + Cartesian.DistanceBetween2Coords(toTest, toTest1).ToString());
            Assert.IsTrue((float)System.Math.Abs(System.Math.Sqrt(2) - Cartesian.DistanceBetween2Coords(toTest, toTest2)) < 0.0001f, "expected Cartesian.DistanceBetween2Coords(toTest, toTest2) to be near from sqrt(2) ~ 1.41," +
                " the value is " + Cartesian.DistanceBetween2Coords(toTest, toTest2).ToString());
        }
    }
}
