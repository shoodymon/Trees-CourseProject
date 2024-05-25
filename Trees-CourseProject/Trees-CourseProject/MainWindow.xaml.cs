using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Trees_CourseProject.MainWindow;

namespace Trees_CourseProject
{
    public class BinarySearchTree
    {
        public class TreeNode
        {
            private int value;

            // Открыто доступное свойство Value, которое обеспечивает доступ к значению переменной value извне класса.
            public int Value
            {
                get { return value; }
                set
                {
                    if (value >= -99 && value <= 99)
                    {
                        this.value = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Value must be between -99 and 99");
                    }
                }
            }

            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            // Конструктор с одним аргументом для инициализации значения узла
            public TreeNode(int value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }

        public TreeNode root;
        private Canvas canvas;

        public BinarySearchTree(Canvas canvas)
        {
            root = null;
            this.canvas = canvas;
        }

        // Метод для добавления значения в дерево
        public void Insert(int value)
        {
            root = Insert(root, value);
            Console.WriteLine($"Inserted node with value {value}");
        }

        // Вспомогательный метод для рекурсивного добавления значения в дерево
        private TreeNode Insert(TreeNode node, int value)
        {
            if (node == null)
            {
                node = new TreeNode(value);
            }
            else if (value < node.Value)
            {
                node.Left = Insert(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Insert(node.Right, value);
            }
            // Если значение уже существует в дереве, ничего не делаем
            return node;
        }

        // Метод для удаления значения из дерева
        public void Delete(int value)
        {
            root = Delete(root, value);
            Console.WriteLine($"Deleted node with value {value}");
        }

        // Вспомогательный метод для рекурсивного удаления значения из дерева
        private TreeNode Delete(TreeNode node, int value)
        {
            if (node == null)
            {
                return null;
            }
            else if (value < node.Value)
            {
                node.Left = Delete(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Delete(node.Right, value);
            }
            else
            {
                // Узел найден, начинаем процесс удаления
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    // У узла есть оба потомка
                    // Находим наименьший узел в правом поддереве (или наибольший в левом поддереве)
                    TreeNode minRight = FindMin(node.Right);
                    // Копируем значение найденного узла в текущий узел
                    node.Value = minRight.Value;
                    // Рекурсивно удаляем найденный узел
                    node.Right = Delete(node.Right, minRight.Value);
                }
            }
            return node;
        }

        // Метод для поиска наименьшего значения в дереве
        private TreeNode FindMin(TreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        // Метод для поиска значения в дереве
        public bool Search(int value)
        {
            return Search(root, value);
        }

        // Вспомогательный метод для рекурсивного поиска значения в дереве
        private bool Search(TreeNode node, int value)
        {
            if (node == null)
            {
                return false;
            }
            else if (node.Value == value)
            {
                return true;
            }
            else if (value < node.Value)
            {
                return Search(node.Left, value);
            }
            else
            {
                return Search(node.Right, value);
            }
        }

        // Методы для обхода дерева в различных порядках (прямом, симметричном и обратном)
        public void PreOrderTraversal()
        {
            PreOrderTraversal(root);
        }

        private void PreOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                Console.Write(node.Value + " ");
                PreOrderTraversal(node.Left);
                PreOrderTraversal(node.Right);
            }
        }

        public void InOrderTraversal()
        {
            InOrderTraversal(root);
        }

