using System;
using System.Collections.Generic;

namespace IronNinja
{
    interface IConsumable
    {
        string Name {get;set;}
        int Calories {get;set;}
        bool IsSpicy {get;set;}
        bool IsSweet {get;set;}
        string GetInfo();
    }
    class Food : IConsumable
    {
        public string Name {get;set;}
        public int Calories {get;set;}
        public bool IsSpicy {get;set;}
        public bool IsSweet {get;set;}
        public string GetInfo()
        {
            return $"{Name} (Food).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
        }
        public Food(string name, int calories, bool spicy, bool sweet)
        {
            Name = name;
            Calories = calories;
            IsSpicy = spicy;
            IsSweet = sweet;
        }
    }

    class Drink : IConsumable
    {
        public string Name {get;set;}
        public int Calories {get;set;}
        public bool IsSpicy {get;set;}
        public bool IsSweet {get;set;}
        public string GetInfo()
        {
            return $"Your { Name } has { Calories } calories. Is it sweet? { IsSweet }. Is it spicy? { IsSpicy }.";
        }
        public Drink(string name, int calories, bool spicy, bool sweet)
        {
            Name = name;
            Calories = calories;
            IsSpicy = spicy;
            IsSweet = sweet;
        }
    }

    class Buffet
    {
        public List<Food> Menu;
        //constructor
        public Buffet()
        {
            Menu = new List<Food>()
            {
                new Food("Pizza", 285, false, false),
                new Food("Cupcake", 131, false, true),
                new Food("Cheeseburger", 303, false, false),
                new Food("Hot Wings", 420, true, false),
                new Food("Brownie", 132, false, true),
                new Food("Fries", 365, false, false),
                new Food("Sushi", 300, true, false),
                new Food("Ice Cream",125, false, true),

            };
        }
        public Food Serve()
        {
            Random randFood = new Random();
            int randInt = randFood.Next(Menu.Count);
            return Menu[randInt];
        }
    }

    abstract class Ninja
    {
        protected int calorieIntake;
        public List<IConsumable> FoodHistory;
        public Ninja()
        {
            calorieIntake = 0;
            FoodHistory = new List<IConsumable>();
        }

        public abstract bool IsFull {get;}

        public abstract void Consume (IConsumable item);
    }

    class SpiceHound : Ninja
    {
        public override bool IsFull
        {
            get
            {
                if (calorieIntake >= 1200)
                {
                    return true;
                }
                return false;
            }
        }

        public override void Consume(IConsumable item)
        {
            if (IsFull == false)
            {
                if (item.IsSpicy)
                {
                    calorieIntake -= 5;
                }
                calorieIntake += item.Calories;
                FoodHistory.Add(item);
                item.GetInfo();
            }
            else
            {
                Console.WriteLine("The SpiceHound is full and can't eat another bite!");
            }
        }
    }

    class SweetTooth : Ninja
    {
        public override bool IsFull
        {
            get
            {
                if (calorieIntake >= 1500)
                {
                    return true;
                }
                return false;
            }
        }
        public override void Consume(IConsumable item)
        {
            if (IsFull == false)
            {
                if (item.IsSweet)
                {
                    calorieIntake += 10;
                }
                calorieIntake += item.Calories;
                FoodHistory.Add(item);
                item.GetInfo();
            }
            else
            {
                Console.WriteLine("SweetTooth is full and can't eat another bite!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Buffet yummy = new Buffet();
            SweetTooth dylan = new SweetTooth();
            SpiceHound dude = new SpiceHound();
            while (!dylan.IsFull)
            {
                dylan.Consume(yummy.Serve());
            }
            while (!dude.IsFull)
            {
                dude.Consume(yummy.Serve());
            }
            if (dylan.FoodHistory.Count > dude.FoodHistory.Count)
            {
                Console.WriteLine($"SweetTooth had the most items consumed with {dylan.FoodHistory.Count} items!");
                Console.Write("The SweetTooth ate the following: ");
                foreach (var food in dylan.FoodHistory)
                {
                    Console.Write($"{food.Name}: {food.Calories} cal; ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"SpiceHound had the most items consumed with {dude.FoodHistory.Count} items!");
                Console.Write("The Spicehound ate the following: ");
                foreach (var food in dude.FoodHistory)
                {
                    Console.Write($"{food.Name}: {food.Calories} cal; ");
                }
                Console.WriteLine();
            }
        }
    }
}