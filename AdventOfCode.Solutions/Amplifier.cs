using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public class Amplifier
    {
        private const int ADD = 1;
        private const int MULTIPLY = 2;
        private const int INPUT = 3;
        private const int OUTPUT = 4;
        private const int JUMP_IF_TRUE = 5;
        private const int JUMP_IF_FALSE = 6;
        private const int LESS_THAN = 7;
        private const int EQUALS = 8;
        private const int HALT = 99;

        private readonly List<int> program;
        private Queue<int> input = new Queue<int>();
        public int IP { get; set; }
        public int Output { get; set; }
        public bool IsHalted { get; set; }
        public bool IsWaiting { get; set; }

        public Amplifier(List<int> program, int? phase = null)
        {
            this.program = program;
            input = new Queue<int>();
            if(phase != null)
                input.Enqueue(phase.Value);
        }

        public void Compute()
        {
            int in1, in2;
            IsWaiting = false;

            while (!IsHalted && !IsWaiting)
            {
                //Console.WriteLine($"Executing IP={ip}, OpCode={program[ip]}");
                var opcode = program[IP] % 100;

                switch (opcode)
                {
                    case ADD:
                        GetInput(program, IP, out in1, out in2);
                        program[program[IP + 3]] = in1 + in2;
                        IP += 4;
                        break;
                    case MULTIPLY:
                        GetInput(program, IP, out in1, out in2);
                        program[program[IP + 3]] = in1 * in2;
                        IP += 4;
                        break;
                    case INPUT:
                        if (input.Count > 0)
                        {
                            program[program[IP + 1]] = input.Dequeue();
                            IP += 2;
                        }
                        else
                        {
                            IsWaiting = true;
                        }
                        break;
                    case OUTPUT:
                        GetInput(program, IP, out in1);
                        Output = in1;
                        IP += 2;
                        break;
                    case JUMP_IF_TRUE:
                        GetInput(program, IP, out in1, out in2);
                        if (in1 != 0)
                            IP = in2;
                        else IP += 3;
                        break;
                    case JUMP_IF_FALSE:
                        GetInput(program, IP, out in1, out in2);
                        if (in1 == 0)
                            IP = in2;
                        else IP += 3;
                        break;
                    case LESS_THAN:
                        GetInput(program, IP, out in1, out in2);
                        program[program[IP + 3]] = (in1 < in2) ? 1 : 0;
                        IP += 4;
                        break;
                    case EQUALS:
                        GetInput(program, IP, out in1, out in2);
                        program[program[IP + 3]] = (in1 == in2) ? 1 : 0;
                        IP += 4;
                        break;
                    case HALT:
                        IsHalted = true;
                        break;
                    default:
                        throw new Exception($"Unrecognized OpCode {program[IP]} @ IP={IP}");
                }
            }
        }

        public void SetInput(int input)
        {
            this.input.Enqueue(input);
        }

        private static void GetInput(List<int> program, int ip, out int in1)
        {
            var s = program[ip].ToString();
            var len = s.Length;
            in1 = s.Length > 2 && s[len - 3] == '1' ? program[ip + 1] : program[program[ip + 1]];
        }

        private static void GetInput(List<int> program, int ip, out int in1, out int in2)
        {
            var s = program[ip].ToString();
            var len = s.Length;
            in1 = s.Length > 2 && s[len - 3] == '1' ? program[ip + 1] : program[program[ip + 1]];
            in2 = s.Length > 3 && s[len - 4] == '1' ? program[ip + 2] : program[program[ip + 2]];
        }
    }
}
