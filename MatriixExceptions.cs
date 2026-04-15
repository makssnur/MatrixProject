using System;

namespace MatrixProject
{
    // Исключение для несовместимых размеров матриц
    public class IncompatibleMatrixSizeException : Exception
    {
        public IncompatibleMatrixSizeException(string message) : base(message) { }
    }

    // Исключение для несовместимых операций (например, умножение с разными размерами)
    public class InvalidMatrixOperationException : Exception
    {
        public InvalidMatrixOperationException(string message) : base(message) { }
    }

    // Исключение для случая, когда матрица не квадратная
    public class NotSquareMatrixException : Exception
    {
        public NotSquareMatrixException(string message) : base(message) { }
    }

    // Исключение для случая, когда матрица вырожденная (детерминант = 0)
    public class SingularMatrixException : Exception
    {
        public SingularMatrixException(string message) : base(message) { }
    }
}