using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static int accumulator = 0;

        static void Main(string[] args)
        {
            string content;

            using (var reader = new StreamReader("input.txt"))
            {
                content = reader.ReadToEnd();
            }

            var input = content
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => new Instruction { Operation = x[0], Argument = Convert.ToInt32(x[1]) })
                .ToArray();

            try
            {
                Task1(input);
            }
            catch
            {
                Console.WriteLine($"Task 1: {accumulator}");
            }

            Task2(input);

            Console.WriteLine($"Task 2: {accumulator}");
        }

        static void Task1(Instruction[] input)
        {
            accumulator = 0;
            int index = 0;
            List<int> executed = new List<int>();            

            while (!executed.Contains(index))
            {
                executed.Add(index);

                switch(input[index].Operation)
                {
                    case "acc":
                        accumulator += input[index].Argument;
                        index++;
                        break;
                    case "jmp":
                        index += input[index].Argument;
                        break;
                    case "nop":
                        index++;
                        break;
                }

                if(index >= input.Count())
                {
                    return;
                }
            }                

            throw new Exception("Loop!");
        }        

        static void Task2(Instruction[] input)
        {
            foreach(var instruction in input.Where(x => x.Operation == "jmp" || x.Operation == "nop"))
            {
                instruction.Operation = instruction.Operation == "jmp" ? "nop" : "jmp";

                try
                {
                    Task1(input);
                    return;
                }
                catch
                {
                    instruction.Operation = instruction.Operation == "jmp" ? "nop" : "jmp";
                }
            }
        }
    }
    
    class Instruction
    {
        public string Operation { get; set; }
        public int Argument { get; set; }
    }
}
