using System;

namespace MatrixProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" МАТРИЧНЫЙ КАЛЬКУЛЯТОР \n");
            
            bool exit = false;
            
            while (!exit)
            {
                Console.WriteLine("\n МЕНЮ ");
                Console.WriteLine("1 - Создать матрицу (случайную)");
                Console.WriteLine("2 - Создать матрицу (ввести вручную)");
                Console.WriteLine("3 - Показать информацию о матрицах");
                Console.WriteLine("4 - Сложить матрицы A + B");
                Console.WriteLine("5 - Умножить матрицы A * B");
                Console.WriteLine("6 - Найти детерминант матрицы");
                Console.WriteLine("7 - Найти обратную матрицу");
                Console.WriteLine("8 - Сравнить матрицы (по детерминанту)");
                Console.WriteLine("9 - Проверить матрицу на нулевость (true/false)");
                Console.WriteLine("10 - Клонировать матрицу (прототип)");
                Console.WriteLine("0 - Выйти");
                Console.Write("\nВыберите действие: ");
                
                string choice = Console.ReadLine();
                
                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateRandomMatrix();
                            break;
                        case "2":
                            CreateManualMatrix();
                            break;
                        case "3":
                            ShowMatrices();
                            break;
                        case "4":
                            AddMatrices();
                            break;
                        case "5":
                            MultiplyMatrices();
                            break;
                        case "6":
                            ShowDeterminant();
                            break;
                        case "7":
                            ShowInverse();
                            break;
                        case "8":
                            CompareMatrices();
                            break;
                        case "9":
                            CheckTrueFalse();
                            break;
                        case "10":
                            CloneMatrix();
                            break;
                        case "0":
                            exit = true;
                            Console.WriteLine("До свидания!");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        
        // Хранилище матриц
        static SquareMatrix matrixA = null;
        static SquareMatrix matrixB = null;
        
        // Создание случайной матрицы
        static void CreateRandomMatrix()
        {
            Console.Write("Введите размер матрицы (например, 3 для 3x3): ");
            int size = int.Parse(Console.ReadLine());
            
            Console.Write("Введите минимальное значение: ");
            double min = double.Parse(Console.ReadLine());
            
            Console.Write("Введите максимальное значение: ");
            double max = double.Parse(Console.ReadLine());
            
            Console.WriteLine("\nКакую матрицу создаём?");
            Console.WriteLine("1 - Матрица A");
            Console.WriteLine("2 - Матрица B");
            Console.Write("Выберите: ");
            
            string choice = Console.ReadLine();
            
            SquareMatrix matrix = new SquareMatrix(size, min, max);
            
            if (choice == "1")
            {
                matrixA = matrix;
                Console.WriteLine("\nМатрица A создана:");
                Console.WriteLine(matrixA.ToString());
            }
            else if (choice == "2")
            {
                matrixB = matrix;
                Console.WriteLine("\nМатрица B создана:");
                Console.WriteLine(matrixB.ToString());
            }
        }
        
        // Создание матрицы вручную
        static void CreateManualMatrix()
        {
            Console.Write("Введите размер матрицы (например, 3 для 3x3): ");
            int size = int.Parse(Console.ReadLine());
            
            double[,] array = new double[size, size];
            
            Console.WriteLine("Введите элементы матрицы построчно:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write($"Элемент [{i},{j}]: ");
                    array[i, j] = double.Parse(Console.ReadLine());
                }
            }
            
            Console.WriteLine("\nКакую матрицу создаём?");
            Console.WriteLine("1 - Матрица A");
            Console.WriteLine("2 - Матрица B");
            Console.Write("Выберите: ");
            
            string choice = Console.ReadLine();
            
            SquareMatrix matrix = new SquareMatrix(array);
            
            if (choice == "1")
            {
                matrixA = matrix;
                Console.WriteLine("\nМатрица A создана:");
                Console.WriteLine(matrixA.ToString());
            }
            else if (choice == "2")
            {
                matrixB = matrix;
                Console.WriteLine("\nМатрица B создана:");
                Console.WriteLine(matrixB.ToString());
            }
        }
        
        // Показать матрицы
        static void ShowMatrices()
        {
            if (matrixA != null)
            {
                Console.WriteLine("\n=== Матрица A ===");
                Console.WriteLine(matrixA.ToString());
                Console.WriteLine($"Детерминант: {matrixA.Determinant():F2}");
            }
            else
            {
                Console.WriteLine("\nМатрица A не создана");
            }
            
            if (matrixB != null)
            {
                Console.WriteLine("\n=== Матрица B ===");
                Console.WriteLine(matrixB.ToString());
                Console.WriteLine($"Детерминант: {matrixB.Determinant():F2}");
            }
            else
            {
                Console.WriteLine("\nМатрица B не создана");
            }
        }
        
        // Сложение матриц
        static void AddMatrices()
        {
            CheckMatricesExist();
            
            SquareMatrix result = matrixA + matrixB;
            Console.WriteLine("\nРезультат сложения A + B:");
            Console.WriteLine(result.ToString());
        }
        
        // Умножение матриц
        static void MultiplyMatrices()
        {
            CheckMatricesExist();
            
            SquareMatrix result = matrixA * matrixB;
            Console.WriteLine("\nРезультат умножения A * B:");
            Console.WriteLine(result.ToString());
        }
        
        // Показать детерминант
        static void ShowDeterminant()
        {
            Console.WriteLine("\nДля какой матрицы показать детерминант?");
            Console.WriteLine("1 - Матрица A");
            Console.WriteLine("2 - Матрица B");
            Console.Write("Выберите: ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1" && matrixA != null)
            {
                Console.WriteLine($"Детерминант матрицы A: {matrixA.Determinant():F2}");
            }
            else if (choice == "2" && matrixB != null)
            {
                Console.WriteLine($"Детерминант матрицы B: {matrixB.Determinant():F2}");
            }
            else
            {
                Console.WriteLine("Матрица не создана");
            }
        }
        
        // Показать обратную матрицу
        static void ShowInverse()
        {
            Console.WriteLine("\nДля какой матрицы найти обратную?");
            Console.WriteLine("1 - Матрица A");
            Console.WriteLine("2 - Матрица B");
            Console.Write("Выберите: ");
            
            string choice = Console.ReadLine();
            
            try
            {
                if (choice == "1" && matrixA != null)
                {
                    SquareMatrix inverse = matrixA.Inverse();
                    Console.WriteLine("\nОбратная матрица A:");
                    Console.WriteLine(inverse.ToString());
                    
                    // Проверка: A * A^(-1) = E
                    SquareMatrix check = matrixA * inverse;
                    Console.WriteLine("Проверка (A * A^(-1)):");
                    Console.WriteLine(check.ToString());
                }
                else if (choice == "2" && matrixB != null)
                {
                    SquareMatrix inverse = matrixB.Inverse();
                    Console.WriteLine("\nОбратная матрица B:");
                    Console.WriteLine(inverse.ToString());
                }
                else
                {
                    Console.WriteLine("Матрица не создана");
                }
            }
            catch (SingularMatrixException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        
        // Сравнение матриц
        static void CompareMatrices()
        {
            CheckMatricesExist();
            
            Console.WriteLine($"\nДетерминант A: {matrixA.Determinant():F2}");
            Console.WriteLine($"Детерминант B: {matrixB.Determinant():F2}");
            
            if (matrixA > matrixB)
                Console.WriteLine("Матрица A > Матрица B (по детерминанту)");
            else if (matrixA < matrixB)
                Console.WriteLine("Матрица A < Матрица B (по детерминанту)");
            else
                Console.WriteLine("Матрица A = Матрица B (по детерминанту)");
            
            if (matrixA == matrixB)
                Console.WriteLine("Матрицы равны поэлементно");
            else
                Console.WriteLine("Матрицы не равны поэлементно");
        }
        
        // Проверка true/false
        static void CheckTrueFalse()
        {
            Console.WriteLine("\nКакую матрицу проверить?");
            Console.WriteLine("1 - Матрица A");
            Console.WriteLine("2 - Матрица B");
            Console.Write("Выберите: ");
            
            string choice = Console.ReadLine();
            SquareMatrix matrix = null;
            
            if (choice == "1")
                matrix = matrixA;
            else if (choice == "2")
                matrix = matrixB;
            
            if (matrix == null)
            {
                Console.WriteLine("Матрица не создана");
                return;
            }
            
            if (matrix)
                Console.WriteLine("Матрица содержит ненулевые элементы (true)");
            else
                Console.WriteLine("Матрица нулевая (false)");
        }
        
        // Клонирование (прототип)
        static void CloneMatrix()
        {
            Console.WriteLine("\nКакую матрицу клонировать?");
            Console.WriteLine("1 - Матрица A");
            Console.WriteLine("2 - Матрица B");
            Console.Write("Выберите: ");
            
            string choice = Console.ReadLine();
            SquareMatrix original = null;
            
            if (choice == "1")
                original = matrixA;
            else if (choice == "2")
                original = matrixB;
            
            if (original == null)
            {
                Console.WriteLine("Матрица не создана");
                return;
            }
            
            SquareMatrix clone = original.DeepCopy();
            Console.WriteLine("\nОригинал:");
            Console.WriteLine(original.ToString());
            Console.WriteLine("Клон:");
            Console.WriteLine(clone.ToString());
            
            // Меняем клон, чтобы показать, что это независимая копия
            clone[0, 0] = 999;
            Console.WriteLine("\nПосле изменения клона (изменили элемент [0,0] на 999):");
            Console.WriteLine("Оригинал:");
            Console.WriteLine(original.ToString());
            Console.WriteLine("Клон:");
            Console.WriteLine(clone.ToString());
        }
        
        // Проверка существования матриц
        static void CheckMatricesExist()
        {
            if (matrixA == null || matrixB == null)
                throw new InvalidMatrixOperationException("Обе матрицы должны быть созданы");
        }
    }
}
// Готово