        private void InOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Value + " ");
                InOrderTraversal(node.Right);
            }
        }

        public void PostOrderTraversal()
        {
            PostOrderTraversal(root);
        }

        private void PostOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                PostOrderTraversal(node.Left);
                PostOrderTraversal(node.Right);
                Console.Write(node.Value + " ");
            }
        }

    }

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

    //public class RedBlackTree
    //{
    //    public enum Color { Red, Black }

    //    public class RBNode
    //    {
    //        public int Value;
    //        public Color NodeColor;
    //        public RBNode Left;
    //        public RBNode Right;
    //        public RBNode Parent;

    //        public RBNode(int value)
    //        {
    //            Value = value;
    //            NodeColor = Color.Red;
    //            Left = null;
    //            Right = null;
    //            Parent = null;
    //        }
    //    }

    //    public RBNode root;
    //    private Canvas canvas;

    //    public RedBlackTree(Canvas canvas)
    //    {
    //        root = null;
    //        this.canvas = canvas;
    //    }

    //    // Методы вставки, удаления и балансировки дерева будут здесь
    //    public void Insert(int value)
    //    {
    //        root = Insert(root, null, value); // Начинаем с корня, у корня нет родителя
    //                                          // Корень всегда должен быть черным, чтобы удовлетворить свойства красно-черного дерева
    //        root.NodeColor = Color.Black;
    //        Console.WriteLine($"Inserted node with value {value}");
    //    }

    //    private RBNode Insert(RBNode node, RBNode parent, int value)
    //    {
    //        if (node == null)
    //        {
    //            node = new RBNode(value);
    //            node.Parent = parent;
    //            return node;
    //        }

    //        if (value < node.Value)
    //            node.Left = Insert(node.Left, node, value);
    //        else if (value > node.Value)
    //            node.Right = Insert(node.Right, node, value);
    //        else
    //            return node; // Значение уже существует в дереве, ничего не делаем

    //        // Проверяем и исправляем свойства красно-черного дерева
    //        if (node.Parent != null && node.Parent.NodeColor == Color.Red)
    //        {
    //            RBNode parentNode = node.Parent;
    //            RBNode grandparent = parentNode.Parent;
    //            RBNode uncle = null;

    //            if (grandparent != null)
    //            {
    //                uncle = grandparent.Left == parentNode ? grandparent.Right : grandparent.Left;

    //                // Случай 1: Дядя красный
    //                if (uncle != null && uncle.NodeColor == Color.Red)
    //                {
    //                    parentNode.NodeColor = Color.Black;
    //                    uncle.NodeColor = Color.Black;
    //                    grandparent.NodeColor = Color.Red;
    //                    return Insert(grandparent, grandparent.Parent, node.Value); // Продолжаем проверку свойств с дедушкой
    //                }

    //                // Случай 2: Дядя черный, текущий узел - правый потомок своего родителя
    //                if (parentNode.Right == node && grandparent.Left == parentNode)
    //                {
    //                    parentNode = LeftRotate(parentNode);
    //                    node = node.Left;
    //                }
    //                // Случай 3: Дядя черный, текущий узел - левый потомок своего родителя
    //                else if (parentNode.Left == node && grandparent.Right == parentNode)
    //                {
    //                    parentNode = RightRotate(parentNode);
    //                    node = node.Right;
    //                }

    //                // Случай 4: Дядя черный, текущий узел - левый потомок своего родителя
    //                parentNode.NodeColor = Color.Black;
    //                grandparent.NodeColor = Color.Red;
    //                if (parentNode.Left == node && grandparent.Left == parentNode)
    //                    grandparent = RightRotate(grandparent);
    //                else
    //                    grandparent = LeftRotate(grandparent);
    //            }
    //        }


    //        return node;
    //    }

    //    public bool Search(int value)
    //    {
    //        return Search(root, value);
    //    }

    //    private bool Search(RBNode node, int value)
    //    {
    //        if (node == null)
    //            return false;

    //        if (value == node.Value)
    //            return true;

    //        if (value < node.Value)
    //            return Search(node.Left, value);
    //        else
    //            return Search(node.Right, value);
    //    }

    //    public void Delete(int value)
    //    {
    //        root = Delete(root, value);
    //    }

    //    private RBNode Delete(RBNode node, int value)
    //    {
    //        if (node == null)
    //            return null;

    //        if (value < node.Value)
    //            node.Left = Delete(node.Left, value);
    //        else if (value > node.Value)
    //            node.Right = Delete(node.Right, value);
    //        else
    //        {
    //            // Найден узел для удаления
    //            if (node.Left == null)
    //                return node.Right;
    //            else if (node.Right == null)
    //                return node.Left;

    //            // У узла есть оба потомка
    //            // Находим наименьший узел в правом поддереве (или наибольший в левом поддереве)
    //            RBNode minRight = FindMin(node.Right);
    //            // Копируем значение найденного узла в текущий узел
    //            node.Value = minRight.Value;
    //            // Рекурсивно удаляем найденный узел
    //            node.Right = Delete(node.Right, minRight.Value);
    //        }

    //        // Проверяем и исправляем свойства красно-черного дерева после удаления узла
    //        if (node.NodeColor == Color.Black)
    //        {
    //            if (node.Left != null && node.Left.NodeColor == Color.Red)
    //                node.Left.NodeColor = Color.Black;
    //            else if (node.Right != null && node.Right.NodeColor == Color.Red)
    //                node.Right.NodeColor = Color.Black;
    //            else
    //                node = FixDoubleBlack(node);
    //        }

    //        return node;
    //    }

    //    // Вспомогательный метод для поиска наименьшего узла в дереве
    //    private RBNode FindMin(RBNode node)
    //    {
    //        while (node.Left != null)
    //            node = node.Left;
    //        return node;
    //    }

    //    // Вспомогательный метод для исправления двойной черной высоты узла
    //    private RBNode FixDoubleBlack(RBNode node)
    //    {
    //        if (node == root)
    //            return root;

    //        if (node.Parent == null)
    //            return node;

    //        RBNode originalNode = node; // Сохраняем исходное значение node

    //        RBNode sibling = GetSibling(node);
    //        RBNode parent = node.Parent;

    //        if (sibling == null)
    //            return FixDoubleBlack(parent);

    //        if (sibling.NodeColor == Color.Black)
    //        {
    //            // Случай 1: Брат черный и у брата есть красный потомок
    //            if ((sibling.Left != null && sibling.Left.NodeColor == Color.Red) || (sibling.Right != null && sibling.Right.NodeColor == Color.Red))
    //            {
    //                if (sibling.Left != null && sibling.Left.NodeColor == Color.Red)
    //                {
    //                    if (parent.Left == sibling)
    //                    {
    //                        sibling.Left.NodeColor = sibling.NodeColor;
    //                        sibling.NodeColor = parent.NodeColor;
    //                        RightRotate(parent);
    //                    }
    //                    else
    //                    {
    //                        sibling.Left.NodeColor = parent.NodeColor;
    //                        RightRotate(sibling);
    //                        LeftRotate(parent);
    //                    }
    //                }
    //                else
    //                {
    //                    if (parent.Left == sibling)
    //                    {
    //                        sibling.Right.NodeColor = parent.NodeColor;
    //                        LeftRotate(sibling);
    //                        RightRotate(parent);
    //                    }
    //                    else
    //                    {
    //                        sibling.Right.NodeColor = sibling.NodeColor;
    //                        sibling.NodeColor = parent.NodeColor;
    //                        LeftRotate(parent);
    //                    }
    //                }
    //                parent.NodeColor = Color.Black;
    //            }
    //            // Случай 2: Брат черный и у брата нет красного потомка
    //            else
    //            {
    //                sibling.NodeColor = Color.Red;
    //                if (parent.NodeColor == Color.Black)
    //                {
    //                    if (originalNode == node) // Проверка на изменение значения node
    //                        return node;
    //                    else
    //                        return FixDoubleBlack(parent);
    //                }
    //                else
    //                    parent.NodeColor = Color.Black;
    //            }
    //        }
    //        // Случай 3: Брат красный
    //        else
    //        {
    //            if (parent.Left == sibling)
    //                RightRotate(parent);
    //            else
    //                LeftRotate(parent);
    //            parent.NodeColor = Color.Red;
    //            sibling.NodeColor = Color.Black;

    //            if (originalNode == node) // Проверка на изменение значения node
    //                return node;
    //            else
    //                return FixDoubleBlack(node);
    //        }

    //        return node;
    //    }

    //    // Вспомогательный метод для получения брата узла
    //    private RBNode GetSibling(RBNode node)
    //    {
    //        if (node.Parent == null)
    //            return null;

    //        if (node == node.Parent.Left)
    //            return node.Parent.Right;
    //        else
    //            return node.Parent.Left;
    //    }

    //    private RBNode LeftRotate(RBNode node)
    //    {
    //        RBNode newRoot = node.Right;
    //        node.Right = newRoot.Left;
    //        if (newRoot.Left != null)
    //            newRoot.Left.Parent = node;
    //        newRoot.Parent = node.Parent;
    //        if (node.Parent == null)
    //            root = newRoot;
    //        else if (node == node.Parent.Left)
    //            node.Parent.Left = newRoot;
    //        else
    //            node.Parent.Right = newRoot;
    //        newRoot.Left = node;
    //        node.Parent = newRoot;
    //        return newRoot;
    //    }

    //    private RBNode RightRotate(RBNode node)
    //    {
    //        RBNode newRoot = node.Left;
    //        node.Left = newRoot.Right;
    //        if (newRoot.Right != null)
    //            newRoot.Right.Parent = node;
    //        newRoot.Parent = node.Parent;
    //        if (node.Parent == null)
    //            root = newRoot;
    //        else if (node == node.Parent.Right)
    //            node.Parent.Right = newRoot;
    //        else
    //            node.Parent.Left = newRoot;
    //        newRoot.Right = node;
    //        node.Parent = newRoot;
    //        return newRoot;
    //    }

    //}

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

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TreeManager treeManager;
        private TreeDrawer treeDrawer;

        private TreeManager binarySearchTreeManager;
        private TreeManager avlTreeManager;
        private TreeManager redBlackTreeManager;

        private TreeDrawer binarySearchTreeDrawer;
        private TreeDrawer avlTreeDrawer;
        private TreeDrawer redBlackTreeDrawer;

        public MainWindow()
        {
            InitializeComponent();
            treeManager = new TreeManager();
            treeDrawer = new TreeDrawer();

            binarySearchTreeManager = new TreeManager(BSTcanvas, null, null, binarySearchTreeDrawer);
            avlTreeManager = new TreeManager(null, AVLcanvas, null, avlTreeDrawer);
            redBlackTreeManager = new TreeManager(null, null, RBcanvas, redBlackTreeDrawer);

            // Создание экземпляров TreeDrawer для каждого типа дерева
            binarySearchTreeDrawer = new TreeDrawer();
            avlTreeDrawer = new TreeDrawer();
            redBlackTreeDrawer = new TreeDrawer();

            List<string> styles = new List<string> { "ClassicTheme", "DarkTheme", "CustomTheme" };
            PresetTheme.SelectionChanged += ThemeChange;
            PresetTheme.Items.Clear();
            PresetTheme.ItemsSource = styles;
            PresetTheme.SelectedItem = "ClassicTheme";
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = PresetTheme.SelectedItem as string;
            // определяем путь к файлу ресурсов
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void UpdateTreeDrawingTabItem(TreeType treeType)
        {
            switch (treeType)
            {
                case TreeType.BinarySearchTree:
                    if (MainTabControl.SelectedItem == BSTree && binarySearchTreeManager.binarySearchTree != null)
                    {
                        binarySearchTreeDrawer.DrawBinarySearchTree(binarySearchTreeManager.binarySearchTree.root, BSTcanvas);
                    }
                    break;
                case TreeType.AVLTree:
                    if (MainTabControl.SelectedItem == AVLtree && avlTreeManager.avlTree != null)
                    {
                        avlTreeDrawer.DrawAVLTree(avlTreeManager.avlTree.root, AVLcanvas);
                    }
                    break;
                case TreeType.RedBlackTree:
                    if (MainTabControl.SelectedItem == RBtree && redBlackTreeManager.redBlackTree != null)
                    {
                        redBlackTreeDrawer.DrawRedBlackTree(redBlackTreeManager.redBlackTree.root, RBcanvas);
                    }
                    break;
            }
        }

        private void TreesButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl mainTabControl = this.MainTabControl;
            if (mainTabControl != null)
            {
                mainTabControl.SelectedItem = mainTabControl.Items.OfType<TabItem>().FirstOrDefault(t => t.Name == "TreeSelectionMenu");
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl mainTabControl = this.MainTabControl;
            if (mainTabControl != null)
            {
                mainTabControl.SelectedItem = mainTabControl.Items.OfType<TabItem>().FirstOrDefault(t => t.Name == "Settings");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BSTreeButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = BSTree;
            UpdateTreeDrawingTabItem(TreeType.BinarySearchTree);
        }

        private void AVLtreeButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = AVLtree;
            UpdateTreeDrawingTabItem(TreeType.AVLTree);
        }

        private void RBtreeButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = RBtree;
            UpdateTreeDrawingTabItem(TreeType.RedBlackTree);
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = StartUpMenu;
        }

        private void BSTreeAdd_Click(object sender, RoutedEventArgs e)
        {
            string newNodeValue = BSTnodeInputTextBox.Text;
            UserBSTNodesTextBlock.Text += newNodeValue + " ";

            if (!string.IsNullOrEmpty(BSTnodeInputTextBox.Text))
            {
                int value = int.Parse(BSTnodeInputTextBox.Text);
                binarySearchTreeManager.Insert(value, TreeType.BinarySearchTree);
                UpdateTreeDrawingTabItem(TreeType.BinarySearchTree);
            }
        }

        private void BSTreeDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BSTnodeInputTextBox.Text))
            {
                int value = int.Parse(BSTnodeInputTextBox.Text);
                binarySearchTreeManager.Delete(value, TreeType.BinarySearchTree);
                UpdateTreeDrawingTabItem(TreeType.BinarySearchTree);
            }
        }

        private void BSTreeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BSTnodeInputTextBox.Text))
            {
                int value = int.Parse(BSTnodeInputTextBox.Text);
                bool found = binarySearchTreeManager.Search(value, TreeType.BinarySearchTree);

                if (found)
                {
                    MessageBox.Show($"Узел со значением {value} найден в бинарном дереве поиска.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Узел со значением {value} не найден в бинарном дереве поиска.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        
        private void RBreeAdd_Click(object sender, RoutedEventArgs e)
        {
            string newNodeValue = RBnodeInputTextBox.Text;
            UserRBNodesTextBlock.Text += newNodeValue + " ";
            if (!string.IsNullOrEmpty(RBnodeInputTextBox.Text))
            {
                int value = int.Parse(RBnodeInputTextBox.Text);
                redBlackTreeManager.Insert(value, TreeType.RedBlackTree);
                UpdateTreeDrawingTabItem(TreeType.RedBlackTree);
            }
        }

        private void RBreeDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RBnodeInputTextBox.Text))
            {
                int value = int.Parse(RBnodeInputTextBox.Text);
                redBlackTreeManager.Delete(value, TreeType.RedBlackTree);
                UpdateTreeDrawingTabItem(TreeType.RedBlackTree);
            }
        }

        private void RBtreeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RBnodeInputTextBox.Text))
            {
                int value = int.Parse(RBnodeInputTextBox.Text);
                bool found = redBlackTreeManager.Search(value, TreeType.RedBlackTree);

                if (found)
                {
                    MessageBox.Show($"Узел со значением {value} найден в КЧ-дереве.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Узел со значением {value} не найден в КЧ-дереве.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void AVLtreeAdd_Click(object sender, RoutedEventArgs e)
        {
            string newNodeValue = AVLnodeInputTextBox.Text;
            UserAVLNodesTextBlock.Text += newNodeValue + " ";
            if (!string.IsNullOrEmpty(AVLnodeInputTextBox.Text))
            {
                int value = int.Parse(AVLnodeInputTextBox.Text);
                avlTreeManager.Insert(value, TreeType.AVLTree);
                UpdateTreeDrawingTabItem(TreeType.AVLTree);
            }
        }

        private void AVLtreeDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AVLnodeInputTextBox.Text))
            {
                int value = int.Parse(AVLnodeInputTextBox.Text);
                avlTreeManager.Delete(value, TreeType.AVLTree);
                UpdateTreeDrawingTabItem(TreeType.AVLTree);
            }
        }

        private void AVLtreeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AVLnodeInputTextBox.Text))
            {
                int value = int.Parse(AVLnodeInputTextBox.Text);
                bool found = avlTreeManager.Search(value, TreeType.AVLTree);

                if (found)
                {
                    MessageBox.Show($"Узел со значением {value} найден в AVL-дереве.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Узел со значением {value} не найден в AVL-дереве.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BackBSTtoTreeMenu_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = TreeSelectionMenu;
        }

        private void BackRBTtoTreeMenu_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = TreeSelectionMenu;
        }

        private void BackAVLTtoTreeMenu_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = TreeSelectionMenu;
        }

        private void PresetTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresetTheme.SelectedIndex == 0)
            {
                SetClassicTheme();
                UpdateTreeDrawingTabItem(TreeType.BinarySearchTree);
            }
            else if (PresetTheme.SelectedIndex == 1)
            {
                SetDarkTheme();
                UpdateTreeDrawingTabItem(TreeType.AVLTree);
            }
            else if (PresetTheme.SelectedIndex == 2)
            {
                SetCustomTheme();
                UpdateTreeDrawingTabItem(TreeType.RedBlackTree);
            }
        }

        private void SetClassicTheme()
        {
            // Изменить цвет узлов дерева
            BSTcanvas.Resources["BSTNodeColorBrush"] = (SolidColorBrush)Application.Current.Resources["BSTNodeColorBrush"];
            RBcanvas.Resources["RBNodeRedColorBrush"] = (SolidColorBrush)Application.Current.Resources["RBNodeRedColorBrush"];
            RBcanvas.Resources["RBNodeBlackColorBrush"] = (SolidColorBrush)Application.Current.Resources["RBNodeBlackColorBrush"];
            AVLcanvas.Resources["AVLNodeColorBrush"] = (SolidColorBrush)Application.Current.Resources["AVLNodeColorBrush"];

            // Изменить цвет стрелок
            BSTcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];
            RBcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];
            AVLcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];

            // Изменить цвет эллипсов
            BSTcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];
            RBcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];
            AVLcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];

            // Изменяем фон
            BSTcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
            RBcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
            AVLcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
        }

        private void SetDarkTheme()
        {
            // Изменить цвет узлов дерева
            BSTcanvas.Resources["BSTNodeColorBrush"] = (SolidColorBrush)Application.Current.Resources["BSTNodeColorBrush"];
            RBcanvas.Resources["RBNodeRedColorBrush"] = (SolidColorBrush)Application.Current.Resources["RBNodeRedColorBrush"];
            RBcanvas.Resources["RBNodeBlackColorBrush"] = (SolidColorBrush)Application.Current.Resources["RBNodeBlackColorBrush"];
            AVLcanvas.Resources["AVLNodeColorBrush"] = (SolidColorBrush)Application.Current.Resources["AVLNodeColorBrush"];

            // Изменить цвет стрелок
            BSTcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];
            RBcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];
            AVLcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];

            // Изменить цвет эллипсов
            BSTcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];
            RBcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];
            AVLcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];

            // Изменяем фон
            BSTcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
            RBcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
            AVLcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
        }

        private void SetCustomTheme()
        {
            // Изменить цвет узлов дерева
            BSTcanvas.Resources["BSTNodeColorBrush"] = (SolidColorBrush)Application.Current.Resources["BSTNodeColorBrush"];
            RBcanvas.Resources["RBNodeRedColorBrush"] = (SolidColorBrush)Application.Current.Resources["RBNodeRedColorBrush"];
            RBcanvas.Resources["RBNodeBlackColorBrush"] = (SolidColorBrush)Application.Current.Resources["RBNodeBlackColorBrush"];
            AVLcanvas.Resources["AVLNodeColorBrush"] = (SolidColorBrush)Application.Current.Resources["AVLNodeColorBrush"];

            // Изменить цвет стрелок
            BSTcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];
            RBcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];
            AVLcanvas.Resources["ArrowColorBrush"] = (SolidColorBrush)Application.Current.Resources["ArrowColorBrush"];

            // Изменить цвет эллипсов
            BSTcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];
            RBcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];
            AVLcanvas.Resources["EllipseColorBrush"] = (SolidColorBrush)Application.Current.Resources["EllipseColorBrush"];

            // Изменяем фон
            BSTcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
            RBcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
            AVLcanvas.Resources["CanvasBackgroundBrush"] = (SolidColorBrush)Application.Current.Resources["CanvasBackgroundBrush"];
        }

    }

}