using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;

namespace Day17;

public enum OpCode : byte
{
    Adv = 0,
    Bxl = 1,
    Bst = 2,
    Jnz = 3,
    Bxc = 4,
    Out = 5,
    Bdv = 6,
    Cdv = 7
}

public class Computer(ImmutableArray<byte> program)
{
    public IEnumerable<byte> Run(long regA)
    {
        var ip = 0;
        var regB = 0L;
        var regC = 0L;
        while (ip < program.Length)
        {
            var opCode = (OpCode)program[ip];
            var operand = program[ip + 1];
            var combo = operand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => regA,
                5 => regB,
                6 => regC,
                _ => throw new InvalidOperationException(
                    "Invalid combo operand. Combo operands must be in the range 0-6 (7 is reserved).")
            };
            switch (opCode)
            {
                case OpCode.Adv:
                    regA >>= (int)combo;
                    break;
                case OpCode.Bxl:
                    regB ^= operand;
                    break;
                case OpCode.Bst:
                    regB = combo % 8;
                    break;
                case OpCode.Jnz:
                    if (regA == 0)
                        break;
                    ip = operand;
                    continue;
                case OpCode.Bxc:
                    regB ^= regC;
                    break;
                case OpCode.Out:
                    var outputValue = (byte)(combo % 8);
                    yield return outputValue;
                    break;
                case OpCode.Bdv:
                    regB = regA >> (int)combo;
                    break;
                case OpCode.Cdv:
                    regC = regA >> (int)combo;
                    break;
                default:
                    throw new InvalidOperationException("Invalid opcode.");
            }

            ip += 2;
        }
    }

    public long Reverse()
    {
        var emptySeed = ImmutableHashSet.Create(0L);
        var shiftPerCycle = 0;
        for (var i = 0; i < program.Length; i += 2)
        {
            if (program[i] == 0)
            {
                shiftPerCycle = program[i + 1];
            }
        }

        Debug.Assert(shiftPerCycle != 0);
        var bitsToCheck = shiftPerCycle + 8;

        var memo = new ConcurrentDictionary<(long, int), ImmutableHashSet<long>>();
        var answers = FindNumber(0, 0).ToList();

        Debug.Assert(answers.Count >= 1, "Answer not found");
        var answer = answers.Min();
        var test = Run(answer).ToList();
        Debug.Assert(test.SequenceEqual(program), "Answer does not fit");
        return answer;

        ImmutableHashSet<long> FindNumber(long a, int programIndex)
        {
            if (memo.TryGetValue((a, programIndex), out var result))
                return result;

            if (programIndex >= program.Length)
                return emptySeed;

            // Given a number with bits 00aa, we only want to change the bits 00.
            var increment = (long)BitOperations.RoundUpToPowerOf2(1ul + (ulong)a);

            var results = ImmutableHashSet.CreateBuilder<long>();
            for (var i = a; i < 1 << bitsToCheck; i += increment)
            {
                int output = Run(i).First();
                if (output != program[programIndex])
                    continue;

                var nextA = i >> shiftPerCycle;
                var tails = FindNumber(nextA, programIndex + 1);
                foreach (var tail in tails)
                {
                    var combined = (tail << shiftPerCycle) | i;
                    var secondOutput = Run(combined);
                    if (secondOutput.SequenceEqual(program.Skip(programIndex)))
                    {
                        results.Add(combined);
                    }
                }
            }

            var immutableResults = results.ToImmutableHashSet();
            memo[(a, programIndex)] = immutableResults;
            return immutableResults;
        }
    }
}