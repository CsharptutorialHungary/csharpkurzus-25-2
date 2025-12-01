using DogDatabase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DogDatabase.Core
{
    internal class ReadFromConsole: IReadFromConsole
    {
        public int ReadInt()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.Write("Invalid number, try again: ");
            }
            return input;
        }

        public string ReadNonEmptyString()
        {
            string? input = null;
            while (string.IsNullOrWhiteSpace(input))
            {
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This field cannot be empty!");
                }
            }
            return input.Trim();
        }

        public string? ReadOptionalString()
        {
            string? input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
        }

        public bool CheckIfGenderIsCorrect(string gender)
        {
            if (gender.ToLower() == "male" || gender.ToLower() == "female")
            {
                return true;
            }

            Console.WriteLine("Invalid input. Gender must be 'male' or 'female'.");
            return false;
        }

        public bool CheckIfHealthStatusIsCorrect(string healthStatus)
        {
            if (healthStatus.ToLower() == "healthy" || healthStatus.ToLower() == "ill" || healthStatus.ToLower() == "deceased")
            {
                return true;
            }

            Console.WriteLine("Invalid input. Health status must be 'healthy' or 'ill' or 'deceased'.");
            return false;
        }
    }
}
