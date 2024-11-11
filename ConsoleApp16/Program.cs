using System;

namespace TourismApp
{
    // Базовий клас для подорожей
    abstract class Tour
    {
        public int Days { get; set; }

        // Конструктор з перевіркою коректності кількості днів
        public Tour(int days)
        {
            if (days <= 0)
            {
                throw new ArgumentException("Кількість днів повинна бути більше 0.");
            }
            Days = days;
        }

        // Віртуальний метод для планування
        public virtual void Plan()
        {
            Console.WriteLine("Планування подорожі...");
        }

        // Перевантажені методи для розрахунку вартості
        public double CalculateCost()
        {
            return Days * 100; // базова вартість за день
        }

        public double CalculateCost(string tourType)
        {
            double baseCost = Days * 100;

            // Додаткова вартість на основі типу подорожі
            return tourType switch
            {
                "Excursion" => baseCost + 50 * Days, // екскурсійний тур
                "Ski" => baseCost + 80 * Days, // гірськолижний тур
                "Cruise" => baseCost + 100 * Days, // круїз
                _ => baseCost
            };
        }
    }

    // Клас для екскурсійного туру
    class ExcursionTour : Tour
    {
        public ExcursionTour(int days) : base(days) { }

        public override void Plan()
        {
            Console.WriteLine("Планування екскурсійного туру: Відвідування культурних об'єктів.");
        }
    }

    // Клас для гірськолижного туру
    class SkiTour : Tour
    {
        public SkiTour(int days) : base(days) { }

        public override void Plan()
        {
            Console.WriteLine("Планування гірськолижного туру: Спортивні активності.");
        }
    }

    // Клас для круїзу
    class Cruise : Tour
    {
        public Cruise(int days) : base(days) { }

        public override void Plan()
        {
            Console.WriteLine("Планування круїзу: Маршрути і екскурсії на суші.");
        }
    }

    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Оберіть тип подорожі:");
                Console.WriteLine("1. Екскурсійний тур");
                Console.WriteLine("2. Гірськолижний тур");
                Console.WriteLine("3. Круїз");
                Console.WriteLine("0. Вихід");
                string choice = Console.ReadLine();

                if (choice == "0") break;

                Tour tour = null;
                int days = 0;

                // Запитуємо кількість днів з перевіркою
                while (true)
                {
                    Console.WriteLine("Введіть кількість днів:");
                    if (int.TryParse(Console.ReadLine(), out days) && days > 0) break;
                    Console.WriteLine("Некоректне значення. Кількість днів повинна бути більше 0.");
                }

                // Ініціалізуємо тур відповідно до вибору
                switch (choice)
                {
                    case "1":
                        tour = new ExcursionTour(days);
                        break;
                    case "2":
                        tour = new SkiTour(days);
                        break;
                    case "3":
                        tour = new Cruise(days);
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        continue;
                }

                // Плануємо подорож
                tour.Plan();

                // Вибираємо розрахунок вартості
                Console.WriteLine("Оберіть метод розрахунку вартості:");
                Console.WriteLine("1. Тільки на основі кількості днів");
                Console.WriteLine("2. На основі кількості днів і типу подорожі");
                string costChoice = Console.ReadLine();

                double cost = 0;

                if (costChoice == "1")
                {
                    cost = tour.CalculateCost();
                }
                else if (costChoice == "2")
                {
                    string tourType = choice switch
                    {
                        "1" => "Excursion",
                        "2" => "Ski",
                        "3" => "Cruise",
                        _ => ""
                    };
                    cost = tour.CalculateCost(tourType);
                }
                else
                {
                    Console.WriteLine("Невірний вибір.");
                    continue;
                }

                Console.WriteLine($"Вартість подорожі: {cost} USD");
                Console.WriteLine();
            }
        }
    }
}

