namespace Dgd;

public class Pet
{
    public string Name { get; set; }
    public PetType Type { get; set; }
    public int Hunger { get; set; } = 50;
    public int Sleep { get; set; } = 50;
    public int Fun { get; set; } = 50;
    public bool IsAlive { get; private set; } = true;

    public Pet(string name, PetType type)
    {
        Name = name;
        Type = type;
    }

    public void UpdateStats()
    {
        if (!IsAlive) return;
        
        Hunger = Math.Max(0, Hunger - 1);
        Sleep = Math.Max(0, Sleep - 1);
        Fun = Math.Max(0, Fun - 1);

        if (Hunger == 0 || Sleep == 0 || Fun == 0)
        {
            IsAlive = false;
        }
    }
} 