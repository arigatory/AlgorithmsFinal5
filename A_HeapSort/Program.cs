using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                var temp = _items[parentIndex];
                _items[parentIndex] = _items[index];
                _items[index] = temp;
                SiftUp(parentIndex);
            }

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
                    indexLargest = right;
                }
                else
                {
                    indexLargest = left;
                }
            }
            else
            {
                indexLargest = left;
            }

            if (_items[index] < _items[indexLargest])
            {
                var temp = (_items[indexLargest]);
                _items[indexLargest] = _items[index];
                _items[index] = temp;
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

        private static List<int> ReadList()
        {
            return _reader.ReadLine()
                .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }

}
