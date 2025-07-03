
public abstract partial class Animal
{
    public double Weight { get; set; } // Вес животного
    public int BirthYear { get; set; } // Год рождения

    public abstract void zvuk();
    public abstract override string ToString();
    public abstract bool DoClone(); // Абстрактный метод
}
