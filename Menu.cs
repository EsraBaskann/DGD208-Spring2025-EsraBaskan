using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Dgd;

public class Menu
{
    private const string StudentInfo = "Created by Esra Baskan - 215040032";
    private readonly Game _game;

    public Menu()
    {
        _game = new Game();
        
        // Subscribe to game events
        _game.PetStatusChanged += OnPetStatusChanged;
        _game.PetDied += OnPetDied;
        
        // Start the game
        _game.Start();
    }

    public async Task ShowMainMenu()
    {
        while (true)
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
                // Handle console error gracefully
            }
            Console.WriteLine(StudentInfo);
            Console.WriteLine("------------------------");
            Console.WriteLine("=== Pet Simulator ===");
            Console.WriteLine("1. Adopt a new pet");
            Console.WriteLine("2. View your pets");
            Console.WriteLine("3. Use items on pets");
            Console.WriteLine("4. Exit");
            Console.Write("\nSelect an option: ");

            string? choice = null;
            try
            {
                choice = Console.ReadLine();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                choice = "1"; // Default to option 1 if there's an error
            }
            switch (choice)
            {
                case "1":
                    ShowAdoptionMenu();
                    break;
                case "2":
                    ShowPetsMenu();
                    break;
                case "3":
                    // Await the task to ensure proper execution
                    await ShowItemsMenu();
                    break;
                case "4":
                    _game.Stop(); // Stop the game before exiting
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                Thread.Sleep(2000); // Wait for 2 seconds instead
            }
                    break;
            }
        }
    }

    private void ShowAdoptionMenu()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Handle console error gracefully
        }
        Console.WriteLine("=== Adopt a Pet ===");
        Console.WriteLine("Choose a pet type:");
        
        var petTypes = Enum.GetValues<PetType>();
        for (int i = 0; i < petTypes.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {petTypes[i]}");
            // Display ASCII art for this pet type
            AsciiAnimals.DisplayPetAscii(petTypes[i]);
            Console.WriteLine(); // Add a blank line after each pet
        }

        Console.Write("\nEnter your choice (or 0 to go back): ");
        string? input = null;
            try
            {
                input = Console.ReadLine();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                input = "0"; // Default to going back if there's an error
            }
            
            if (int.TryParse(input, out int choice))
        {
            if (choice == 0) return;
            
            if (choice > 0 && choice <= petTypes.Length)
            {
                var selectedType = petTypes[choice - 1];
                string typeSpecificMessage = selectedType switch
                {
                    PetType.Dog => "*excited tail wagging* What's my name gonna be? Woof! ",
                    PetType.Cat => "*purrs curiously* Meow... I need a fancy name: ",
                    PetType.Bird => "*chirps happily* Tweet! What name shall I sing? ",
                    PetType.Fish => "*blub blub* A name as shiny as my scales: ",
                    PetType.Rabbit => "*wiggles nose* *hop hop* I need a cute name: ",
                    _ => "Enter a name for your pet: "
                };

                Console.Write("\n" + typeSpecificMessage);
                string? name = null;
                try
                {
                    name = Console.ReadLine();
                }
                catch (InvalidOperationException)
                {
                    // Handle console input error gracefully
                    name = "Pet" + DateTime.Now.Ticks % 1000; // Generate a default name if there's an error
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _game.AdoptPet(name, selectedType);
                    
                    string adoptionMessage = selectedType switch
                    {
                        PetType.Dog => $"\nWoohoo! {name} is wagging their tail with joy!",
                        PetType.Cat => $"\nPurrfect! {name} has chosen to grace you with their presence!",
                        PetType.Bird => $"\nChirp chirp! {name} is singing with happiness!",
                        PetType.Fish => $"\nSplash! {name} is doing a happy swim!",
                        PetType.Rabbit => $"\nHop hop! {name} is bouncing with excitement!",
                        _ => $"\nCongratulations! You've adopted a {selectedType} named {name}!"
                    };
                    
                    Console.WriteLine(adoptionMessage);
                }
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                Thread.Sleep(2000); // Wait for 2 seconds instead
            }
    }

    private void ShowPetsMenu()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Handle console error gracefully
        }
        Console.WriteLine("=== Your Pets ===");

        var adoptedPets = _game.AdoptedPets;
        if (adoptedPets.Count == 0)
        {
            Console.WriteLine("You don't have any pets yet!");
        }
        else
        {
            foreach (var pet in adoptedPets)
            {
                Console.WriteLine($"\n{pet.Name} the {pet.Type}");
                // Display ASCII art for this pet
                AsciiAnimals.DisplayPetAscii(pet.Type);
                Console.WriteLine($"Status: {(pet.IsAlive ? "Alive" : "Deceased")}");
                if (pet.IsAlive)
                {
                    Console.WriteLine($"Hunger: {pet.Hunger}");
                    Console.WriteLine($"Sleep: {pet.Sleep}");
                    Console.WriteLine($"Fun: {pet.Fun}");
                }
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                Thread.Sleep(2000); // Wait for 2 seconds instead
            }
    }
    
    // Changed from async void to async Task to properly handle exceptions
    private async Task ShowItemsMenu()
    {
        if (_game.AdoptedPets.Count == 0 || !_game.AdoptedPets.Any(p => p.IsAlive))
        {
            try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Handle console error gracefully
        }
            Console.WriteLine("You don't have any living pets to use items on!");
            Console.WriteLine("\nPress any key to continue...");
            try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                Thread.Sleep(2000); // Wait for 2 seconds instead
            }
            return;
        }
        
        while (true)
        {
            try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Handle console error gracefully
        }
            Console.WriteLine("=== Use Items ===");
            Console.WriteLine("Choose a pet to use an item on:");
            
            var livingPets = _game.AdoptedPets.Where(p => p.IsAlive).ToList();
            for (int i = 0; i < livingPets.Count; i++)
            {
                var pet = livingPets[i];
                Console.WriteLine($"{i + 1}. {pet.Name} the {pet.Type} (Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun})");
            }
            
            Console.WriteLine("0. Back to main menu");
            Console.Write("\nEnter your choice: ");
            
            string? petInput = null;
            try
            {
                petInput = Console.ReadLine();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                petInput = "0"; // Default to going back if there's an error
            }
            
            // Validate input before parsing
            if (!string.IsNullOrWhiteSpace(petInput) && int.TryParse(petInput, out int petChoice))
            {
                if (petChoice == 0) return;
                
                if (petChoice > 0 && petChoice <= livingPets.Count)
                {
                    var selectedPet = livingPets[petChoice - 1];
                    await ShowItemsForPet(selectedPet);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                Thread.Sleep(2000); // Wait for 2 seconds instead
            }
                }
            }
        }
    }
    
    private async Task ShowItemsForPet(Pet pet)
    {
        while (true)
        {
            try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Handle console error gracefully
        }
            Console.WriteLine($"=== Items for {pet.Name} the {pet.Type} ===");
            Console.WriteLine($"Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}");
            Console.WriteLine("\nChoose an item to use:");
            
            var availableItems = _game.GetAvailableItemsForPet(pet);
            for (int i = 0; i < availableItems.Count; i++)
            {
                var item = availableItems[i];
                Console.WriteLine($"{i + 1}. {item.Name} - Affects {item.AffectedStat} (+{item.EffectAmount})");
            }
            
            Console.WriteLine("0. Back to pet selection");
            Console.Write("\nEnter your choice: ");
            
            string? itemInput = null;
            try
            {
                itemInput = Console.ReadLine();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                itemInput = "0"; // Default to going back if there's an error
            }
            
            // Validate input before parsing
            if (!string.IsNullOrWhiteSpace(itemInput) && int.TryParse(itemInput, out int itemChoice))
            {
                if (itemChoice == 0) return;
                
                // Make sure the choice is valid and the item list hasn't changed
                if (itemChoice > 0 && itemChoice <= availableItems.Count)
                {
                    var selectedItem = availableItems[itemChoice - 1];
                    
                    try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Handle console error gracefully
        }
                    Console.WriteLine($"Using {selectedItem.Name} on {pet.Name}...");
                    Console.WriteLine($"This will take {selectedItem.Duration} seconds.");
                    
                    // Use the item on the pet
                    await _game.UseItemOnPet(selectedItem, pet);
                    
                    Console.WriteLine($"\n{pet.Name} used {selectedItem.Name}!");
                    Console.WriteLine($"New stats - Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}");
                    Console.WriteLine("\nPress any key to continue...");
                    try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                Thread.Sleep(2000); // Wait for 2 seconds instead
            }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                Thread.Sleep(2000); // Wait for 2 seconds instead
            }
                }
            }
        }
    }
    
    // Event handlers
    private void OnPetStatusChanged(object? sender, PetStatusEventArgs e)
    {
        // This could be used for real-time updates in a more advanced UI
    }
    
    private void OnPetDied(object? sender, PetDeathEventArgs e)
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Handle console error gracefully
        }
        Console.WriteLine($"\nOh no! {e.Pet.Name} the {e.Pet.Type} has passed away.");
        // Display sad ASCII art
        Console.WriteLine(AsciiAnimals.SadPetAscii);
        Console.WriteLine($"Goodbye {e.Pet.Name}... (2023-{DateTime.Now.Year})");
        Console.WriteLine("\nPress any key to continue...");
        try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Handle console input error gracefully
                Thread.Sleep(2000); // Wait for 2 seconds instead
            }
    }
}