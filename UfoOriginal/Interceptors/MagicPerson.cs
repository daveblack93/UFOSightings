namespace UfoOriginal.Interceptors;

public class MagicPerson
{
    public int CalculateTotalAge()
    {
        var age = new Random().Next(1, 100);
        return age;
    }
}