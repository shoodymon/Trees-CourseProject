using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Trees_CourseProject
{
    public class TreeDrawer
    {
        private Canvas canvas;

        public TreeDrawer(Canvas canvas)
        {
            this.canvas = canvas;
        }

        private void DrawNode(int nodeValue, double x, double y, Brush color)
        {
            // Отрисовываем узел
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 35;
            ellipse.Height = 35;
            ellipse.Stroke = Brushes.Black;
            ellipse.Fill = color;
            Canvas.SetLeft(ellipse, x - ellipse.Width / 2);
            Canvas.SetTop(ellipse, y);

            // Отрисовываем значение узла
            Border border = new Border();
            border.Child = new TextBlock
            {
                Text = nodeValue.ToString(),
                FontWeight = FontWeights.Bold,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                FontSize = 16
            };
            Canvas.SetLeft(border, x - border.ActualWidth / 2 - 4.5);
            Canvas.SetTop(border, y + ellipse.Height / 2 - border.ActualHeight / 2 - 10);

            // Добавляем узел на холст
            canvas.Children.Add(ellipse);
            canvas.Children.Add(border);
        }

        private void DrawArrow(double x1, double y1, double x2, double y2, double arrowLength)
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            canvas.Children.Add(line);

            // Вычисляем угол наклона линии
            double theta = Math.Atan2(y2 - y1, x2 - x1);
            double phi = Math.PI / 6;

            // Отрисовываем треугольник стрелки
            Polygon polygon = new Polygon();
            polygon.Fill = Brushes.Black;
            PointCollection points = new PointCollection();
            points.Add(new System.Windows.Point(x2, y2));
            points.Add(new System.Windows.Point(x2 - arrowLength * Math.Cos(theta - phi), y2 - arrowLength * Math.Sin(theta - phi)));
            points.Add(new System.Windows.Point(x2 - arrowLength * Math.Cos(theta + phi), y2 - arrowLength * Math.Sin(theta + phi)));
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }

        public void DrawBinarySearchTree(BinarySearchTree.TreeNode root)
        {
            canvas.Children.Clear(); // Очищаем холст перед отрисовкой нового дерева
            if (root != null)
            {
                DrawBST(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80);
            }
        }

        public void DrawAVLTree(AVLTree.AVLNode root)
        {
            canvas.Children.Clear();
            if (root != null)
            {
                DrawAVL(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80);
            }
        }

        public void DrawRedBlackTree(RedBlackTree.RBNode root)
        {
            canvas.Children.Clear();
            if (root != null)
            {
                DrawRB(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80);
            }
        }

        private void DrawBST(BinarySearchTree.TreeNode node, double x, double y, double offsetX, double offsetY)
        {
            DrawNode(node.Value, x, y, Brushes.LightGreen);
            if (node.Left != null)
            {
                DrawBST(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY);
                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10);
            }
            if (node.Right != null)
            {
                DrawBST(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY);
                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10);
            }
        }

        private void DrawAVL(AVLTree.AVLNode node, double x, double y, double offsetX, double offsetY)
        {
            DrawNode(node.Value, x, y, Brushes.LightBlue);
            if (node.Left != null)
            {
                DrawAVL(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY);
                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10);
            }
            if (node.Right != null)
            {
                DrawAVL(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY);
                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10);
            }
        }

        private void DrawRB(RedBlackTree.RBNode node, double x, double y, double offsetX, double offsetY)
        {
            DrawNode(node.Value, x, y, node.NodeColor == RedBlackTree.Color.Black ? Brushes.LightGray : Brushes.Red);
            if (node.Left != null)
            {
                DrawRB(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY);
                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10);
            }
            if (node.Right != null)
            {
                DrawRB(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY);
                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10);
            }
        }

    }

}
