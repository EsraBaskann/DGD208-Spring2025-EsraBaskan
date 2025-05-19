namespace Dgd;

public class Menu
{
    private const string StudentInfo = "Created by Esra Baskan - 215040032";
    private List<Pet> adoptedPets = new();

    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Esra Baskan - 215040032");
            Console.WriteLine("------------------------");
            Console.WriteLine("=== Pet Simulator ===");
            Console.WriteLine("1. Adopt a new pet");
            Console.WriteLine("2. View your pets");
            Console.WriteLine("3. Exit");
            Console.Write("\nSelect an option: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ShowAdoptionMenu();
                    break;
                case "2":
                    ShowPetsMenu();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void ShowAdoptionMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Adopt a Pet ===");
        Console.WriteLine("Choose a pet type:");
        
        var petTypes = Enum.GetValues<PetType>();
        for (int i = 0; i < petTypes.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {petTypes[i]}");
        }

        Console.Write("\nEnter your choice (or 0 to go back): ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= petTypes.Length)
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
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                var pet = new Pet(name, selectedType);
                adoptedPets.Add(pet);
                
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

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private void ShowPetsMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Your Pets ===");

        if (adoptedPets.Count == 0)
        {
            Console.WriteLine("You don't have any pets yet!");
        }
        else
        {
            foreach (var pet in adoptedPets)
            {
                Console.WriteLine($"\n{pet.Name} the {pet.Type}");
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
        Console.ReadKey();
    }
} 