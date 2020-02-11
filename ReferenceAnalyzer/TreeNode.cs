using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReferenceAnalyzer
{
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }
        public TreeNode<T> Parent { get; set; }

        public TreeNode(T value)
        {
            this.Value = value;
            Children = new List<TreeNode<T>>(2);
        }

        public TreeNode<T> Search(Func<TreeNode<T>, bool> f)
        {
            if (f(this)) return this;
            var result = Children.FirstOrDefault(f);
            return result ?? Children.Select(x => x.Search(f)).FirstOrDefault();
        }

        public (TreeNode<T> node, bool) Insert(TreeNode<T> parent, TreeNode<T> node)
        {
            var exisingParent = Search(x => Equals(x.Value, parent.Value));
            if (exisingParent == null)
            {
                if (Insert(node, parent).Item2)
                {
                    return (parent, true);
                }
                return (node, false);
            }

            node.Parent = exisingParent;
            exisingParent.Children.Add(node);
            return (node, true);
        }

        public (TreeNode<T> node, bool) Insert(TreeNode<T> parent, T value)
        {
            var node = new TreeNode<T>(value);
            var exisingParent = Search(x => Equals(x.Value, parent.Value));
            if (exisingParent == null)
            {
                return (node, false);
            }

            node.Parent = exisingParent;
            exisingParent.Children.Add(node);
            return (node, true);
        }

        public override string ToString()
        {
            return this.ToString(0);
        }

        public string ToString(int depth = 0)
        {
            var sb = new StringBuilder();
            string offset = String.Empty;
            for (int i = 0; i < depth; i++)
            {
                offset +="--";
            }
            sb.Append(Value);

            if (Children.Any())
            {
                sb.Append("\r\n");
                sb.Append(string.Join("\r\n", Children.Select(x=> offset + x.ToString(depth + 1))));
            }

            return sb.ToString();
        }
    }
}
