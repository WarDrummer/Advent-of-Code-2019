using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public static class IntcodeComputer
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

        public static int Compute(List<int> program, int input = 1, int? phase = null)
        {
            var output = 0;
            var ip = 0;
            var halted = false;
            int in1, in2;

            while (!halted)
            {
               // Console.WriteLine($"Executing IP={ip}, OpCode={program[ip]}");
                var opcode = program[ip] % 100;

                switch (opcode)
                {
                    case ADD:
                        GetInput(program, ip, out in1, out in2);
                        program[program[ip + 3]] = in1 + in2;
                        ip += 4;
                        break;
                    case MULTIPLY:
                        GetInput(program, ip, out in1, out in2);
                        program[program[ip + 3]] = in1 * in2;
                        ip += 4;
                        break;
                    case INPUT:
                        if (phase != null)
                        {
                            program[program[ip + 1]] = phase.Value;
                            phase = null;
                        }
                        else
                            program[program[ip + 1]] = input;
                        ip += 2;
                        break;
                    case OUTPUT:
                        GetInput(program, ip, out in1);
                        output = in1;
                        ip += 2;
                        break;
                    case JUMP_IF_TRUE:
                        GetInput(program, ip, out in1, out in2);
                        if (in1 != 0)
                            ip = in2;
                        else ip += 3;
                        break;
                    case JUMP_IF_FALSE:
                        GetInput(program, ip, out in1, out in2);
                        if (in1 == 0)
                            ip = in2;
                        else ip += 3;
                        break;
                    case LESS_THAN:
                        GetInput(program, ip, out in1, out in2);
                        program[program[ip + 3]] = (in1 < in2) ? 1 : 0;
                        ip += 4;
                        break;
                    case EQUALS:
                        GetInput(program, ip, out in1, out in2);
                        program[program[ip + 3]] = (in1 == in2) ? 1 : 0;
                        ip += 4;
                        break;
                    case HALT:
                        halted = true;
                        break;
                    default:
                        throw new Exception($"Unrecognized OpCode {program[ip]} @ IP={ip}");
                }
            }

            return output;
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
