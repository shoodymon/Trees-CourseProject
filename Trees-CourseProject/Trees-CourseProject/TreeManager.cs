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

        public TreeManager(Canvas canvas)
        { 
            this.canvas = canvas;
            binarySearchTree = new BinarySearchTree(canvas);
            avlTree = new AVLTree(canvas);
            redBlackTree = new RedBlackTree(canvas);
            treeDrawer = new TreeDrawer(canvas);
        }

        public void Insert(int value, TreeType treeType) 
        {
            switch (treeType)
            {
                case TreeType.BinarySearchTree: binarySearchTree.Insert(value); break;
                case TreeType.AVLTree:          avlTree.Insert(value);          break;
                case TreeType.RedBlackTree:     redBlackTree.Insert(value);     break; 
                default: throw new ArgumentException("Неподдерживаемый тип дерева!");
            }
        }

        public void Delete(int value, TreeType treeType) 
        {
            switch (treeType) 
            {
                case TreeType.BinarySearchTree: binarySearchTree.Delete(value); break;
                case TreeType.AVLTree:          avlTree.Delete(value);          break;
                case TreeType.RedBlackTree:     redBlackTree.Delete(value);     break;
                default: throw new ArgumentException("Неподдерживаемый тип дерева!");
            }
        }

        public bool Search(int value, TreeType treeType) 
        { switch (treeType)
            {
                case TreeType.BinarySearchTree: return binarySearchTree.Search(value);
                case TreeType.AVLTree:          return avlTree.Search(value);
                case TreeType.RedBlackTree:     return redBlackTree.Search(value);
                default: throw new ArgumentException("Неподдерживаемый тип дерева!");
            } 
        }

    }
}
