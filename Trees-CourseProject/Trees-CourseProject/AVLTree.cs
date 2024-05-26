using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Trees_CourseProject
{

    public class AVLTree
    {
        public class AVLNode
        {
            public int Value;
            public int Height;
            public AVLNode Left;
            public AVLNode Right;

            public AVLNode(int value)
            {
                Value = value;
                Height = 1;
                Left = null;
                Right = null;
            }
        }

        public AVLNode root;
        private Canvas canvas;

        public AVLTree(Canvas canvas)
        {
            root = null;
            this.canvas = canvas;
        }

        private int Height(AVLNode node)
        {
            return node == null ? 0 : node.Height;
        }

        private int BalanceFactor(AVLNode node)
        {
            return Height(node.Left) - Height(node.Right);
        }

        private void UpdateHeight(AVLNode node)
        {
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private AVLNode RightRotate(AVLNode pivot)
        {
            AVLNode newRoot = pivot.Left;
            pivot.Left = newRoot.Right;
            newRoot.Right = pivot;
            UpdateHeight(pivot);
            UpdateHeight(newRoot);
            return newRoot;
        }

        private AVLNode LeftRotate(AVLNode newRoot)
        {
            AVLNode pivot = newRoot.Right;
            newRoot.Right = pivot.Left;
            pivot.Left = newRoot;
            UpdateHeight(newRoot);
            UpdateHeight(pivot);
            return pivot;
        }

        private AVLNode Insert(AVLNode node, int value)
        {
            if (node == null)
                return new AVLNode(value);

            if (value < node.Value)
                node.Left = Insert(node.Left, value);
            else if (value > node.Value)
                node.Right = Insert(node.Right, value);
            else
                return node; // Значение уже существует в дереве, ничего не делаем

            UpdateHeight(node);

            int balance = BalanceFactor(node);

            // Перебалансировка дерева, если необходимо
            if (balance > 1)
            {
                // Вставленное значение меньше значения в левом поддереве
                if (value < node.Left.Value)
                    return RightRotate(node);
                // Вставленное значение больше значения в левом поддереве
                if (value > node.Left.Value)
                {
                    node.Left = LeftRotate(node.Left);
                    return RightRotate(node);
                }
            }
            else if (balance < -1)
            {
                // Вставленное значение больше значения в правом поддереве
                if (value > node.Right.Value)
                    return LeftRotate(node);
                // Вставленное значение меньше значения в правом поддереве
                if (value < node.Right.Value)
                {
                    node.Right = RightRotate(node.Right);
                    return LeftRotate(node);
                }
            }

            return node;
        }

        public void Insert(int value)
        {
            root = Insert(root, value);
            Console.WriteLine($"Inserted node with value {value}");
        }

        private AVLNode Delete(AVLNode root, int key)
        {
            if (root == null)
                return root;

            if (key < root.Value)
                root.Left = Delete(root.Left, key);
            else if (key > root.Value)
                root.Right = Delete(root.Right, key);
            else
            {
                if (root.Left == null || root.Right == null)
                {
                    AVLNode temp = root.Left != null ? root.Left : root.Right;

                    // Узел с одним или без детей
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else
                        root = temp;
                }
                else
                {
                    // Узел с двумя детьми: получаем наименьший узел в правом поддереве
                    AVLNode temp = MinValueNode(root.Right);

                    // Копируем данные наименьшего узла в текущий узел
                    root.Value = temp.Value;

                    // Удаляем наименьший узел в правом поддереве
                    root.Right = Delete(root.Right, temp.Value);
                }
            }

            // Если дерево состоит только из одного узла, возвращаем его
            if (root == null)
                return root;

            // Обновляем высоту текущего узла
            UpdateHeight(root);

            // Получаем коэффициент балансировки текущего узла
            int balance = BalanceFactor(root);

            // Перебалансировка дерева, если необходимо
            if (balance > 1 && BalanceFactor(root.Left) >= 0)
                return RightRotate(root);

            if (balance < -1 && BalanceFactor(root.Right) <= 0)
                return LeftRotate(root);

            if (balance > 1 && BalanceFactor(root.Left) < 0)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }

            if (balance < -1 && BalanceFactor(root.Right) > 0)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }

            return root;
        }

        private AVLNode MinValueNode(AVLNode node)
        {
            AVLNode current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        public void Delete(int key)
        {
            root = Delete(root, key);
        }

        public bool Search(int key)
        {
            return Search(root, key);
        }

        private bool Search(AVLNode node, int key)
        {
            if (node == null)
                return false;
            if (key == node.Value)
                return true;
            if (key < node.Value)
                return Search(node.Left, key);
            else
                return Search(node.Right, key);
        }

    }

}
