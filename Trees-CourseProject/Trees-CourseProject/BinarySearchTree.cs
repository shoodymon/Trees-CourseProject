using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
    }
}
