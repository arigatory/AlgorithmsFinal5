// https://contest.yandex.ru/contest/24810/run-report/68465706/

/* 
 * -- ПРИНЦИП РАБОТЫ --
   Алгоритм полностью совпадает с тем, который описан в уроке:
    1) Создадим пустую бинарную неубывающую кучу (min-heap).
    2) Вставим в неё по одному все элементы массива, сохраняя свойства кучи. Так как нам нужна сортировка от меньшего к большему,
        на вершине пирамиды должен оказаться самый маленький элемент. Если бы мы захотели реализовать сортировку 
        по убыванию — на вершине был бы самый большой элемент.
    3) Будем извлекать из неё наиболее приоритетные элементы (с самым маленьким значением), удаляя их из кучи.
  
 * -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
    На каждом шаге мы извлекаем наиболее приоритетный элемент из кучи. 
    Самый приоритетный элемент пирамиды находится на её вершине. 
    Поэтому когда извлекаем и удаляем его из кучи, мы обрабатываем все элементы и выдаем их в отсортированном порядке.
  
 * -- ВРЕМЕННАЯ СЛОЖНОСТЬ --

    Первый шаг — создание бинарной кучи. Сложность этой операции — O(1). Нам просто нужно выделить память под массив из n элементов.
    Далее вставим nn элементов подряд в бинарную кучу.
    Сложность этого этапа:
    O(log1)+O(log2)+...+O(logn)
    Считать точное значение этого выражения мы не будем, оценим его сложность сверху.
    Для этого заменим каждое слагаемое тем, которое не меньше текущего, но удобнее в формуле.
    O(logn)+O(logn)+...+O(logn)=O(nlogn)
    Последним шагом извлекаем nn элементов. Сложность этой операции также не больше, чем O(nlogn).
    O(logn)+...+O(log2)+O(log1)<O(logn)+...+O(logn)+O(logn)=O(nlogn)
    Получим:
    T=O(1)+O(nlogn)+O(nlogn)=O(nlogn)
    Это сложность алгоритма пирамидальной сортировки, которая в худшем случае работает не дольше, чем за O(nlogn).


 * -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    Для описанной реализации алгоритма пирамидальной сортировки нужно выделить память под массив из n элементов. 
    То есть потребуется O(n) дополнительной памяти. 
 */

using System;
using System.IO;

namespace A_HeapSort
{
    class Person : IComparable<Person>
    {
        public string Login { get; set; }
        public int Score { get; set; }
        public int Fine { get; set; }

        public int CompareTo(Person other)
        {
            return (-Score, Fine, Login).CompareTo((-other.Score, other.Fine, other.Login));
        }

        public static bool operator <(Person op1, Person op2)
        {
            return op1.CompareTo(op2) < 0;
        }

        public static bool operator >(Person op1, Person op2)
        {
            return op1.CompareTo(op2) > 0;
        }
    }

    class Heap
    {
        private Person[] _items;
        private int _maxSize;
        private int _lastIndex;


        public Heap(int n)
        {
            _items = new Person[n + 1];
            _lastIndex = 0;
            _maxSize = n;
        }

        public void Add(Person p)
        {
            if (_lastIndex > _maxSize)
            {
                throw new IndexOutOfRangeException();
            }
            _items[++_lastIndex] = p;
            SiftUp(_lastIndex);
        }

        private void SiftUp(int index)
        {
            if (index <= 1)
            {
                return;
            }

            int parentIndex = index / 2;
            if (_items[parentIndex] > _items[index])
            {
                Swap(index, parentIndex);
                SiftUp(parentIndex);
            }

        }

        private void Swap(int index, int parentIndex)
        {
            var temp = _items[parentIndex];
            _items[parentIndex] = _items[index];
            _items[index] = temp;
        }

        private void SiftDown(int index)
        {
            int left = 2 * index;
            int right = 2 * index + 1;

            if (_lastIndex < left)
            {
                return;
            }
            int indexLargest;
            if (right <= _lastIndex)
            {
                if (_items[left] < _items[right])
                {
                    indexLargest = left;
                }
                else
                {
                    indexLargest = right;
                }
            }
            else
            {
                indexLargest = left;
            }

            if (_items[index] > _items[indexLargest])
            {
                Swap(index, indexLargest);
                SiftDown(indexLargest);
            }
        }

        public Person GetMaxPriority()
        {
            if (_lastIndex == 0)
            {
                return null;
            }
            var result = _items[1];
            _items[1] = _items[_lastIndex--];
            SiftDown(1);
            return result;
        }

        public void Print()
        {
            for (int i = 1; i < _lastIndex; i++)
            {
                Console.WriteLine($"{_items[i].Score}\t{_items[i].Fine}\t{_items[i].Login}");

            }
            Console.WriteLine();
        }
    }

    public class Solution
    {
        private static TextReader _reader;
        private static TextWriter _writer;

        public static void Main(string[] args)
        {
            InitialiseStreams();

            var n = ReadInt();

            var heap = new Heap(n);

            for (var i = 0; i < n; i++)
            {
                var items = _reader.ReadLine().Split();
                var person = new Person
                {
                    Login = items[0],
                    Score = int.Parse(items[1]),
                    Fine = int.Parse(items[2])
                };
                heap.Add(person);
            }

            var p = heap.GetMaxPriority();

            while (p != null)
            {
                _writer.WriteLine(p.Login);
                p = heap.GetMaxPriority();
            }

            CloseStreams();
        }

        private static void CloseStreams()
        {
            _reader.Close();
            _writer.Close();
        }

        private static void InitialiseStreams()
        {
            _reader = new StreamReader(Console.OpenStandardInput());
            _writer = new StreamWriter(Console.OpenStandardOutput());
        }

        private static int ReadInt()
        {
            return int.Parse(_reader.ReadLine());
        }
    }

}
