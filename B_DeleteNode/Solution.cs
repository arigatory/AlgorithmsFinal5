// https://contest.yandex.ru/contest/24810/run-report/68486991/

/* 
 * -- ПРИНЦИП РАБОТЫ --
   Алгоритм полностью с тем, который описан в уроке. У нас 4 случая:
      - Если дерево состояло из одной вершины, то после её удаления дерева не останется.
      - Если мы удаляем лист, то дерево останется одним деревом и не распадётся на части.
      - Если мы удаляем корень, у которого есть оба поддерева, то каждое поддерево станет отдельным деревом.
      - Если мы удаляем вершину, у которой есть оба ребёнка и родитель, то дерево распадётся на родительское и два поддерева.
    Заменять удаляемую вершину я буду крайней левой в правом поддереве.
  
 * -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
    Крайняя левая вершина будет больше любой из оставшихся вершин в правом поддереве, поэтому использование ее в качестве корня для
    этого поддерева не сломает бинарное дерево поиска.
  
 * -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
    Временная сложность операции удаления в наихудшем случае составляет O(h), где h — высота двоичного дерева поиска. 
    В худшем случае нам, возможно, придется путешествовать от корня к самому глубокому конечному узлу. 
    Высота перекошенного дерева может стать n, а временная сложность операции удаления может стать O(n).


 * -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    Дополнительные структуры не создаются, но возможны накладные расходи из-за использования рекурсии.

 */



// закомментируйте перед отправкой
using System;

public class Node
{
    public int Value { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}


public class Solution
{
    public static Node Remove(Node root, int key)
    {
        if (root == null)
        {
            return null;
        }

        if (root.Value > key)
        {
            root.Left = Remove(root.Left, key);
            return root;
        }
        else if (root.Value < key)
        {
            root.Right = Remove(root.Right, key);
            return root;
        }


        if (root.Left == null)
        {
            return root.Right;
        }
        else if (root.Right == null)
        {
            return root.Left;
        }
        else
        {
            var pParent = root;
            Node p = root.Right;
            while (p.Left != null)
            {
                pParent = p;
                p = p.Left;
            }
            if (pParent != root)
            {
                pParent.Left = p.Right;
            }
            else
            {
                pParent.Right = p.Right;
            }

            root.Value = p.Value;
            return root;
        }
    }

    private static Node FindParent(Node root, int key)
    {
        if (root == null)
        {
            return null;
        }

        if (root.Left == null && root.Right == null)
        {
            return null;
        }

        if (root.Left == null)
        {
            if (root.Right.Value == key)
            {
                return root;
            }
            return FindParent(root.Right, key);
        }

        if (root.Right == null)
        {
            if (root.Left.Value == key)
            {
                return root;
            }
            return FindParent(root.Left, key);
        }
        if (root.Right.Value == key || root.Left.Value == key)
        {
            return root;
        }
        var tryLeft = FindParent(root.Left, key);
        var tryRight = FindParent(root.Right, key);
        if (tryLeft != null)
        {
            return tryLeft;
        }
        if (tryRight != null)
        {
            return tryRight;
        }
        return null;

    }

    private static void Print(Node root)
    {
        if (root != null)
        {
            Print(root.Left);
            Console.Write(root.Value + " ");
            Print(root.Right);
        }
    }

    public static void Test()
    {
        var node1 = new Node(2);
        var node2 = new Node(3)
        {
            Left = node1
        };

        var node3 = new Node(1)
        {
            Right = node2
        };

        var node4 = new Node(6);
        var node5 = new Node(8)
        {
            Left = node4
        };

        var node6 = new Node(10)
        {
            Left = node5
        };

        var node7 = new Node(5)
        {
            Left = node3,
            Right = node6
        };

        Print(node7);
        Console.WriteLine();
        var newHead = Remove(node7, 10);
        Print(node7);
        Console.WriteLine();
        Console.WriteLine(newHead.Value == 5);
        Console.WriteLine(newHead.Right == node5);
        Console.WriteLine(newHead.Right.Value == 8);
    }
}
