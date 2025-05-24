namespace Dgd;

public class Game
{
    private List<Pet> _adoptedPets = new();
    private Timer? _statUpdateTimer;
    private const int StatUpdateIntervalMs = 5000; // 5 seconds
    
    // Events for pet status changes
    public event EventHandler<PetStatusEventArgs>? PetStatusChanged;
    public event EventHandler<PetDeathEventArgs>? PetDied;
    
    public List<Pet> AdoptedPets => _adoptedPets;
    
    public Game()
    {
        Initialize();
    }
    
    public void Start()
    {
        // Start the stat update timer
        _statUpdateTimer = new Timer(UpdateAllPetStats, null, 0, StatUpdateIntervalMs);
    }
    
    public void Stop()
    {
        // Stop the timer
        _statUpdateTimer?.Dispose();
        _statUpdateTimer = null;
    }
    
    private void Initialize()
    {
        // Load any saved pets or initialize game state
    }
    
    public void AdoptPet(string name, PetType type)
    {
        var pet = new Pet(name, type);
        _adoptedPets.Add(pet);
        
        // Raise the status changed event
        OnPetStatusChanged(pet);
    }
    
    private void UpdateAllPetStats(object? state)
    {
        foreach (var pet in _adoptedPets.ToList()) // Create a copy to avoid collection modification issues
        {
            if (pet.IsAlive)
            {
                pet.UpdateStats();
                
                // Check if the pet died after updating stats
                if (!pet.IsAlive)
                {
                    OnPetDied(pet);
                }
                else
                {
                    OnPetStatusChanged(pet);
                }
            }
        }
    }
    
    public async Task UseItemOnPet(Item item, Pet pet)
    {
        if (!pet.IsAlive)
        {
            return; // Can't use items on deceased pets
        }
        
        if (!item.CompatibleWith.Contains(pet.Type))
        {
            return; // Item not compatible with this pet type
        }
        
        // Wait for the duration of the item usage
        await Task.Delay((int)(item.Duration * 1000));
        
        // Apply the item effect to the pet
        switch (item.AffectedStat)
        {
            case PetStat.Hunger:
                pet.Hunger = Math.Min(100, pet.Hunger + item.EffectAmount);
                break;
            case PetStat.Sleep:
                pet.Sleep = Math.Min(100, pet.Sleep + item.EffectAmount);
                break;
            case PetStat.Fun:
                pet.Fun = Math.Min(100, pet.Fun + item.EffectAmount);
                break;
        }
        
        // Notify about the pet status change
        OnPetStatusChanged(pet);
    }
    
    // Get available items for a specific pet
    public List<Item> GetAvailableItemsForPet(Pet pet)
    {
        return ItemDatabase.AllItems.Where(item => item.CompatibleWith.Contains(pet.Type)).ToList();
    }
    
    // Event methods
    protected virtual void OnPetStatusChanged(Pet pet)
    {
        PetStatusChanged?.Invoke(this, new PetStatusEventArgs(pet));
    }
    
    protected virtual void OnPetDied(Pet pet)
    {
        PetDied?.Invoke(this, new PetDeathEventArgs(pet));
    }
}