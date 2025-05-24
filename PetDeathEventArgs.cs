namespace Dgd;

public class PetDeathEventArgs : EventArgs
{
    public Pet Pet { get; }

    public PetDeathEventArgs(Pet pet)
    {
        Pet = pet;
    }
}
