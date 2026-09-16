namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// One row of the animal table
    /// </summary>
    /// <param name="Nr">unique number</param>
    /// <param name="Name">name of the animal</param>
    /// <param name="Art">species</param>
    /// <param name="Futter">favourite food</param>
    public record MyAnimal(int Nr, string Name, string Art, string Futter);
}
