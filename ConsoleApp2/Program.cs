using System;
using System.IO;

class Cars
{
    int TotalAccidents;
    int[] RepairCars;
    static int CarCount;
    float Engine;
    int Doors;
    string nameCar;
    static Random rand = new Random();

    // Конструктор по умолчанию
    public Cars()
    {
        CarCount++;
        Doors = 4;
        nameCar = "Subaru";
        Engine = 2.3f;
        TotalAccidents = 2;
        RepairCars = new int[TotalAccidents];

        for (int i = 0; i < TotalAccidents; i++)
        {
            RepairCars[i] = rand.Next(10);
        }
    }

    // Конструктор с параметрами
    public Cars(int Doors, float Engine)
    {
        CarCount++;
        nameCar = "Mercedes";
        this.Engine = Engine;
        this.Doors = Doors;
        TotalAccidents = 4;
        RepairCars = new int[TotalAccidents];

        for (int i = 0; i < TotalAccidents; i++)
        {
            RepairCars[i] = rand.Next(10);
        }
    }

    // Конструктор копирования
    public Cars(Cars Prototype)
    {
        CarCount++;
        nameCar = Prototype.nameCar;
        Engine = Prototype.Engine;
        TotalAccidents = Prototype.TotalAccidents;
        Doors = Prototype.Doors;
        RepairCars = new int[TotalAccidents];

        for (int i = 0; i < TotalAccidents; i++)
        {
            RepairCars[i] = Prototype.RepairCars[i];
        }
    }

