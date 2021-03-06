﻿// Accord Unit Tests
// The Accord.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2014
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.Tests.Math
{
    using System;
    using Accord.Math.Optimization;
    using Accord.Math.Optimization.Constrained;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class NonlinearConstraintTest
    {


        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }



        [TestMethod]
        public void ConstructorTest6()
        {
            var constraints = new[]
            {
                new NonlinearConstraint(2, x =>  1.0 - x[0] * x[0] - x[1] * x[1]),
                new NonlinearConstraint(2, x =>  1.0 - x[0] * x[0] - x[1] * x[1] >= 0),
                new NonlinearConstraint(2, x =>  -x[0] * x[0] - x[1] * x[1] >= -1.0),
                new NonlinearConstraint(2, x =>  -(-x[0] * x[0] - x[1] * x[1]) <= 1.0),
            };

            Assert.AreEqual(ConstraintType.GreaterThanOrEqualTo, constraints[0].ShouldBe);
            Assert.AreEqual(ConstraintType.GreaterThanOrEqualTo, constraints[1].ShouldBe);
            Assert.AreEqual(ConstraintType.GreaterThanOrEqualTo, constraints[2].ShouldBe);
            Assert.AreEqual(ConstraintType.LesserThanOrEqualTo, constraints[3].ShouldBe);

            Assert.AreEqual(0.0, constraints[0].Value);
            Assert.AreEqual(0.0, constraints[1].Value);
            Assert.AreEqual(-1.0, constraints[2].Value);
            Assert.AreEqual(1.0, constraints[3].Value);

            foreach (var c1 in constraints)
            {
                double v1 = c1.GetViolation(new double[] { 4, 2 });

                foreach (var c2 in constraints)
                {
                    double v2 = c2.GetViolation(new double[] { 4, 2 });

                    Assert.AreEqual(v1, v2);
                }
            }
        }

    }
}
