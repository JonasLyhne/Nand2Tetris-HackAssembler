using System;
using System.Collections.Generic;

namespace HackAssembler
{
    public class Translator
    {

        public string[] TranslateToBinary(string[] assemblyString)
        {
            assemblyString = TranslateAInstructions(assemblyString);
            assemblyString = TranslateCInstructions(assemblyString);
            return assemblyString;
        }
        
        private string[] TranslateAInstructions(string[] assemblyString)
        {
            for (int i = 0; i < assemblyString.Length; i++)
            {
                if (!assemblyString[i].StartsWith('@')) continue;
                var instruction = assemblyString[i].TrimStart('@');
                
                if (int.TryParse(instruction, out var address));
                else if (SymbolTable.Pointers != null && SymbolTable.Pointers.ContainsKey(instruction))
                {
                    address = SymbolTable.Pointers[instruction];
                }
                assemblyString[i] = Convert.ToString(address, 2).PadLeft(16, '0');
            }

            return assemblyString;
        }

        private string[] TranslateCInstructions(string[] assemblyString)
        {
            for (var i = 0; i < assemblyString.Length; i++)
            {
                var dest = false;
                var cPos = 0;
                var jump = false;
                
                if (assemblyString[i].Contains('='))
                {
                    cPos = 1;
                    dest = true;
                }
                if (assemblyString[i].Contains(';')) jump = true;
                
                if(!(dest || jump)) continue;
                
                var bits = "111";
                var spited = assemblyString[i].Split(new[]{'=',';'});
                
                if (SymbolTable.CInstructionTable != null && SymbolTable.CInstructionTable.ContainsKey(spited[cPos]))
                    bits += SymbolTable.CInstructionTable[spited[cPos]];
                else
                    bits += "0000000";
                
                if (dest)
                {
                    bits += spited[0].Contains('A') ? '1' : '0';
                    bits += spited[0].Contains('D') ? '1' : '0';
                    bits += spited[0].Contains('M') ? '1' : '0';
                }
                else
                    bits += "000";
                
                if (jump)
                {
                    if (SymbolTable.JumpInstructionTable != null && SymbolTable.JumpInstructionTable.ContainsKey(spited[^1]))
                        bits += SymbolTable.JumpInstructionTable[spited[^1]];
                    else
                        bits += "000";
                }
                else
                    bits += "000";

                assemblyString[i] = bits;
            }

            return assemblyString;
        }
    }
}

