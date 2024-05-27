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