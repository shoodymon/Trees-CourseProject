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

        // ref, out, in используются для входных и выходных параметров при передаче по ссылке
        private bool TryGetValidatedInput(string input, out int value)
        {
            value = 0;

            // Проверка на пустое значение
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Введите значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Проверка, является ли введенное значение числом
            if (!int.TryParse(input, out value))
            {
                MessageBox.Show("Введите числовое значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Проверка, что значение в диапазоне от -99 до 99
            if (value < -99 || value > 99)
            {
                MessageBox.Show("Введите значение в диапазоне от -99 до 99.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void BSTreeAdd_Click(object sender, RoutedEventArgs e)
        {
            string newNodeValue = BSTnodeInputTextBox.Text;

            if (TryGetValidatedInput(newNodeValue, out int value))
            {
                UserBSTNodesTextBlock.Text += value + " ";
                binarySearchTreeManager.Insert(value, TreeType.BinarySearchTree);
                UpdateTreeDrawingTabItem(TreeType.BinarySearchTree);
            }
        }

        private void BSTreeDelete_Click(object sender, RoutedEventArgs e)
        {
            string newNodeValue = BSTnodeInputTextBox.Text;

            if (TryGetValidatedInput(newNodeValue, out int value))
            {
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

            if (TryGetValidatedInput(newNodeValue, out int value))
            {
                UserRBNodesTextBlock.Text += value + " ";
                redBlackTreeManager.Insert(value, TreeType.RedBlackTree);
                UpdateTreeDrawingTabItem(TreeType.RedBlackTree);
            }
        }

        private void RBreeDelete_Click(object sender, RoutedEventArgs e)
        {
            string newNodeValue = RBnodeInputTextBox.Text;

            if (TryGetValidatedInput(newNodeValue, out int value))
            {
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

            if (TryGetValidatedInput(newNodeValue, out int value))
            {
                UserAVLNodesTextBlock.Text += value + " ";
                avlTreeManager.Insert(value, TreeType.AVLTree);
                UpdateTreeDrawingTabItem(TreeType.AVLTree);
            }
        }

        private void AVLtreeDelete_Click(object sender, RoutedEventArgs e)
        {
            string newNodeValue = AVLnodeInputTextBox.Text;

            if (TryGetValidatedInput(newNodeValue, out int value))
            {
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