    // Конструктор, который загружает данные из текстового файла
    public Cars(string fileName)
    {
        CarCount++;
        // Чтение данных из файла
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                nameCar = reader.ReadLine();
                Doors = int.Parse(reader.ReadLine());
                Engine = float.Parse(reader.ReadLine());
                TotalAccidents = int.Parse(reader.ReadLine());

                RepairCars = new int[TotalAccidents];
                for (int i = 0; i < TotalAccidents; i++)
                {
                    RepairCars[i] = int.Parse(reader.ReadLine());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    // Сохранение данных объекта в файл
    public void SaveToFile(string fileName)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(nameCar);
                writer.WriteLine(Doors);
                writer.WriteLine(Engine);
                writer.WriteLine(TotalAccidents);
                for (int i = 0; i < RepairCars.Length; i++)
                {
                    writer.WriteLine(RepairCars[i]);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
        }
    }

    // Вывод данных об автомобиле
    public void PrintFields()
    {
        Console.WriteLine("===== Данные об автомобиле =====");
        Console.WriteLine($"Марка автомобиля: {nameCar}");
        Console.WriteLine($"Число дверей: {Doors}");
        Console.WriteLine($"Объем двигателя: {Engine}");
        Console.WriteLine($"Количество аварий: {TotalAccidents}");


        Console.Write("Ремонты: ");
        for (int i = 0; i < RepairCars.Length; i++)
        {
            Console.Write(RepairCars[i] + " ");
        }
        Console.WriteLine("\n===============================");
    }
    public void InputDoors()
    {
        bool validInput;
        do
        {
            validInput = false;
            try
            {
                Console.Write("ВВЕДИТЕ КОЛИЧЕСТВО ДВЕРЕЙ: ");
                Doors = Convert.ToInt32(Console.ReadLine());
                if (Doors < 0 || Doors > 5)
                    throw new CarsException(Doors);
            }
            catch (CarsException ex)
            {
                validInput = true;
                ex.Processing();
            }
            catch (FormatException)
            {
                validInput = true;
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
            }
        } while (validInput);
    }

    public void InputEngine()
    {
        Console.Write("ВВЕДИТЕ ОБЪЕМ ДВИГАТЕЛЯ: ");
        Engine = Convert.ToSingle(Console.ReadLine());
        if (Engine < 0 || Engine > 6)
            throw new CarsException(Engine);

    }
    // Ввод данных с клавиатуры
    public void InputFields()
    {
        this.InputDoors();
        bool validInput;
        do
        {
            validInput = false;
            try
            {
                this.InputEngine();
            }
            catch (CarsException ex)
            {
                validInput = true;
                ex.Processing();
            }
            catch (FormatException)
            {
                validInput = true;
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
            }
        } while (validInput);
    }

    public static void StandardException()
    {
        try
        {
            try
            {
                Console.WriteLine("Введите Количество отремонтированных авто:");
                int x = Convert.ToInt32(Console.ReadLine());
                int[] y = new int[x];
            }
            catch (OverflowException ex)
            {
                throw new CarsException(ex);
            }
        }
        catch (CarsException ex)
        {
            ex.Processing();
        }
    }



    // Заполнение случайными данными
    public void FillWithRandomData()
    {
        string[] carBrands = { "Toyota", "BMW", "Audi", "Ford", "Honda", "Mercedes", "Subaru" };
        nameCar = carBrands[rand.Next(carBrands.Length)];
        Doors = rand.Next(2, 6);
        Engine = (float)(rand.NextDouble() * 4.0 + 1.0);
        TotalAccidents = rand.Next(0, 5);
        RepairCars = new int[TotalAccidents];

        for (int i = 0; i < TotalAccidents; i++)
        {
            RepairCars[i] = rand.Next(10);
        }
    }

    // Получение количества аварий
    public int GetAccidents()
    {
        return TotalAccidents;
    }

    // Получение суммарной стоимости ремонтов
    public int GetTotalRepairCost()
    {
        int totalRepairCost = 0;
        for (int i = 0; i < RepairCars.Length; i++)
        {
            totalRepairCost += RepairCars[i];
        }
        return totalRepairCost;
    }

    // Статический метод для получения общего количества автомобилей
    public static int GetCarCount()
    {
        return CarCount;
    }

    // Статическая функция для нахождения самого убитого автомобиля
    public static Cars GetMostDamagedCar(Cars[] cars)
    {
        Cars mostDamagedCar = cars[0];
        int maxRepairCost = cars[0].GetTotalRepairCost();

        for (int i = 1; i < cars.Length; i++)
        {
            int currentRepairCost = cars[i].GetTotalRepairCost();
            if (currentRepairCost > maxRepairCost)
            {
                mostDamagedCar = cars[i];
                maxRepairCost = currentRepairCost;
            }
        }

        return mostDamagedCar;
    }


    public class CarsException : Exception
    {
        int errDoors;
        float errEngine;
        public Exception standardException = null;
        public CarsException(int errDoors)
        {
            this.errDoors = errDoors;
        }
        public CarsException(float errEngine)
        {
            this.errEngine = errEngine;
        }
        public CarsException(Exception standardException)
        {
            this.standardException = standardException;
        }
        public void Processing()
        {
            if (errDoors < 0)
                Console.WriteLine("Количество дверей не может быть отрицательным.");
            else if (errDoors > 5)
                Console.WriteLine("Количество дверей не может превышать 5.");

            if (errEngine < 0)
                Console.WriteLine("Объем двигателя не может быть отрицательным.");
            else if (errEngine > 6)
                Console.WriteLine("Объем двигателя не может превышать 6.");

            if (standardException is OverflowException)
                Console.WriteLine("Переполнение при вводе данных.");
            else if (standardException is FormatException)
                Console.WriteLine("Некорректный формат данных.");
        }


    }

    // Главная функция
    public static void Main()
    {
        int size;
        do
        {
            Console.Write("Введите количество автомобилей: ");
            size = Convert.ToInt32(Console.ReadLine());
        }
        while (size <= 1);

        Cars[] carArray = new Cars[size];

        // Инициализация через разные конструкторы
        if (size > 0) carArray[0] = new Cars(); // Конструктор по умолчанию
        if (size > 1) carArray[1] = new Cars(2, 4.0f); // Конструктор с параметрами
        if (size > 2) carArray[2] = new Cars(carArray[0]); // Конструктор копирования

        // Остальные создаем стандартно
        for (int i = 3; i < size; i++)
        {
            carArray[i] = new Cars();
        }

        Console.WriteLine("\n=== Исходные данные автомобилей ===");
        for (int i = 0; i < size; i++)
        {
            carArray[i].PrintFields();
        }

        // Заполнение всех авто случайными значениями
        for (int i = 0; i < size; i++)
        {
            carArray[i].FillWithRandomData();
        }

        Console.WriteLine("\n=== Данные после заполнения случайными значениями ===");
        for (int i = 0; i < size; i++)
        {
            carArray[i].PrintFields();
        }

        // Последний автомобиль заполняем с клавиатуры
        if (size > 0)
        {
            Console.WriteLine("\nВведите данные последнего автомобиля:");
            carArray[size - 1].InputFields();
        }

        Console.WriteLine("\n=== Данные после ввода последнего авто ===");
        for (int i = 0; i < size; i++)
        {
            carArray[i].PrintFields();
        }

        // Поиск самой "убитой" машины (с максимальной суммарной стоимостью ремонтов)
        Cars mostDamagedCar = Cars.GetMostDamagedCar(carArray);

        Console.WriteLine("\n=== Самая убитая машина ===");
        mostDamagedCar.PrintFields();

        // Сохранение данных всех машин в текстовые файлы
        for (int i = 0; i < size; i++)
        {
            string fileName = $"{carArray[i].nameCar}.txt";
            carArray[i].SaveToFile(fileName);
            Console.WriteLine($"Данные автомобиля {carArray[i].nameCar} сохранены в файл {fileName}");
        }
        Cars.StandardException(); // Проверка на стандартные исключения

        // Вывод общего количества созданных автомобилей
        Console.WriteLine($"\nВсего создано автомобилей: {Cars.GetCarCount()}");
    }
}
