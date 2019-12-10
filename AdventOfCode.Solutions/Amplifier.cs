using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode.Solutions
{
    public class Amplifier
    {
        private const long ADD = 1;
        private const long MULTIPLY = 2;
        private const long INPUT = 3;
        private const long OUTPUT = 4;
        private const long JUMP_IF_TRUE = 5;
        private const long JUMP_IF_FALSE = 6;
        private const long LESS_THAN = 7;
        private const long EQUALS = 8;
        private const long SET_RELATIVE_BASE = 9;
        private const long HALT = 99;

        private readonly Dictionary<long, long> program = new Dictionary<long, long>();
        private Queue<long> input = new Queue<long>();
        public long IP { get; set; }
        private long _output;
        public long Output { 
            get { return _output; } 
            set {
                _output = value;
                //Console.WriteLine(_output);
            } 
        }
        public long RelativeBase { get; set; }
        public bool IsHalted { get; set; }
        public bool IsWaiting { get; set; }

        public Amplifier(List<long> program, long? phase = null)
        {
            var idx = 0;
            foreach(var v in program)
            {
                this.program.Add(idx++, v);
            }
            input = new Queue<long>();
            if(phase != null)
                input.Enqueue(phase.Value);
        }

        public void Compute()
        {
            long in1, in2;
            IsWaiting = false;

            while (!IsHalted && !IsWaiting)
            {
                var opcode = program[IP] % 100;

                switch (opcode)
                {
                    case ADD:
                        GetInput(program, IP, out in1, out in2);
                        program[GetOutputParameter(program, IP, 3)] = in1 + in2;
                        IP += 4;
                        break;
                    case MULTIPLY:
                        GetInput(program, IP, out in1, out in2);
                        program[GetOutputParameter(program, IP, 3)] = in1 * in2;
                        IP += 4;
                        break;
                    case INPUT:
                        if (input.Count > 0)
                        {
                            program[GetOutputParameter(program, IP, 1)] = input.Dequeue();
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
                        program[GetOutputParameter(program, IP, 3)] = (in1 < in2) ? 1 : 0;
                        IP += 4;
                        break;
                    case EQUALS:
                        GetInput(program, IP, out in1, out in2);
                        program[GetOutputParameter(program, IP, 3)] = (in1 == in2) ? 1 : 0;
                        IP += 4;
                        break;
                    case SET_RELATIVE_BASE:
                        GetInput(program, IP, out in1);
                        RelativeBase += in1;
                        IP += 2;
                        break;
                    case HALT:
                        IsHalted = true;
                        break;
                    default:
                        throw new Exception($"Unrecognized OpCode {program[IP]} @ IP={IP}");
                }
            }
        }

        public void SetInput(long input)
        {
            this.input.Enqueue(input);
        }

        private void GetInput(Dictionary<long, long> program, long ip, out long in1)
        {
            in1 = GetParameterValue(program, ip, 1);
        }

        private void GetInput(Dictionary<long, long> program, long ip, out long in1, out long in2)
        {
            in1 = GetParameterValue(program, ip, 1);
            in2 = GetParameterValue(program, ip, 2);
        }

        private static long GetParameterMode(long opcode, int paramNumber)
        {
            var s = opcode.ToString();
            var len = s.Length;
            var mode = 0;
            if(s.Length > paramNumber + 1)
                mode = s[len - 2 - paramNumber] - '0';
            return mode;
        }

        private long GetParameterValue(Dictionary<long, long> program, long ip, int paramNumber)
        {
            var mode = GetParameterMode(program[ip], paramNumber);
            long value, idx1, idx2;
            switch (mode)
            {
                case 0:
                    idx1 = ip + paramNumber;
                    if (!program.ContainsKey(idx1))
                        program[idx1] = 0;

                    Debug.Assert(idx1 >= 0);

                    idx2 = program[idx1];
                    if (!program.ContainsKey(idx2))
                        program[idx2] = 0;

                    Debug.Assert(idx2 >= 0);

                    value = program[idx2];
                    break;
                case 1:
                    idx1 = ip + paramNumber;
                    if (!program.ContainsKey(idx1))
                        program[idx1] = 0;

                    Debug.Assert(idx1 >= 0);

                    value = program[idx1];
                    break;
                case 2:
                    idx1 = ip + paramNumber;
                    if (!program.ContainsKey(idx1))
                        program[idx1] = 0;

                    Debug.Assert(idx1 >= 0);

                    idx2 = program[idx1] + RelativeBase;
                    if (!program.ContainsKey(idx2))
                        program[idx2] = 0;

                    Debug.Assert(idx2 >= 0);

                    value = program[idx2];
                    break;
                default:
                    throw new Exception($"Unrecognized parameter mode: {mode}");
            }

            return value;
        }

        private long GetOutputParameter(Dictionary<long, long> program, long ip, int paramNumber)
        {
            var mode = GetParameterMode(program[ip], paramNumber);
            long value, idx1, idx2;
            switch (mode)
            {
                case 0:
                case 1:
                    idx1 = ip + paramNumber;
                    if (!program.ContainsKey(idx1))
                        program[idx1] = 0;

                    Debug.Assert(idx1 >= 0);

                    idx2 = program[idx1];
                    if (!program.ContainsKey(idx2))
                        program[idx2] = 0;

                    Debug.Assert(idx2 >= 0);

                    value = idx2;
                    break;
                //case 1:
                //    idx1 = ip + paramNumber;
                //    if (!program.ContainsKey(idx1))
                //        program[idx1] = 0;

                //    Debug.Assert(idx1 >= 0);

                //    value = program[idx1];
                //    break;
                case 2:
                    idx1 = ip + paramNumber;
                    if (!program.ContainsKey(idx1))
                        program[idx1] = 0;

                    Debug.Assert(idx1 >= 0);

                    idx2 = program[idx1] + RelativeBase;
                    if (!program.ContainsKey(idx2))
                        program[idx2] = 0;

                    Debug.Assert(idx2 >= 0);

                    value = idx2;
                    break;
                default:
                    throw new Exception($"Unrecognized parameter mode: {mode}");
            }

            return value;
        }
    }
}

