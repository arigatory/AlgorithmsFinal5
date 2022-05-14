using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace A_HeapSort
{
    class Person
    {
        public string Login { get; set; }
        public int SolvedTasksNumber { get; set; }
        public int Fine { get; set; }
    }

    class Heap
    {

    }

    public class Solution
    {
        private static TextReader _reader;
        private static TextWriter _writer;

        public static void Main(string[] args)
        {
            InitialiseStreams();

            var n = ReadInt();

            var persons = new List<Person>();
            var result = new List<Person>();

            for (var i = 0; i < n; i++)
            {
                var items = _reader.ReadLine().Split();
                persons.Add(new Person
                {
                    Login = items[0],
                    SolvedTasksNumber = int.Parse(items[1]),
                    Fine = int.Parse(items[2]
                    )
                });
            }

            foreach (var item in persons)
            {
                Console.WriteLine(item.Login);
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
