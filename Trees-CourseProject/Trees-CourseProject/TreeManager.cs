using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Diagnostics;

namespace Trees_CourseProject
{
    public enum TreeType { BinarySearchTree, AVLTree, RedBlackTree }
    public class TreeManager
    {
        public BinarySearchTree binarySearchTree;
        public AVLTree avlTree;
        public RedBlackTree redBlackTree;
        public TreeDrawer treeDrawer;
        public Canvas canvas;
        public Canvas BSTcanvas;
        public Canvas AVLcanvas;
        public Canvas RBcanvas;

        /*
        public TreeManager(Canvas canvas)
        {
            this.canvas = canvas;
            treeDrawer = new TreeDrawer();
        }
        */

        // Конструкторы с именованными параметрами для каждого Canvas
        public TreeManager(Canvas BSTcanvas = null, Canvas AVLcanvas = null, Canvas RBcanvas = null, TreeDrawer treeDrawer = null)
        {
            this.BSTcanvas = BSTcanvas;
            this.AVLcanvas = AVLcanvas;
            this.RBcanvas = RBcanvas;

            if (BSTcanvas != null)
            {
                binarySearchTree = new BinarySearchTree(BSTcanvas); // Создание экземпляра бинарного дерева поиска
                this.treeDrawer = treeDrawer; // Присваиваем переданный экземпляр TreeDrawer
            }

            if (AVLcanvas != null)
            {
                avlTree = new AVLTree(AVLcanvas); // Создание экземпляра АВЛ-дерева
                this.treeDrawer = treeDrawer; 
            }

            if (RBcanvas != null)
            {
                redBlackTree = new RedBlackTree(RBcanvas); // Создание экземпляра КЧ-дерева
                this.treeDrawer = treeDrawer; 
            }
        }


        public void Insert(int value, TreeType treeType)
        {
            switch (treeType)
            {
                case TreeType.BinarySearchTree:
                    if (binarySearchTree != null)
                    {
                        binarySearchTree.Insert(value);
                    }
                    break;
                case TreeType.AVLTree:
                    if (avlTree != null)
                    {
                        avlTree.Insert(value);
                    }
                    break;
                case TreeType.RedBlackTree:
                    if (redBlackTree != null)
                    {
                        redBlackTree.Insert(value);
                    }
                    break;
                default:
                    throw new ArgumentException("Неподдерживаемый тип дерева!");
            }
        }

        public void Delete(int value, TreeType treeType)
        {
            switch (treeType)
            {
                case TreeType.BinarySearchTree:
                    if (binarySearchTree != null)
                    {
                        binarySearchTree.Delete(value);
                    }
                    break;
                case TreeType.AVLTree:
                    if (avlTree != null)
                    {
                        avlTree.Delete(value);
                    }
                    break;
                case TreeType.RedBlackTree:
                    if (redBlackTree != null)
                    {
                        redBlackTree.Delete(value);
                    }
                    break;
                default:
                    throw new ArgumentException("Неподдерживаемый тип дерева!");
            }
        }

        public bool Search(int value, TreeType treeType)
        {
            switch (treeType)
            {
                case TreeType.BinarySearchTree:
                    return binarySearchTree != null && binarySearchTree.Search(value);
                case TreeType.AVLTree:
                    return avlTree != null && avlTree.Search(value);
                case TreeType.RedBlackTree:
                    return redBlackTree != null && redBlackTree.Search(value);
                default:
                    throw new ArgumentException("Неподдерживаемый тип дерева!");
            }
        }

    }
}
