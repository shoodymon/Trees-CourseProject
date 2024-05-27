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
            // Проверяем, если новый корень существует и он красный, то перекрашиваем его в черный
            if (root != null)
                root.NodeColor = Color.Black;
        }

        private RBNode Delete(RBNode node, int value)
        {
            if (node == null)
                return null;

            if (value < node.Value)
            {
                node.Left = Delete(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Delete(node.Right, value);
            }
            else
            {
                // Узел для удаления найден

                // Если у узла нет потомков или только один потомок
                if (node.Left == null || node.Right == null)
                {
                    return delete_one_child(node);
                }
                else
                {
                    // Узел имеет два потомка
                    // Найдем наибольший узел в левом поддереве
                    RBNode successor = FindMax(node.Left);

                    // Скопируем значение наибольшего узла левого поддерева в удаляемый узел
                    node.Value = successor.Value;

                    // Удалим наибольший узел из левого поддерева
                    node.Left = Delete(node.Left, successor.Value);
                }
            }

            return node;
        }

        private RBNode delete_one_child(RBNode node)
        {
            RBNode child = node.Left != null ? node.Left : node.Right;

            if (node.NodeColor == Color.Black)
            {
                if (child != null && child.NodeColor == Color.Red)
                {
                    child.NodeColor = Color.Black;
                }
                else
                {
                    FixDoubleBlack(child);
                }
            }

            replace_node(node, child);
            return child;
        }

        private RBNode FindMax(RBNode node)
        {
            while (node.Right != null)
                node = node.Right;
            return node;
        }

        // ZAEBIS

        private RBNode FindMin(RBNode node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        private void replace_node(RBNode n, RBNode child)
        {
            if (child != null)
            {
                child.Parent = n.Parent;
            }

            if (n.Parent == null)
            {
                root = child;
            }
            else if (n == n.Parent.Left)
            {
                n.Parent.Left = child;
            }
            else
            {
                n.Parent.Right = child;
            }
        }

        private RBNode FixDoubleBlack(RBNode node)
        {
            if (node == root)
                return node;

            RBNode sibling = GetSibling(node);
            RBNode parent = null;

            if (node != null)
                parent = node.Parent;

            if (sibling == null)
            {
                if (parent != null)
                    FixDoubleBlack(parent);
            }
            else
            {
                if (sibling.NodeColor == Color.Red)
                {
                    if (parent != null)
                    {
                        parent.NodeColor = Color.Red;
                        sibling.NodeColor = Color.Black;
                        if (sibling == parent.Left)
                            RightRotate(parent);
                        else
                            LeftRotate(parent);
                        FixDoubleBlack(node);
                    }
                }
                else
                {
                    if (
                        (sibling.Left == null || sibling.Left.NodeColor == Color.Black) &&
                        (sibling.Right == null || sibling.Right.NodeColor == Color.Black)
                    )
                    {
                        sibling.NodeColor = Color.Red;
                        if (parent != null && parent.NodeColor == Color.Black)
                            FixDoubleBlack(parent);
                    }
                    else
                    {
                        if (
                            sibling == parent?.Left &&
                            (sibling.Right == null || sibling.Right.NodeColor == Color.Black) &&
                            (sibling.Left != null && sibling.Left.NodeColor == Color.Red)
                        )
                        {
                            sibling.NodeColor = Color.Red;
                            sibling.Left.NodeColor = Color.Black;
                            RightRotate(sibling);
                        }
                        else if (
                            sibling == parent?.Right &&
                            (sibling.Left == null || sibling.Left.NodeColor == Color.Black) &&
                            (sibling.Right != null && sibling.Right.NodeColor == Color.Red)
                        )
                        {
                            sibling.NodeColor = Color.Red;
                            sibling.Right.NodeColor = Color.Black;
                            LeftRotate(sibling);
                        }

                        sibling = GetSibling(node);
                        FixDoubleBlack(node);
                    }
                }
            }

            return node;
        }

        private RBNode GetSibling(RBNode node)
        {
            if (node == null || node.Parent == null)
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
