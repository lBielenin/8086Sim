﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace _8086Sim
{
    class Program
    {
        private static readonly List<string> HEX_LETTERS = new List<string> { "A", "B", "C", "D", "E", "F" };
        private static readonly List<string> REGISTRY_TYPES = new List<string> { "AL", "AH", "BL", "BH", "CL", "CH", "DL", "DH" };
        private static readonly List<string> REGISTRY_COMMANDS = new List<string> { "MOV", "XCHANGE" };

        private static readonly Dictionary<string, string> REGISTRY_VALUES = new Dictionary<string, string>();


        static void Main(string[] args)
        {
            InitializeRegistry();
            PrintRegistry();


            Console.WriteLine("Please, provide instruction:");
            Console.WriteLine($"These can be as follows: {REGISTRY_COMMANDS}");
            var instruction = Console.ReadLine().ToUpperInvariant();
            if(!REGISTRY_COMMANDS.Contains(instruction))
                throw new ArgumentException();

            Console.WriteLine("Please, provide value of first registry in hexademical value.");
            var firstReg = Console.ReadLine().ToUpperInvariant();

            if(!REGISTRY_VALUES.ContainsKey(firstReg))
                throw new ArgumentException();

            Console.WriteLine("Please, provide value of second registry in hexademical value.");
            var secondReg = Console.ReadLine().ToUpperInvariant();

            if (!REGISTRY_VALUES.ContainsKey(secondReg))
                throw new ArgumentException();



        }

        private static void VerifyHexValue(string[] splitInput)
        {
            for (int i = 0; i < splitInput.Length; i++)
            {
                if (!int.TryParse(splitInput[i], out int _) || !HEX_LETTERS.Contains(splitInput[i].ToUpper()))
                    throw new ArgumentException($"Character number {i} is invalid!");
            }

        }

        private static void InitializeRegistry()
        {
            REGISTRY_TYPES.ForEach(type => {

                var hex = ProvideHex(type);
                REGISTRY_VALUES.Add(type, string.Empty);

            });
        }

        private static string ProvideHex(string type)
        {
            string result = string.Empty;
            string input;

            do
            {
                try 
                {
                    Console.WriteLine($"Please, provide value for {type}");
                    Console.WriteLine("Provide it in hexademical value.");
                    input = Console.ReadLine();

                    if (input.Length != 2)
                        throw new ArgumentException("Invalid input length!");

                    var splitInput = input.Split();

                    VerifyHexValue(splitInput);

                    result = input;

                }
                catch (ArgumentException e) 
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please, try again.");
                }

            } while (result == string.Empty);

            return result;
        }

        private static void PrintRegistry()
        {
            Console.WriteLine("The current state of registry is as follows:");
            foreach (var key in REGISTRY_VALUES.Keys)
            {
                Console.WriteLine($"{key} : {REGISTRY_VALUES[key]}");
            }
        }

    }
}
