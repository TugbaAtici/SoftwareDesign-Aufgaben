using System;

namespace Aufgabe10_GenerischerBaumErweitert
{
        class Program
    {
        static void Main(string[] args)
        {
            var root = new TreeNode<string>("I am the Godfather.");
            var child1 = root.CreateNode("abc", root);
            var child2 = root.CreateNode("Hey, me too!", root);
            var gchild11 = root.CreateNode("abc", child1);
            var gchild12 = root.CreateNode("Another brick in the wall.", child1);
            var ggchild121 = root.CreateNode("I'm still an ancestor of the Godfather!", gchild12);
            var gggchild1211 = root.CreateNode("abc", ggchild121);


            root.PrintTree();


            Console.WriteLine("");
            ggchild121.RemoveChild(gggchild1211);
            gchild11.AppendChild(gggchild1211);
            root.PrintTree();


            foreach (var element in root.Find("abc"))
            {
                Console.WriteLine(element);
            }


            // This would cause an infinite loop, since the child element (re)refers to it's root.
            //gggchild1211.AppendChild(root);
            //root.PrintTree();
        }
    }


    class TreeNode<T>
    {
        private T _Content;
        private TreeNode<T> Parent;
        private List<TreeNode<T>> Children = new List<TreeNode<T>>();


        public TreeNode(T content, TreeNode<T> parent = null)
        {
            _Content = content;
            if (parent != null)
            {
                parent.AppendChild(this);
            }
        }


        public TreeNode<T> CreateNode(T content, TreeNode<T> parent = null)
        {
            return new TreeNode<T>(content, parent);
        }


        public void AppendChild(TreeNode<T> node)
        {
            Children.Add(node);
            node.Parent = this;
        }


        public void RemoveChild(TreeNode<T> node)
        {
            Children.Remove(node);
        }


        public void PrintTree(string prefix = "")
        {
            Console.WriteLine(ToString());


            if (Children.Count != 0)
            {
                TreeNode<T> lastChild = Children.Last();
                foreach (TreeNode<T> child in Children)
                {
                    if (child == lastChild)
                    {
                        Console.Write(prefix + "└─ ");
                        child.PrintTree(prefix + "   ");
                    }
                    else
                    {
                        Console.Write(prefix + "├─ ");
                        child.PrintTree(prefix + "|  ");
                    }
                }
            }
        }


        public override string ToString()
        {
            return _Content.ToString();
        }


        public List<TreeNode<T>> Find(T searchFor, List<TreeNode<T>> result = null)
        {
            if (result == null)
            {
                result = new List<TreeNode<T>>();
            }
            if (_Content.Equals(searchFor)) {
                result.Add(this);
            }
            foreach(var child in Children)
            {
                child.Find(searchFor, result);
            }
            return result;
        }
    }
}

