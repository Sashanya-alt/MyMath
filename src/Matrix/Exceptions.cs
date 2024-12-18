namespace MyMath.Exceptions
{
    public class MatrixDimensionException : Exception
    {
        public MatrixDimensionException() { }

        public MatrixDimensionException(string message)
            : base(message) { }

        public MatrixDimensionException(string message, Exception inner)
            : base(message, inner) { }
    }
}
