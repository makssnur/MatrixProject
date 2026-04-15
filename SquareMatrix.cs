using System;
using System.Collections.Generic;

namespace MatrixProject
{
    public class SquareMatrix : ICloneable, IComparable<SquareMatrix>
    {
        // Поля
        private double[,] data;  // данные матрицы
        private int size;        // размер (например, 3 для матрицы 3x3)

        // Свойство для доступа к размеру
        public int Size => size;

        // Индексатор - чтобы можно было писать matrix[i, j]
        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= size || j < 0 || j >= size)
                    throw new IndexOutOfRangeException("Индекс вне границ матрицы");
                return data[i, j];
            }
            set
            {
                if (i < 0 || i >= size || j < 0 || j >= size)
                    throw new IndexOutOfRangeException("Индекс вне границ матрицы");
                data[i, j] = value;
            }
        }

        //  КОНСТРУКТОРЫ 

        // 1. Пустая матрица заданного размера (все элементы 0)
        public SquareMatrix(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Размер должен быть положительным числом");
            
            this.size = size;
            data = new double[size, size];
        }

        // 2. Матрица из готового двумерного массива
        public SquareMatrix(double[,] array)
        {
            if (array.GetLength(0) != array.GetLength(1))
                throw new NotSquareMatrixException("Массив не является квадратным");
            
            size = array.GetLength(0);
            data = new double[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    data[i, j] = array[i, j];
        }

        // 3. Случайная матрица с числами от minValue до maxValue
        public SquareMatrix(int size, double minValue, double maxValue)
        {
            if (size <= 0)
                throw new ArgumentException("Размер должен быть положительным числом");
            
            this.size = size;
            data = new double[size, size];
            
            Random rand = new Random();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    data[i, j] = minValue + (maxValue - minValue) * rand.NextDouble();
        }

        //  МЕТОДЫ ДЛЯ ВЫЧИСЛЕНИЙ 

        // Вычисление детерминанта (через рекурсию или метод Гаусса)
        public double Determinant()
        {
            if (size == 1)
                return data[0, 0];
            
            if (size == 2)
                return data[0, 0] * data[1, 1] - data[0, 1] * data[1, 0];
            
            double det = 0;
            for (int j = 0; j < size; j++)
            {
                det += data[0, j] * GetCofactor(0, j) * (j % 2 == 0 ? 1 : -1);
            }
            return det;
        }

        // Получение минора (матрица без строки row и столбца col)
        private SquareMatrix GetMinor(int row, int col)
        {
            SquareMatrix minor = new SquareMatrix(size - 1);
            int mi = 0;
            for (int i = 0; i < size; i++)
            {
                if (i == row) continue;
                int mj = 0;
                for (int j = 0; j < size; j++)
                {
                    if (j == col) continue;
                    minor[mi, mj] = data[i, j];
                    mj++;
                }
                mi++;
            }
            return minor;
        }

        // Получение кофактора (знак * детерминант минора)
        private double GetCofactor(int row, int col)
        {
            SquareMatrix minor = GetMinor(row, col);
            return minor.Determinant();
        }

        // Транспонирование матрицы
        public SquareMatrix Transpose()
        {
            SquareMatrix result = new SquareMatrix(size);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] = data[j, i];
            return result;
        }

        // Обратная матрица
        public SquareMatrix Inverse()
        {
            double det = Determinant();
            if (Math.Abs(det) < 1e-10)
                throw new SingularMatrixException("Детерминант равен 0, обратной матрицы не существует");
            
            SquareMatrix result = new SquareMatrix(size);
            
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    double cofactor = GetCofactor(i, j) * ((i + j) % 2 == 0 ? 1 : -1);
                    result[j, i] = cofactor / det;  // транспонируем сразу
                }
            
            return result;
        }

        //  ПЕРЕГРУЗКА ОПЕРАТОРОВ 

        // Оператор + (сложение матриц)
        public static SquareMatrix operator +(SquareMatrix a, SquareMatrix b)
        {
            if (a.size != b.size)
                throw new IncompatibleMatrixSizeException("Матрицы должны быть одинакового размера");
            
            SquareMatrix result = new SquareMatrix(a.size);
            for (int i = 0; i < a.size; i++)
                for (int j = 0; j < a.size; j++)
                    result[i, j] = a[i, j] + b[i, j];
            return result;
        }

        // Оператор * (умножение матриц)
        public static SquareMatrix operator *(SquareMatrix a, SquareMatrix b)
        {
            if (a.size != b.size)
                throw new IncompatibleMatrixSizeException("Матрицы должны быть одинакового размера");
            
            SquareMatrix result = new SquareMatrix(a.size);
            for (int i = 0; i < a.size; i++)
                for (int j = 0; j < a.size; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < a.size; k++)
                        sum += a[i, k] * b[k, j];
                    result[i, j] = sum;
                }
            return result;
        }

        // Умножение матрицы на число
        public static SquareMatrix operator *(SquareMatrix a, double scalar)
        {
            SquareMatrix result = new SquareMatrix(a.size);
            for (int i = 0; i < a.size; i++)
                for (int j = 0; j < a.size; j++)
                    result[i, j] = a[i, j] * scalar;
            return result;
        }

        public static SquareMatrix operator *(double scalar, SquareMatrix a)
        {
            return a * scalar;
        }

        // Операторы сравнения (по детерминанту)
        public static bool operator >(SquareMatrix a, SquareMatrix b)
        {
            return a.Determinant() > b.Determinant();
        }

        public static bool operator <(SquareMatrix a, SquareMatrix b)
        {
            return a.Determinant() < b.Determinant();
        }

        public static bool operator >=(SquareMatrix a, SquareMatrix b)
        {
            return a.Determinant() >= b.Determinant();
        }

        public static bool operator <=(SquareMatrix a, SquareMatrix b)
        {
            return a.Determinant() <= b.Determinant();
        }

        // Операторы равенства
        public static bool operator ==(SquareMatrix a, SquareMatrix b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            
            if (a.size != b.size)
                return false;
            
            for (int i = 0; i < a.size; i++)
                for (int j = 0; j < a.size; j++)
                    if (Math.Abs(a[i, j] - b[i, j]) > 1e-10)
                        return false;
            return true;
        }

        public static bool operator !=(SquareMatrix a, SquareMatrix b)
        {
            return !(a == b);
        }

        // Операторы true/false (проверка на ненулевость)
        public static bool operator true(SquareMatrix a)
        {
            for (int i = 0; i < a.size; i++)
                for (int j = 0; j < a.size; j++)
                    if (Math.Abs(a[i, j]) > 1e-10)
                        return true;
            return false;
        }

        public static bool operator false(SquareMatrix a)
        {
            for (int i = 0; i < a.size; i++)
                for (int j = 0; j < a.size; j++)
                    if (Math.Abs(a[i, j]) > 1e-10)
                        return false;
            return true;
        }

        // Явное/неявное приведение типов
        public static implicit operator SquareMatrix(double[,] array)
        {
            return new SquareMatrix(array);
        }

        public static explicit operator double[,](SquareMatrix matrix)
        {
            double[,] result = new double[matrix.size, matrix.size];
            for (int i = 0; i < matrix.size; i++)
                for (int j = 0; j < matrix.size; j++)
                    result[i, j] = matrix[i, j];
            return result;
        }

        //  МЕТОДЫ Object (Equals, GetHashCode, ToString) 

        public override string ToString()
        {
            string result = $"Матрица {size}x{size}:\n";
            for (int i = 0; i < size; i++)
            {
                result += "| ";
                for (int j = 0; j < size; j++)
                {
                    result += $"{data[i, j],8:F2} ";
                }
                result += "|\n";
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is SquareMatrix other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            int hash = size.GetHashCode();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    hash = hash * 31 + data[i, j].GetHashCode();
            return hash;
        }

        //  IComparable 
        public int CompareTo(SquareMatrix other)
        {
            if (other == null) return 1;
            return this.Determinant().CompareTo(other.Determinant());
        }

        //  ICloneable (Прототип с глубоким копированием) 
        public object Clone()
        {
            SquareMatrix clone = new SquareMatrix(size);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    clone[i, j] = this[i, j];
            return clone;
        }

        // Метод для глубокого копирования (альтернативный способ)
        public SquareMatrix DeepCopy()
        {
            return (SquareMatrix)this.Clone();
        }
    }
}