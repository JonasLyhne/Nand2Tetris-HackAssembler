using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    using System.Collections.Generic;
    
    public static class SymbolTable
    {
        public static Dictionary<string, string>? CInstructionTable { get; private set; }
        public static Dictionary<string, string>? JumpInstructionTable { get; private set; }

        public static Dictionary<string, int>? Pointers { get; private set; }


        static SymbolTable()
        {
            InitializeTables();
        }
    
        private static void InitializeTables()
        {
            CInstructionTable = new Dictionary<string, string>()
            {
                { "M", "1110000" },
                { "!M", "1110001" },
                { "-M", "1110011" },
                { "M+1", "1110111" },
                { "M-1", "1110010" },
                { "D+M", "1000010" },
                { "M+D", "1000010" },
                { "D-M", "1010011" },
                { "M-D", "1000111" },
                { "D&M", "1000000" },
                { "M&D", "1000000" },
                { "D|M", "1010101" },
                { "M|D", "1010101" },
                { "0", "0101010" },
                { "1", "0111111" },
                { "-1", "0111010" },
                { "D", "0001100" },
                { "A", "0110000" },
                { "!D", "0001101" },
                { "!A", "0110001" },
                { "-D", "00011q1" },
                { "-A", "0110011" },
                { "D+1", "0011111" },
                { "A+1", "0110111" },
                { "D-1", "0001110" },
                { "A-1", "0110010" },
                { "D+A", "0000010" },
                { "A+D", "0000010" },
                { "D-A", "0010011" },
                { "A-D", "0000111" },
                { "D&A", "0000000" },
                { "A&D", "0000000" },
                { "D|A", "0010101" },
                { "A|D", "0010101" }
            };
            JumpInstructionTable = new Dictionary<string, string>()
            {
                { "JGT", "001" },
                { "JEQ", "010" },
                { "JGE", "011" },
                { "JLT", "100" },
                { "JNE", "101" },
                { "JLE", "110" },
                { "JMP", "111" }
            };
            Pointers = new Dictionary<string, int>()
            {
                { "R0", 0 },
                { "R1", 1 },
                { "R2", 2 },
                { "R3", 3 },
                { "R4", 4 },
                { "R5", 5 },
                { "R6", 6 },
                { "R7", 7 },
                { "R8", 8 },
                { "R9", 9 },
                { "R10", 10 },
                { "R11", 11 },
                { "R12", 12 },
                { "R13", 13 },
                { "R14", 14 },
                { "R15", 15 },
                { "SP", 0 },
                { "LCL", 1 },
                { "ARG", 2 },
                { "THIS", 3 },
                { "THAT", 4 },
                { "SCREEN", 16384 },
                { "KBD", 24576 }
            };
        }
        
    }    
}

