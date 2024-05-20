//using System;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Shapes;

//namespace Trees_CourseProject
//{
//    public class TreeDrawer
//    {
//        public TreeDrawer()
//        {
//            // Конструктор по умолчанию
//        }

//        private void DrawNode(int nodeValue, double x, double y, Brush color, Canvas canvas)
//        {
//            // Отрисовываем узел
//            Ellipse ellipse = new Ellipse();
//            ellipse.Width = 35;
//            ellipse.Height = 35;
//            ellipse.Stroke = Brushes.Black;
//            ellipse.Fill = color;
//            Canvas.SetLeft(ellipse, x - ellipse.Width / 2);
//            Canvas.SetTop(ellipse, y);

//            // Отрисовываем значение узла
//            Border border = new Border();
//            border.Child = new TextBlock
//            {
//                Text = nodeValue.ToString(),
//                FontWeight = FontWeights.Bold,
//                VerticalAlignment = System.Windows.VerticalAlignment.Center,
//                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
//                FontSize = 16
//            };
//            Canvas.SetLeft(border, x - border.ActualWidth / 2 - 4.5);
//            Canvas.SetTop(border, y + ellipse.Height / 2 - border.ActualHeight / 2 - 10);

//            // Добавляем узел на холст
//            canvas.Children.Add(ellipse);
//            canvas.Children.Add(border);
//        }

//        private void DrawArrow(double x1, double y1, double x2, double y2, double arrowLength, Canvas canvas)
//        {
//            Line line = new Line();
//            line.Stroke = Brushes.Black;
//            line.X1 = x1;
//            line.Y1 = y1;
//            line.X2 = x2;
//            line.Y2 = y2;
//            canvas.Children.Add(line);

//            // Вычисляем угол наклона линии
//            double theta = Math.Atan2(y2 - y1, x2 - x1);
//            double phi = Math.PI / 6;

//            // Отрисовываем треугольник стрелки
//            Polygon polygon = new Polygon();
//            polygon.Fill = Brushes.Black;
//            PointCollection points = new PointCollection();
//            points.Add(new System.Windows.Point(x2, y2));
//            points.Add(new System.Windows.Point(x2 - arrowLength * Math.Cos(theta - phi), y2 - arrowLength * Math.Sin(theta - phi)));
//            points.Add(new System.Windows.Point(x2 - arrowLength * Math.Cos(theta + phi), y2 - arrowLength * Math.Sin(theta + phi)));
//            polygon.Points = points;
//            canvas.Children.Add(polygon);
//        }

//        public void DrawBinarySearchTree(BinarySearchTree.TreeNode root, Canvas canvas)
//        {
//            canvas.Children.Clear(); // Очищаем холст перед отрисовкой нового дерева
//            if (root != null)
//            {
//                DrawBST(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80, canvas);
//            }
//        }

//        public void DrawAVLTree(AVLTree.AVLNode root, Canvas canvas)
//        {
//            canvas.Children.Clear();
//            if (root != null)
//            {
//                DrawAVL(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80, canvas);
//            }
//        }

//        public void DrawRedBlackTree(RedBlackTree.RBNode root, Canvas canvas)
//        {
//            canvas.Children.Clear();
//            if (root != null)
//            {
//                DrawRB(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80, canvas);
//            }
//        }

//        private void DrawBST(BinarySearchTree.TreeNode node, double x, double y, double offsetX, double offsetY, Canvas canvas)
//        {
//            DrawNode(node.Value, x, y, Brushes.LightGreen, canvas);
//            if (node.Left != null)
//            {
//                DrawBST(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
//                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10, canvas);
//            }
//            if (node.Right != null)
//            {
//                DrawBST(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
//                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10, canvas);
//            }
//        }

//        private void DrawAVL(AVLTree.AVLNode node, double x, double y, double offsetX, double offsetY, Canvas canvas)
//        {
//            DrawNode(node.Value, x, y, Brushes.LightBlue, canvas);
//            if (node.Left != null)
//            {
//                DrawAVL(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
//                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10, canvas);
//            }
//            if (node.Right != null)
//            {
//                DrawAVL(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
//                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10, canvas);
//            }
//        }

