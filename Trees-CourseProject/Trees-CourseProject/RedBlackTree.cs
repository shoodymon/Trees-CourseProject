using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Trees_CourseProject
{

    public class RedBlackTree
    {
        public enum Color { Red, Black }

        public class RBNode
        {
            public int Value;
            public Color NodeColor;
            public RBNode Left;
            public RBNode Right;
            public RBNode Parent;

            public RBNode(int value)
            {
                Value = value;
                NodeColor = Color.Red;
                Left = null;
                Right = null;
                Parent = null;
            }
        }

        public RBNode root;
        private Canvas canvas;

        public RedBlackTree(Canvas canvas)
        {
            root = null;
            this.canvas = canvas;
        }

        public void Insert(int value)
        {
            RBNode newNode = new RBNode(value);
            root = Insert(root, newNode);
            FixViolation(newNode);
        }

        private RBNode Insert(RBNode root, RBNode node)
        {
            if (root == null)
                return node;

            if (node.Value < root.Value)
            {
                root.Left = Insert(root.Left, node);
                root.Left.Parent = root;
            }
            else if (node.Value > root.Value)
            {
                root.Right = Insert(root.Right, node);
                root.Right.Parent = root;
            }
            return root;
        }

        private void FixViolation(RBNode node)
        {
            while (node != root && node.Parent.NodeColor == Color.Red)
            {
                RBNode parentNode = node.Parent;
                RBNode grandparent = parentNode.Parent;

                if (parentNode == grandparent.Left)
                {
                    RBNode uncle = grandparent.Right;
                    if (uncle != null && uncle.NodeColor == Color.Red)
                    {
                        grandparent.NodeColor = Color.Red;
                        parentNode.NodeColor = Color.Black;
                        uncle.NodeColor = Color.Black;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == parentNode.Right)
                        {
                            LeftRotate(parentNode);
                            node = parentNode;
                            parentNode = node.Parent;
                        }
                        RightRotate(grandparent);
                        Color tempColor = parentNode.NodeColor;
                        parentNode.NodeColor = grandparent.NodeColor;
                        grandparent.NodeColor = tempColor;
                        node = parentNode;
                    }
                }
                else
                {
                    RBNode uncle = grandparent.Left;
                    if (uncle != null && uncle.NodeColor == Color.Red)
                    {
                        grandparent.NodeColor = Color.Red;
                        parentNode.NodeColor = Color.Black;
                        uncle.NodeColor = Color.Black;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == parentNode.Left)
                        {
                            RightRotate(parentNode);
                            node = parentNode;
                            parentNode = node.Parent;
                        }
                        LeftRotate(grandparent);
                        Color tempColor = parentNode.NodeColor;
                        parentNode.NodeColor = grandparent.NodeColor;
                        grandparent.NodeColor = tempColor;
                        node = parentNode;
                    }
                }
            }
            root.NodeColor = Color.Black;
        }

        public void Delete(int value)
        {
            root = Delete(root, value);
        }

        private RBNode Delete(RBNode node, int value)
        {
            if (node == null)
                return null;

            if (value < node.Value)
                node.Left = Delete(node.Left, value);
            else if (value > node.Value)
                node.Right = Delete(node.Right, value);
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    RBNode temp = node.Left ?? node.Right;

                    if (temp == null)
                    {
                        temp = node;
                        node = null;
                    }
                    else
                    {
                        node = temp;
                    }

                    if (node != null)
                        node.Parent = temp.Parent;
                }
                else
                {
                    RBNode temp = FindMin(node.Right);
                    node.Value = temp.Value;
                    node.Right = Delete(node.Right, temp.Value);
                }
            }

            if (node == null)
                return node;

            return FixDoubleBlack(node);
        }

        private RBNode FindMin(RBNode node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        private RBNode FixDoubleBlack(RBNode node)
        {
            if (node == root)
                return node;

            RBNode sibling = GetSibling(node);
            RBNode parent = node.Parent;

            if (sibling == null)
            {
                return FixDoubleBlack(parent);
            }

            if (sibling.NodeColor == Color.Red)
            {
                parent.NodeColor = Color.Red;
                sibling.NodeColor = Color.Black;
                if (sibling == parent.Left)
                    RightRotate(parent);
                else
                    LeftRotate(parent);
                sibling = GetSibling(node);
            }

            if ((sibling.Left == null || sibling.Left.NodeColor == Color.Black) &&
                (sibling.Right == null || sibling.Right.NodeColor == Color.Black))
            {
                sibling.NodeColor = Color.Red;
                if (parent.NodeColor == Color.Black)
                    return FixDoubleBlack(parent);
                else
                    parent.NodeColor = Color.Black;
            }
            else
            {
                if (sibling == parent.Left)
                {
                    if (sibling.Left != null && sibling.Left.NodeColor == Color.Red)
                    {
                        sibling.Left.NodeColor = sibling.NodeColor;
                        sibling.NodeColor = parent.NodeColor;
                        RightRotate(parent);
                    }
                    else
                    {
                        sibling.Right.NodeColor = parent.NodeColor;
                        LeftRotate(sibling);
                        RightRotate(parent);
                    }
                }
                else
                {
                    if (sibling.Right != null && sibling.Right.NodeColor == Color.Red)
                    {
                        sibling.Right.NodeColor = sibling.NodeColor;
                        sibling.NodeColor = parent.NodeColor;
                        LeftRotate(parent);
                    }
                    else
                    {
                        sibling.Left.NodeColor = parent.NodeColor;
                        RightRotate(sibling);
                        LeftRotate(parent);
                    }
                }
                parent.NodeColor = Color.Black;
            }

            return node;
        }

        private RBNode GetSibling(RBNode node)
        {
            if (node.Parent == null)
                return null;

            if (node == node.Parent.Left)
                return node.Parent.Right;
            else
                return node.Parent.Left;
        }

        private void LeftRotate(RBNode node)
        {
            RBNode newRoot = node.Right;
            node.Right = newRoot.Left;
            if (newRoot.Left != null)
                newRoot.Left.Parent = node;
            newRoot.Parent = node.Parent;
            if (node.Parent == null)
                root = newRoot;
            else if (node == node.Parent.Left)
                node.Parent.Left = newRoot;
            else
                node.Parent.Right = newRoot;
            newRoot.Left = node;
            node.Parent = newRoot;
        }

        private void RightRotate(RBNode node)
        {
            RBNode newRoot = node.Left;
            node.Left = newRoot.Right;
            if (newRoot.Right != null)
                newRoot.Right.Parent = node;
            newRoot.Parent = node.Parent;
            if (node.Parent == null)
                root = newRoot;
            else if (node == node.Parent.Right)
                node.Parent.Right = newRoot;
            else
                node.Parent.Left = newRoot;
            newRoot.Right = node;
            node.Parent = newRoot;
        }

        public bool Search(int value)
        {
            return Search(root, value);
        }

        private bool Search(RBNode node, int value)
        {
            if (node == null)
                return false;

            if (value == node.Value)
                return true;

            if (value < node.Value)
                return Search(node.Left, value);
            else
                return Search(node.Right, value);
        }
    }

}
