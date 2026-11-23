using System;

class Program
{
    static void Main(string[] args)
    {
        ICombatant knight = new Knight("Артур");
        ICombatant mage = new Mage("Мерлин");
        
        while (!knight.IsDead && !mage.IsDead)
        {
            knight.Attack(mage);
            if (mage.IsDead) break;
            mage.Attack(knight);
        }
        
        if (!knight.IsDead) Console.WriteLine($"\nПобедил Рыцарь {knight.Name}!");
        else Console.WriteLine($"\nПобедил Маг {mage.Name}!");
    }
}

public interface ICombatant
{
    string Name { get; }
    int HP { get; set; }
    bool IsDead { get; }
    int Damage { get; }
    void Attack(ICombatant target);
    void TakeDamage(int amount);
}

public class Knight : ICombatant
{
    public string Name { get; }
    public int HP { get; set; }
    public int Damage { get; }
    public bool IsDead => HP <= 0;
    
    public Knight(string name)
    {
        Name = name;
        HP = 150;
        Damage = 25;
    }
    
    public void Attack(ICombatant target)
    {
        if (IsDead) { Console.WriteLine($"{Name} мертв и не может атаковать"); return; }
        if (target.IsDead) { Console.WriteLine($"{Name} не может атаковать мертвую цель"); return; }
        
        Console.WriteLine($"Рыцарь {Name} атакует!");
        target.TakeDamage(Damage);
    }
    
    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP < 0) HP = 0;
        Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}

public class Mage : ICombatant
{
    public string Name { get; }
    public int HP { get; set; }
    public int Damage { get; }
    public bool IsDead => HP <= 0;
    
    public Mage(string name)
    {
        Name = name;
        HP = 80;
        Damage = 40;
    }
    
    public void Attack(ICombatant target)
    {
        if (IsDead) { Console.WriteLine($"Маг {Name} мертв и не может атаковать"); return; }
        if (target.IsDead) { Console.WriteLine($"Маг {Name} не может атаковать мертвую цель"); return; }
        
        Console.WriteLine($"Маг {Name} кастует двойное заклинание");
        target.TakeDamage(Damage);
        if (!target.IsDead)
        {
            target.TakeDamage(Damage);
        }
    }
    
    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP < 0) HP = 0;
        Console.WriteLine($"Маг {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}