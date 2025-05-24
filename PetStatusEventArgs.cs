namespace Dgd;

public class PetStatusEventArgs : EventArgs
{
    public Pet Pet { get; }

    public PetStatusEventArgs(Pet pet)
    {
        Pet = pet;
    }
}
