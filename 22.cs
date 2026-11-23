using System;
using System.Collections.Generic;

class Program {
    static void Main(string[] args)
    {
        Player player = new Player("Герой");
        List<IUsableItem> items = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };
        
        Console.WriteLine($"Начальные характеристики: HP = {player.HP}, Damage = {player.Damage}");
        
        foreach (var item in items)
        {
            if (player.IsDead)
            {
                Console.WriteLine("Игрок мертв, предметы использовать нельзя");
                break;
            }
            item.Use(player);
        }
        
        Console.WriteLine($"\nИтоговые характеристики: HP = {player.HP}, Damage = {player.Damage}. Статус: {(player.IsDead ? "Мертв" : "Жив")}");
    }
}

public enum Rarity { Common, Rare, Legendary }

public class Player
{
    public string Name { get; set; }
    public int HP { get; set; } = 100;
    public int Damage { get; set; } = 10;
    public bool IsDead => HP <= 0;
    
    public Player(string Name) { Name = name; }
    
    public void Heal(int amount)
    {
        HP += amount;
        Console.WriteLine($"Игрок восстановил {amount} HP. Текущее HP: {HP}");
    }
    
    public void IncreaseDamage(int amount)
    {
        Damage += amount;
        Console.WriteLine($"Урон игрока увеличен на {amount}. Текущий урон: {Damage}");
    }
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }
    void Use(Player player);
}

public class HealthPotion : IUsableItem
{
    public string Name => "Зелье здоровья";
    public string Description => "Восстанавливает 50 HP";
    public Rarity Rarity => Rarity.Common;
    
    public void Use(Player player)
    {
        Console.WriteLine($"\nВы выпили {Name}. {Description}");
        player.Heal(50);
    }
}

public class Sword : IUsableItem
{
    public string Name => "Меч";
    public string Description => "Увеличивает урон на 10";
    public Rarity Rarity => Rarity.Rare;
    
    public void Use(Player player)
    {
        Console.WriteLine($"\nВы взяли в руки {Name}. {Description}");
        player.IncreaseDamage(10);
    }
}

public class AncientScroll : IUsableItem
{
    public string Name => "Древний свиток";
    public string Description => "Наносит 20 урона, но увеличивает урон на 30";
    public Rarity Rarity => Rarity.Legendary;
    
    public void Use(Player player)
    {
        Console.WriteLine($"\nВы прочитали {Name}. {Description}");
        player.HP -= 20;
        Console.WriteLine($"Вы получили 20 урона от проклятия! Текущее HP: {player.HP}");
        player.IncreaseDamage(30);
    }
}