//        private void DrawRB(RedBlackTree.RBNode node, double x, double y, double offsetX, double offsetY, Canvas canvas)
//        {
//            DrawNode(node.Value, x, y, node.NodeColor == RedBlackTree.Color.Black ? Brushes.LightGray : Brushes.Red, canvas);
//            if (node.Left != null)
//            {
//                DrawRB(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
//                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10, canvas);
//            }
//            if (node.Right != null)
//            {
//                DrawRB(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
//                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10, canvas);
//            }
//        }
//    }
//}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Trees_CourseProject
{
    public class TreeDrawer
    {
        private void DrawNode(int nodeValue, double x, double y, Brush brush, Canvas canvas)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = 35,
                Height = 35,
                Stroke = (Brush)Application.Current.Resources["EllipseColorBrush"],
                Fill = brush
            };
            Canvas.SetLeft(ellipse, x - ellipse.Width / 2);
            Canvas.SetTop(ellipse, y);

            Border border = new Border
            {
                Child = new TextBlock
                {
                    Text = nodeValue.ToString(),
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 16
                }
            };
            Canvas.SetLeft(border, x - border.ActualWidth / 2 - 4.5);
            Canvas.SetTop(border, y + ellipse.Height / 2 - border.ActualHeight / 2 - 10);

            canvas.Children.Add(ellipse);
            canvas.Children.Add(border);
        }

        private void DrawArrow(double x1, double y1, double x2, double y2, double arrowLength, Canvas canvas)
        {
            Line line = new Line
            {
                Stroke = (Brush)Application.Current.Resources["ArrowColorBrush"],
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            };
            canvas.Children.Add(line);

            double theta = Math.Atan2(y2 - y1, x2 - x1);
            double phi = Math.PI / 6;

            Polygon polygon = new Polygon
            {
                Fill = (Brush)Application.Current.Resources["ArrowColorBrush"],
                Points = new PointCollection
                {
                    new Point(x2, y2),
                    new Point(x2 - arrowLength * Math.Cos(theta - phi), y2 - arrowLength * Math.Sin(theta - phi)),
                    new Point(x2 - arrowLength * Math.Cos(theta + phi), y2 - arrowLength * Math.Sin(theta + phi))
                }
            };
            canvas.Children.Add(polygon);
        }

        public void DrawBinarySearchTree(BinarySearchTree.TreeNode root, Canvas canvas)
        {
            canvas.Children.Clear();
            if (root != null)
            {
                DrawBST(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80, canvas);
            }
        }

        public void DrawAVLTree(AVLTree.AVLNode root, Canvas canvas)
        {
            canvas.Children.Clear();
            if (root != null)
            {
                DrawAVL(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80, canvas);
            }
        }

        public void DrawRedBlackTree(RedBlackTree.RBNode root, Canvas canvas)
        {
            canvas.Children.Clear();
            if (root != null)
            {
                DrawRB(root, canvas.ActualWidth / 2, 20, canvas.ActualWidth / 4, 80, canvas);
            }
        }

        private void DrawBST(BinarySearchTree.TreeNode node, double x, double y, double offsetX, double offsetY, Canvas canvas)
        {
            DrawNode(node.Value, x, y, (Brush)Application.Current.Resources["BSTNodeColorBrush"], canvas);
            if (node.Left != null)
            {
                DrawBST(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10, canvas);
            }
            if (node.Right != null)
            {
                DrawBST(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10, canvas);
            }
        }

        private void DrawAVL(AVLTree.AVLNode node, double x, double y, double offsetX, double offsetY, Canvas canvas)
        {
            DrawNode(node.Value, x, y, (Brush)Application.Current.Resources["AVLNodeColorBrush"], canvas);
            if (node.Left != null)
            {
                DrawAVL(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10, canvas);
            }
            if (node.Right != null)
            {
                DrawAVL(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10, canvas);
            }
        }

        private void DrawRB(RedBlackTree.RBNode node, double x, double y, double offsetX, double offsetY, Canvas canvas)
        {
            Brush nodeBrush;
            if (node.NodeColor == RedBlackTree.Color.Black)
            {
                nodeBrush = (Brush)Application.Current.Resources["RBNodeBlackColorBrush"];
            }
            else
            {
                nodeBrush = (Brush)Application.Current.Resources["RBNodeRedColorBrush"];
            }

            DrawNode(node.Value, x, y, nodeBrush, canvas);

            if (node.Left != null)
            {
                DrawRB(node.Left, x - offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
                DrawArrow(x, y + 35, x - offsetX, y + offsetY, 10, canvas);
            }

            if (node.Right != null)
            {
                DrawRB(node.Right, x + offsetX, y + offsetY, offsetX / 2, offsetY, canvas);
                DrawArrow(x, y + 35, x + offsetX, y + offsetY, 10, canvas);
            }
        }
    }
}