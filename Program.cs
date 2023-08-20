using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // ID ширина высота глубина вес
            Box box1 = new Box(1, 10, 10, 10, 2, new DateTime(2023, 1, 1));
            Box box2 = new Box(2, 8, 8, 8, 1, new DateTime(2023, 3, 15));
            Box box3 = new Box(3, 5, 5, 5, 0.5, new DateTime(2023, 2, 10));
            Box box4 = new Box(4, 10, 10, 10, 1.7, new DateTime(2021, 1, 13));
            Box box5 = new Box(5, 8, 8, 8, 1.8, new DateTime(2022, 3, 19));
            Box box6 = new Box(6, 5, 5, 5, 0.5, new DateTime(2022, 2, 1));
            Box box7 = new Box(7, 10, 10, 10, 8, new DateTime(2023, 1, 1));
            Box box8 = new Box(8, 8, 8, 8, 6, new DateTime(2023, 3, 15));
            Box box9 = new Box(9, 5, 5, 5, 9.5, new DateTime(2021, 1, 1));
            Box box10 = new Box(10, 10, 10, 10, 15, new DateTime(2022, 12, 1));
            Box box11 = new Box(11, 8, 8, 8, 6.4, new DateTime(2022, 7, 9));
            Box box12 = new Box(12, 5, 5, 5, 3.5, new DateTime(2023, 2, 10));
            Box box13 = new Box(13, 10, 10, 10, 11.5, new DateTime(2023, 1, 1));
            Box box14 = new Box(14, 8, 8, 8, 8.4, new DateTime(2021, 6, 19));
            Box box15 = new Box(15, 5, 5, 5, 1.5, new DateTime(2021, 9, 10));
            Box box16 = new Box(16, 10, 10, 10, 2, new DateTime(2022, 1, 30));
            Box box17 = new Box(17, 8, 8, 8, 1, new DateTime(2023, 3, 15));
            Box box18 = new Box(18, 5, 5, 5, 0.5, new DateTime(2023, 2, 10));
           //id ширина высота, глубина, коробки
            Pallet pallet1 = new Pallet(1, 50, 50, 50, 0);
            Pallet pallet2 = new Pallet(2, 60, 60, 60, 0);
            Pallet pallet3 = new Pallet(3, 60, 60, 60, 0);
            Pallet pallet4 = new Pallet(4, 60, 60, 60, 0);
            Pallet pallet5 = new Pallet(5, 60, 60, 60, 0);
            Pallet pallet6 = new Pallet(6, 60, 60, 60, 0);
            List<Pallet> pallets = new List<Pallet> { pallet1, pallet2, pallet3, pallet4, pallet5, pallet6 };

            pallet1.AddBox(box1);
            pallet1.AddBox(box2);
            pallet1.AddBox(box3);
            pallet2.AddBox(box4);
            pallet2.AddBox(box5);
            pallet2.AddBox(box6);
            pallet3.AddBox(box7);
            pallet3.AddBox(box8);
            pallet3.AddBox(box9);
            pallet4.AddBox(box10);
            pallet4.AddBox(box11);
            pallet4.AddBox(box12);
            pallet5.AddBox(box13);
            pallet5.AddBox(box14);
            pallet5.AddBox(box15);
            pallet6.AddBox(box16);
            pallet6.AddBox(box17);
            pallet6.AddBox(box18);

          
            // Группировка и сортировка паллет по сроку годности и весу
            var groupedPallets = pallets
                .OrderBy(p => p.GetMinExpirationDate())
                .ThenBy(p => p.GetTotalWeight())
                .ToList();

            // Вывод информации о паллетах
            Console.WriteLine("1:");
            foreach (var pallet in groupedPallets)
            {
                Console.WriteLine($"ID: {pallet.ID}, Минимальный срок годности: {pallet.GetMinExpirationDate().ToShortDateString()}, Вес: {pallet.GetTotalWeight()} кг");
            }
            // Выбор 3 паллет с наибольшим сроком годности и их сортировка по объему
            var top3Pallets = groupedPallets
                .OrderByDescending(p => p.GetMaxExpirationDate())
                .Take(3)
                .OrderBy(p => p.GetTotalVolume())
                .ToList();

            Console.WriteLine("\n2:");
            foreach (var pallet in top3Pallets)
            {
                Console.WriteLine($"ID: {pallet.ID}, Максимальный срок годности: {pallet.GetMaxExpirationDate().ToShortDateString()}, Объем: {pallet.GetTotalVolume()} куб. см");
            }
            Console.ReadLine();
        }
    }
    class Box
    {
        public int ID { get; }
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Weight { get; }
        public DateTime ProductionDate { get; }
        public DateTime ExpirationDate { get; }

        public Box(int id, double width, double height, double depth, double weight, DateTime productionDate)
        {
            ID = id;
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            ProductionDate = productionDate;
            ExpirationDate = productionDate.AddDays(100);
        }

        public bool FitsOnPallet(Pallet pallet)
        {
            return Width <= pallet.Width && Depth <= pallet.Depth;
        }
    }

    class Pallet
    {
        public int ID { get; }
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        private List<Box> Boxes { get; } = new List<Box>();

        public Pallet(int id, double width, double height, double depth, double weight)
        {
            ID = id;
            Width = width;
            Height = height;
            Depth = depth;
        }

        public void AddBox(Box box)
        {
            if (box.FitsOnPallet(this))
            {
                Boxes.Add(box);
            }
        }

        public DateTime GetMinExpirationDate()
        {
            if (Boxes.Count == 0)
            {
                return DateTime.MaxValue;
            }

            return Boxes.Min(b => b.ExpirationDate);
        }

        public DateTime GetMaxExpirationDate()
        {
            if (Boxes.Count == 0)
            {
                return DateTime.MaxValue;
            }

            return Boxes.Max(b => b.ExpirationDate);
        }

        public double GetTotalWeight()
        {
            return Boxes.Sum(b => b.Weight) + 30;
        }

        public double GetTotalVolume()
        {
            return Boxes.Sum(b => b.Width * b.Height * b.Depth) + (Width * Height * Depth);
        }
    }
}
