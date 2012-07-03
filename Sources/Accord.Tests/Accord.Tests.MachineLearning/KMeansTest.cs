﻿// Accord Unit Tests
// The Accord.NET Framework
// http://accord.googlecode.com
//
// Copyright © César Souza, 2009-2012
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

using Accord.MachineLearning;
using Accord.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Accord.Tests.MachineLearning
{


    /// <summary>
    ///This is a test class for KMeansTest and is intended
    ///to contain all KMeansTest Unit Tests
    ///</summary>
    [TestClass()]
    public class KMeansTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for KMeans Constructor
        ///</summary>
        [TestMethod()]
        public void KMeansConstructorTest()
        {

            Accord.Math.Tools.SetupGenerator(0);


            // Declare some observations
            double[][] observations = 
            {
                new double[] { -5, -2, -1 },
                new double[] { -5, -5, -6 },
                new double[] {  2,  1,  1 },
                new double[] {  1,  1,  2 },
                new double[] {  1,  2,  2 },
                new double[] {  3,  1,  2 },
                new double[] { 11,  5,  4 },
                new double[] { 15,  5,  6 },
                new double[] { 10,  5,  6 },
            };

            // Create a new K-Means algorithm with 3 clusters 
            KMeans kmeans = new KMeans(3);

            // Compute the algorithm, retrieving an integer array
            //  containing the labels for each of the observations
            int[] labels = kmeans.Compute(observations);

            // As a result, the first two observations should belong to the
            //  same cluster (thus having the same label). The same should
            //  happen to the next four observations and to the last three.

            Assert.AreEqual(labels[0], labels[1]);

            Assert.AreEqual(labels[2], labels[3]);
            Assert.AreEqual(labels[2], labels[4]);
            Assert.AreEqual(labels[2], labels[5]);

            Assert.AreEqual(labels[6], labels[7]);
            Assert.AreEqual(labels[6], labels[8]);

            Assert.AreNotEqual(labels[0], labels[2]);
            Assert.AreNotEqual(labels[2], labels[6]);
            Assert.AreNotEqual(labels[0], labels[6]);


            int[] labels2 = kmeans.Classify(observations);
            Assert.IsTrue(labels.IsEqual(labels2));
        }

        /// <summary>
        ///A test for KMeans Constructor
        ///</summary>
        [TestMethod()]
        public void KMeansConstructorTest2()
        {

            Accord.Math.Tools.SetupGenerator(0);


            // Declare some observations
            double[][] observations = 
            {
                new double[] { -5, -2, -1 },
                new double[] { -5, -5, -6 },
                new double[] {  2,  1,  1 },
                new double[] {  1,  1,  2 },
                new double[] {  1,  2,  2 },
                new double[] {  3,  1,  2 },
                new double[] { 11,  5,  4 },
                new double[] { 15,  5,  6 },
                new double[] { 10,  5,  6 },
            };

            double error, e;

            // Create a new algorithm
            KMeans kmeans = new KMeans(3);
            kmeans.Randomize(observations);

            // Save the first initialization
            double[][] initial = kmeans.Clusters.Centroids;

            // Compute the first K-Means
            kmeans.Compute(observations, out error);

            // Create more K-Means algorithms 
            //  with the same initializations
            for (int i = 0; i < 1000; i++)
            {
                kmeans = new KMeans(3);
                kmeans.Clusters.Centroids = initial;
                kmeans.Compute(observations, out e);

                Assert.AreEqual(error, e);
            }

            // Create more K-Means algorithms 
            //  without the same initialization
            bool differ = false;
            for (int i = 0; i < 1000; i++)
            {
                kmeans = new KMeans(3);
                kmeans.Compute(observations, out e);

                if (error != e) differ = true;
            }

            Assert.IsTrue(differ);
        }

        /// <summary>
        ///A test for KMeans Constructor
        ///</summary>
        [TestMethod()]
        public void KMeansConstructorTest3()
        {
            // Create a new algorithm
            KMeans kmeans = new KMeans(3);
            Assert.IsNotNull(kmeans.Clusters);
            Assert.IsNotNull(kmeans.Distance);
            Assert.IsNotNull(kmeans.Clusters.Centroids);
            Assert.IsNotNull(kmeans.Clusters.Count);
            Assert.IsNotNull(kmeans.Clusters.Covariances);
            Assert.IsNotNull(kmeans.Clusters.Proportions);
        }
    }
}