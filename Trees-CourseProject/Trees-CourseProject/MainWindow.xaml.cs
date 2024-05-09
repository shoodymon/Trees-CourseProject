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

        // Методы вставки, удаления и балансировки дерева будут здесь
        public void Insert(int value)
        {
            root = Insert(root, null, value); // Начинаем с корня, у корня нет родителя
                                              // Корень всегда должен быть черным, чтобы удовлетворить свойства красно-черного дерева
            root.NodeColor = Color.Black;
            Console.WriteLine($"Inserted node with value {value}");
        }

        private RBNode Insert(RBNode node, RBNode parent, int value)
        {
            if (node == null)
            {
                node = new RBNode(value);
                node.Parent = parent;
                return node;
            }

            if (value < node.Value)
                node.Left = Insert(node.Left, node, value);
            else if (value > node.Value)
                node.Right = Insert(node.Right, node, value);
            else
                return node; // Значение уже существует в дереве, ничего не делаем

            // Проверяем и исправляем свойства красно-черного дерева
            if (node.Parent != null && node.Parent.NodeColor == Color.Red)
            {
                RBNode parentNode = node.Parent; // Изменено имя переменной
                RBNode grandparent = parentNode.Parent;
                RBNode uncle = grandparent.Left == parentNode ? grandparent.Right : grandparent.Left;

                // Случай 1: Дядя красный
                if (uncle != null && uncle.NodeColor == Color.Red)
                {
                    parentNode.NodeColor = Color.Black;
                    uncle.NodeColor = Color.Black;
                    grandparent.NodeColor = Color.Red;
                    return Insert(grandparent, grandparent.Parent, node.Value); // Продолжаем проверку свойств с дедушкой
                }

                // Случай 2: Дядя черный, текущий узел - правый потомок своего родителя
                if (parentNode.Right == node && grandparent.Left == parentNode)
                {
                    parentNode = LeftRotate(parentNode);
                    node = node.Left;
                }
                // Случай 3: Дядя черный, текущий узел - левый потомок своего родителя
                else if (parentNode.Left == node && grandparent.Right == parentNode)
                {
                    parentNode = RightRotate(parentNode);
                    node = node.Right;
                }

                // Случай 4: Дядя черный, текущий узел - левый потомок своего родителя
                parentNode.NodeColor = Color.Black;
                grandparent.NodeColor = Color.Red;
                if (parentNode.Left == node && grandparent.Left == parentNode)
                    grandparent = RightRotate(grandparent);
                else
                    grandparent = LeftRotate(grandparent);
            }


            return node;
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
                // Найден узел для удаления
                if (node.Left == null)
                    return node.Right;
                else if (node.Right == null)
                    return node.Left;

                // У узла есть оба потомка
                // Находим наименьший узел в правом поддереве (или наибольший в левом поддереве)
                RBNode minRight = FindMin(node.Right);
                // Копируем значение найденного узла в текущий узел
                node.Value = minRight.Value;
                // Рекурсивно удаляем найденный узел
                node.Right = Delete(node.Right, minRight.Value);
            }

            // Проверяем и исправляем свойства красно-черного дерева после удаления узла
            if (node.NodeColor == Color.Black)
            {
                if (node.Left != null && node.Left.NodeColor == Color.Red)
                    node.Left.NodeColor = Color.Black;
                else if (node.Right != null && node.Right.NodeColor == Color.Red)
                    node.Right.NodeColor = Color.Black;
                else
                    node = FixDoubleBlack(node);
            }

            return node;
        }

        // Вспомогательный метод для поиска наименьшего узла в дереве
        private RBNode FindMin(RBNode node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        // Вспомогательный метод для исправления двойной черной высоты узла
        private RBNode FixDoubleBlack(RBNode node)
        {
            if (node.Parent == null)
                return node;

            RBNode sibling = GetSibling(node);
            RBNode parent = node.Parent;

            if (sibling == null)
                return FixDoubleBlack(parent);

            if (sibling.NodeColor == Color.Black)
            {
                // Случай 1: Брат черный и у брата есть красный потомок
                if ((sibling.Left != null && sibling.Left.NodeColor == Color.Red) || (sibling.Right != null && sibling.Right.NodeColor == Color.Red))
                {
                    if (sibling.Left != null && sibling.Left.NodeColor == Color.Red)
                    {
                        if (parent.Left == sibling)
                        {
                            sibling.Left.NodeColor = sibling.NodeColor;
                            sibling.NodeColor = parent.NodeColor;
                            RightRotate(parent);
                        }
                        else
                        {
                            sibling.Left.NodeColor = parent.NodeColor;
                            RightRotate(sibling);
                            LeftRotate(parent);
                        }
                    }
                    else
                    {
                        if (parent.Left == sibling)
                        {
                            sibling.Right.NodeColor = parent.NodeColor;
                            LeftRotate(sibling);
                            RightRotate(parent);
                        }
                        else
                        {
                            sibling.Right.NodeColor = sibling.NodeColor;
                            sibling.NodeColor = parent.NodeColor;
                            LeftRotate(parent);
                        }
                    }
                    parent.NodeColor = Color.Black;
                }
                // Случай 2: Брат черный и у брата нет красного потомка
                else
                {
                    sibling.NodeColor = Color.Red;
                    if (parent.NodeColor == Color.Black)
                        return FixDoubleBlack(parent);
                    else
                        parent.NodeColor = Color.Black;
                }
            }
            // Случай 3: Брат красный
            else
            {
                if (parent.Left == sibling)
                    RightRotate(parent);
                else
                    LeftRotate(parent);
                parent.NodeColor = Color.Red;
                sibling.NodeColor = Color.Black;
                return FixDoubleBlack(node);
            }

            return node;
        }

        // Вспомогательный метод для получения брата узла
        private RBNode GetSibling(RBNode node)
        {
            if (node.Parent == null)
                return null;

            if (node == node.Parent.Left)
                return node.Parent.Right;
            else
                return node.Parent.Left;
        }

        private RBNode LeftRotate(RBNode node)
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
            return newRoot;
        }

        private RBNode RightRotate(RBNode node)
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
            return newRoot;
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
            treeManager = new TreeManager(canvas);
            treeDrawer = new TreeDrawer();

            binarySearchTreeManager = new TreeManager(BSTcanvas, null, null, binarySearchTreeDrawer);
            avlTreeManager = new TreeManager(null, AVLcanvas, null, avlTreeDrawer);
            redBlackTreeManager = new TreeManager(null, null, RBcanvas, redBlackTreeDrawer);

            // Создание экземпляров TreeDrawer для каждого типа дерева
            binarySearchTreeDrawer = new TreeDrawer();
            avlTreeDrawer = new TreeDrawer();
            redBlackTreeDrawer = new TreeDrawer();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = Brushes.LightGray;
            }
            else
            {
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TreeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvas.Children.Clear();

            ComboBox comboBox = (ComboBox)sender;
            TreeType selectedTree = (TreeType)comboBox.SelectedIndex;

            UpdateTreeDrawing(selectedTree);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nodeInputTextBox.Text))
            {
                int value = int.Parse(nodeInputTextBox.Text); // Извлечение значения из поля ввода
                TreeType selectedTree = (TreeType)TreeTypeComboBox.SelectedIndex;
                treeManager.Insert(value, selectedTree);
                UpdateTreeDrawing(selectedTree);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nodeInputTextBox.Text))
            {
                int value = int.Parse(nodeInputTextBox.Text);
                TreeType selectedTree = (TreeType)TreeTypeComboBox.SelectedIndex;
                treeManager.Delete(value, selectedTree);
                UpdateTreeDrawing(selectedTree);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nodeInputTextBox.Text))
            {
                int value = int.Parse(nodeInputTextBox.Text);
                TreeType selectedTree = (TreeType)TreeTypeComboBox.SelectedIndex;
                bool found = treeManager.Search(value, selectedTree);

                if (found)
                {
                    MessageBox.Show($"Узел со значением {value} найден в дереве.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Узел со значением {value} не найден в дереве.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void UpdateTreeDrawing(TreeType treeType)
        {
            switch (treeType)
            {
                case TreeType.BinarySearchTree:
                    if (MainTabControl.SelectedItem == BSTree)
                    {
                        treeDrawer.DrawBinarySearchTree(treeManager.binarySearchTree.root, BSTcanvas);
                    }
                    break;
                case TreeType.AVLTree:
                    if (MainTabControl.SelectedItem == AVLtree)
                    {
                        treeDrawer.DrawAVLTree(treeManager.avlTree.root, AVLcanvas);
                    }
                    break;
                case TreeType.RedBlackTree:
                    if (MainTabControl.SelectedItem == RBtree)
                    {
                        treeDrawer.DrawRedBlackTree(treeManager.redBlackTree.root, RBcanvas);
                    }
                    break;
            }
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
            // дернем отрисовку
        }

        private void AVLtreeButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = AVLtree;
            UpdateTreeDrawingTabItem(TreeType.AVLTree);
            //treeDrawer.DrawAVLTree(treeManager.avlTree.root, AVLcanvas);
        }

        private void RBtreeButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = RBtree;
            UpdateTreeDrawingTabItem(TreeType.RedBlackTree);
            //treeDrawer.DrawRedBlackTree(treeManager.redBlackTree.root, RBcanvas);
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = StartUpMenu;
        }

        private void BSTreeAdd_Click(object sender, RoutedEventArgs e)
        {
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
    }

}