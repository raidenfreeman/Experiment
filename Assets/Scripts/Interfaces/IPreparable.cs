
// This exists, along with IWashable, because some surfaces
// can interact with IPreparable, but not IWashable,
// and vice versa. IInteractible is not enough.
public interface IPreparable
{
    int Prepare(float timeToAdd);
}