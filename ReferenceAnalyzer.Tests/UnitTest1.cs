using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReferenceAnalyzer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var node = new TreeNode<int>(1);
            node.Insert(node, 3);
            var found = node.Search(x => x.Value == 3);
            Assert.IsNotNull(found);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var r = new Random();
            var node = new TreeNode<int>(1);
            var maxValue = 10;

            for (int i = 0; i <= 20; i++)
            {
                
                if (i % 2 == 0)
                {
                    node.Insert(node, r.Next(maxValue));
                }
                else
                {
                    var newNode = node.Search(x => r.Next(maxValue) == x.Value);
                    if (newNode != null)
                    {
                        node.Insert(newNode, r.Next(maxValue));
                    }
                }
            }

            Debug.Print(node.ToString());
            Assert.IsFalse(false);
        }
    }
}
