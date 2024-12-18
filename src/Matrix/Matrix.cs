using System.Collections;
using System.Numerics;
using System.Xml.Linq;
using MyMath.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyMath.Matrix
{
    public class Matrix<TSelf> : IEquatable<Matrix<TSelf>>
        where TSelf : INumber<TSelf>
    {
        internal TSelf[,] _array = new TSelf[,] { };

        public readonly int X = 0;

        public readonly int Y = 0;

        public static readonly Matrix<TSelf> Empty = new Matrix<TSelf>();

        protected Matrix()
        {
            _array = new TSelf[,] { };
        }

        public Matrix(int x, int y)
        {
            X = x;
            Y = y;
            _array = new TSelf[X,Y];
            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    _array[i, j] = TSelf.Zero;
                }
            }
        }

        public Matrix(int x, int y, TSelf value) :
            this(x, y)
        {
            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    _array[i, j] = value;
                }
            }
        }

        public Matrix(TSelf[,] array)
        {
            _array = array;
            X = array.GetLength(0);
            Y = array.GetLength(1);
        }

        public TSelf this[int i, int j]
        {
            get { return _array[i, j]; }
            set { _array[i, j] = value; }
        }
        
        public Matrix<TSelf> Add(Matrix<TSelf> matrix)
        {
            if (matrix.X != X || matrix.Y != Y)
                throw new MatrixDimensionException($"Resulting matrix is {X}x{Y}, but addend matrix is {matrix.X}x{matrix.Y}");

            var result = new Matrix<TSelf>(X, Y);

            for (int i = 0; i < result.X; ++i)
            {
                for (int j = 0; j < result.Y; ++j)
                {
                    result._array[i, j] = _array[i, j] + matrix._array[i, j];
                }
            }

            return result;
        }
        
        
        public static Matrix<TSelf> operator + (Matrix<TSelf> matrixA, Matrix<TSelf> matrixB)
        {
            return matrixA.Add(matrixB);
        }

        public Matrix<TSelf> ScalarMultiplication(TSelf number)
        {
            var result = new Matrix<TSelf>(X, Y);

            for (int i = 0; i < result.X; ++i)
            {
                for (int j = 0; j < result.Y; ++j)
                {
                    result._array[i, j] = _array[i, j] * number;
                }
            }

            return result;
        }

        public static Matrix<TSelf> operator *(Matrix<TSelf> matrix, TSelf number)
        {
            return matrix.ScalarMultiplication(number);
        }

        public static Matrix<TSelf> operator *(TSelf number, Matrix<TSelf> matrix)
        {
            return matrix.ScalarMultiplication(number);
        }

        public static Matrix<TSelf> operator -(Matrix<TSelf> matrix)
        {
            return matrix * (- TSelf.One);
        }

        public Matrix<TSelf> Subtract(Matrix<TSelf> matrix)
        {
            return Add(-matrix);
        }

        public static Matrix<TSelf> operator -(Matrix<TSelf> matrixA, Matrix<TSelf> matrixB)
        {
            return matrixA.Subtract(matrixB);
        }

        public Matrix<TSelf> T()
        {
            var result = new Matrix<TSelf>(Y, X);

            for (int i = 0; i < result.X; ++i)
            {
                for (int j = 0; j < result.Y; ++j)
                {
                    result._array[i, j] = _array[j, i];
                }
            }

            return result;
        }

        public override bool Equals(object? obj) => this.Equals(obj as Matrix<TSelf>);

        public bool Equals(Matrix<TSelf>? p)
        {
            if (p is null)
            {
                return false;
            }

            if (ReferenceEquals(this, p))
            {
                return true;
            }

            if (GetType() != p.GetType())
            {
                return false;
            }

            if (X != p.X || Y != p.Y)
                return false;

            for (int i = 0; i < X; ++i)
                for (int j = 0; j < Y; ++j)
                    if (_array[i, j].CompareTo(p._array[i, j]) != 0)
                        return false;

            return true;
        }

        public override int GetHashCode() => (X, Y).GetHashCode();

        public static bool operator ==(Matrix<TSelf> lhs, Matrix<TSelf> rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Matrix<TSelf> lhs, Matrix<TSelf> rhs) => !(lhs == rhs);

        public override string ToString()
        {
            var str = string.Empty;

            str += Environment.NewLine;
            for (int i = 0; i < X; ++i)
            {
                str += "|";
                for (int j = 0; j < Y; ++j)
                {
                    str += $"\t{_array[i, j]}";
                }
                str += "\t|";
                str += Environment.NewLine;
            }
            return str;
        }
    }
}
