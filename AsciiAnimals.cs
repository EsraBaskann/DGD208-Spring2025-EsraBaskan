using System;
using System.Collections.Generic;

namespace Dgd
{
    public static class AsciiAnimals
    {
        // ASCII art for when a pet dies
        public static readonly string SadPetAscii = 
            "   .-\"\"\"\"-.   \n" +
            "  /        \\  \n" +
            " /          \\ \n" +
            "|    X  X    |\n" +
            "|            |\n" +
            "|   .-----.  |\n" +
            " \\ (      ) /\n" +
            "  \\  \"\"\"\"  / \n" +
            "   '------'  \n" +
            "   R.I.P.    ";
        
        // Dictionary to store ASCII art for each pet type
        public static readonly Dictionary<PetType, string> PetAsciiArt = new Dictionary<PetType, string>
        {
            // Dog ASCII art
            { PetType.Dog, 
                "\n   /\\_/\\    \n" +
                "  / o o \\   \n" +
                " (   \"   )  \n" +
                "  \\__U__/   \n" +
                "    ||      \n" +
                "    ||__    \n" +
                "   /    \\   " },
            
            // Cat ASCII art
            { PetType.Cat, 
                "\n    /\\___/\\  \n" +
                "   ( o   o ) \n" +
                "   (  =^=  ) \n" +
                "    )     (  \n" +
                "   (       ) \n" +
                "    \\     /  \n" +
                "     \\___/   " },
            
            // Bird ASCII art
            { PetType.Bird, 
                "\n      /\\    \n" +
                "     /  \\   \n" +
                "    /    \\  \n" +
                "   /      \\ \n" +
                "  /  .--.  \\\n" +
                "  \\_(o  o)_/\n" +
                "     \\  /   \n" +
                "      \\/    " },
            
            // Fish ASCII art
            { PetType.Fish, 
                "\n       o    \n" +
                "       /\\   \n" +
                "  ____/  \\__\n" +
                " /         /\n" +
                "/         / \n" +
                "\\________/  " },
            
            // Rabbit ASCII art
            { PetType.Rabbit, 
                "\n   /\\ /\\    \n" +
                "  ( o.o )   \n" +
                "  (  _  )   \n" +
                "   \\/ \\/    \n" +
                "  /    \\    \n" +
                " /      \\   \n" +
                "/        \\  " }
        };

        // Method to display the ASCII art for a specific pet type
        public static void DisplayPetAscii(PetType petType)
        {
            if (PetAsciiArt.ContainsKey(petType))
            {
                Console.WriteLine(PetAsciiArt[petType]);
            }
        }

        // Method to display all available pet ASCII art
        public static void DisplayAllPetAscii()
        {
            foreach (var pet in PetAsciiArt)
            {
                Console.WriteLine($"=== {pet.Key} ===");
                Console.WriteLine(pet.Value);
                Console.WriteLine();
            }
        }
    }
}
