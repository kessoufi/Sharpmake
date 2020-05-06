﻿// Copyright (c) 2017 Ubisoft Entertainment
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Sharpmake.UnitTests
{
    internal static class UniqueListTests
    {
        /// <summary>
        ///     Verify if the <c>OrderableStrings</c> was copy in the array
        /// </summary>
        [Test]
        public static void TestToString()
        {
            UniqueList<string> uniqueList1 = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            UniqueList<string> uniqueList2 = new UniqueList<string>
            {
                "U",
                "D",
                "H"
            };
            Assert.AreEqual("AA,BBB,CC,DDD", uniqueList1.ToString());
            Assert.AreEqual("U,D,H", uniqueList2.ToString());
        }

        /// <summary>
        ///     Verify that the value specified was updated
        /// </summary>
        [Test]
        public static void TestUpdateValue()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            uniqueList.UpdateValue("AA", "EE");

            Assert.AreEqual("EE,BBB,CC,DDD", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that the value was not duplicated
        /// </summary>
        [Test]
        public static void TestUpdateValueUnique()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            uniqueList.UpdateValue("BBB", "AA");

            Assert.AreEqual("AA,CC,DDD", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that the specified value was added
        /// </summary>
        [Test]
        public static void TestAddOneValue()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            uniqueList.Add("EE");

            Assert.AreEqual("AA,BBB,CC,DDD,EE", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that two specified values were added
        /// </summary>
        [Test]
        public static void TestAddTwoValues()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            uniqueList.Add("EE", "FFF");

            Assert.AreEqual("AA,BBB,CC,DDD,EE,FFF", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that the values in the array were added
        /// </summary>
        [Test]
        public static void TestAddTabValues()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            uniqueList.Add(new[]{
                "EE",
                "FFF",
                "GG",
                "H"
            });

            Assert.AreEqual("AA,BBB,CC,DDD,EE,FFF,GG,H", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that the values in the <c>IEnumerable</c> were added
        /// </summary>
        [Test]
        public static void TestAddRange()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            IEnumerable<string> listParams = new List<string>
            {
                "EE",
                "FFF",
                "GG",
                "H"
            };

            uniqueList.AddRange(listParams);

            Assert.AreEqual("AA,BBB,CC,DDD,EE,FFF,GG,H", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that the remaining element was the one include in both list
        /// </summary>
        [Test]
        public static void TestIntersectWith()
        {
            UniqueList<string> uniqueList1 = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            UniqueList<string> uniqueList2 = new UniqueList<string>
            {
                "AA",
                "D",
                "H"
            };

            uniqueList1.IntersectWith(uniqueList2);

            Assert.AreEqual("AA", uniqueList1.ToString());
        }

        /// <summary>
        ///     Verify that all element were removed because both <c>UniqueList</c> have different elements
        /// </summary>
        [Test]
        public static void TestNoIntersectWith()
        {
            UniqueList<string> uniqueList1 = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            UniqueList<string> uniqueList2 = new UniqueList<string>
            {
                "U",
                "D",
                "H"
            };

            uniqueList1.IntersectWith(uniqueList2);

            Assert.AreEqual(0, uniqueList1.Count);
        }

        /// <summary>
        ///     Verify if the right elements were removed from the <c>uniqueList</c> and added to <c>uniqueRest</c>
        /// </summary>
        [Test]
        public static void TestIntersectWithRest()
        {
            UniqueList<string> uniqueList1 = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            UniqueList<string> uniqueList2 = new UniqueList<string>
            {
                "AA",
                "G",
                "H"
            };
            UniqueList<string> uniqueRest = new UniqueList<string>();

            uniqueList1.IntersectWith(uniqueList2, uniqueRest);

            Assert.AreEqual("AA", uniqueList1.ToString());
            Assert.AreEqual("G,H,BBB,CC,DDD", uniqueRest.ToString());
        }

        /// <summary>
        ///     Verify if all the elements were removed from the <c>uniqueList</c> and added to <c>uniqueRest</c>
        /// </summary>
        [Test]
        public static void TestNoIntersectWithRest()
        {
            UniqueList<string> uniqueList1 = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            UniqueList<string> uniqueList2 = new UniqueList<string>
            {
                "U",
                "D",
                "H"
            };
            UniqueList<string> uniqueRest = new UniqueList<string>();

            uniqueList1.IntersectWith(uniqueList2, uniqueRest);
            Assert.AreEqual(0, uniqueList1.Count);
            Assert.AreEqual("U,D,H,AA,BBB,CC,DDD", uniqueRest.ToString());
        }

        /// <summary>
        ///     Verify that the element with a length of 3 were removed
        /// </summary>
        [Test]
        public static void TestRemoveAll()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            uniqueList.RemoveAll(s => s.Length == 3);

            Assert.AreEqual("AA,CC", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that the elements include in the list were removed from <c>UniqueList</c>
        /// </summary>
        [Test]
        public static void TestRemoveRange()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            IEnumerable<string> listParams = new List<string>
            {
                "DDD",
                "F",
                "CC"
            };

            uniqueList.RemoveRange(listParams);

            Assert.AreEqual("AA,BBB", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that the elements include in the array were removed
        /// </summary>
        [Test]
        public static void TestRemoveTab()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            string[] listParams = { "AA", "BBB", "CC" };

            uniqueList.Remove(listParams);

            Assert.AreEqual("DDD", uniqueList.ToString());
        }

        /// <summary>
        ///     Verify that the order of the element is according to their length
        /// </summary>
        [Test]
        public static void TestGetValuesWithCustomSort()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            List<string> listReturn = uniqueList.GetValuesWithCustomSort((x, y) =>
            {
                return x.Length.CompareTo(y.Length);
            });

            Assert.AreEqual(new List<string>() { "AA", "CC", "BBB", "DDD" }, listReturn);
        }

        /// <summary>
        ///     Verify that all elements were removed
        /// </summary>
        [Test]
        public static void TestClear()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };
            uniqueList.Clear();

            Assert.AreEqual(0, uniqueList.Count);
        }

        /// <summary>
        ///     Verify <c>Contains</c> return False if an existent element is tested
        /// </summary>
        [Test]
        public static void TestContainsTrue()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };

            Assert.IsTrue(uniqueList.Contains("BBB"));
        }

        /// <summary>
        ///     Verify <c>Contains</c> return False if an nonexistent element is tested
        /// </summary>
        [Test]
        public static void TestContainsFalse()
        {
            UniqueList<string> uniqueList = new UniqueList<string>
            {
                "AA",
                "BBB",
                "CC",
                "DDD"
            };

            Assert.IsFalse(uniqueList.Contains("HHH"));
        }

        private class ListContainer
        {
            public ListContainer()
            {
                List.Clear();
            }
            public UniqueList<string> List = new UniqueList<string>();
            public IEnumerable<string> SortedList => List.SortedValues;
        }

        /// <summary>
        /// The goal of this test is to test the reliability of the SortedValues property in UniqueList when
        /// used via another property using IEnumerable similar to the way it is used in Project.Configuration for the 
        /// ResolvedEventCustomPreBuildExe property. Previous implementation of UniqueList was sometimes causing 
        /// exception when multiple threads were accessing the property.
        /// </summary>
        [Test]
        public static void MultithreadEmptyValuesSorted()
        {
            int nbrThreads = Environment.ProcessorCount;
            var container = new ListContainer();

            int TOTAL_TEST_COUNT = 100000;
            long nbrThreadsFinished = 0;
            long nbrThreadsGate1 = 0;
            Exception taskTestException = null;

            // Note: Using a Barrier to synchronize all the threads at each iteration
            using (Barrier barrier = new Barrier(nbrThreads, (b) =>
             {
                 Interlocked.Increment(ref nbrThreadsGate1);
                 container.List.AddRange(new List<string> { }); // Adding an empty collection makes the UniqueList dirty
             }))
            {
                ThreadPool.TaskCallback taskLambda = (object taskParams) =>
                {
                    var listContainersTask = (ListContainer)taskParams;

                    try
                    {
                        for (int i = 0; i < TOTAL_TEST_COUNT; ++i)
                        {
                            // Synchronize all threads
                            barrier.SignalAndWait();

                            if (taskTestException != null)
                                break; // Abort once we got an exception

                            // Attempt to access the SortedList property from multiple threads. 
                            // It must not create any exception!
                            foreach (var s in container.SortedList)
                            {
                                Console.WriteLine(s);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // Keep the exception around as we will need it in the main thread of this test.
                        taskTestException = e;
                    }
                    finally
                    {
                        barrier.RemoveParticipant(); // Must remove the participant to unblock all the other threads.
                        Interlocked.Increment(ref nbrThreadsFinished);
                    }
                };

                // Start a thread pool and execute tasks.
                using (ThreadPool pool = new ThreadPool())
                {
                    // Add 1 task per thread
                    pool.Start(nbrThreads);
                    for (int i = 0; i < nbrThreads; ++i)
                    {
                        pool.AddTask(taskLambda, container);
                    }

                    // Wait for all the tasks to complete.
                    pool.Wait();

                    // Check the results.
                    TestContext.Out.WriteLine("nbr Finished: {0}, nbr Gate1: {1}", nbrThreadsFinished, nbrThreadsGate1);
                    if (taskTestException != null)
                    {
                        throw taskTestException;
                    }
                }
            }
        }
    }
}
