using FluentAssertions;
using MyMath.Exceptions;
using System.Numerics;

namespace MyMath.Matrix.Tests
{
    public class Summation
    {
        [Fact]
        public void AddTwoMatrices_Positive()
        {
            Matrix<int> matrixA = new(3, 3, 3);
            Matrix<int> matrixB = new(3, 3, 9);
            Matrix<int> expected = new(3, 3, 12);
            (matrixA.Add(matrixB)).Should().Be(expected);
            (matrixB.Add(matrixA)).Should().Be(expected);
        }

        [Fact]
        public void AddTwoMatrices_WithOperator_Positive()
        {
            Matrix<int> matrixA = new(3, 3, 3);
            Matrix<int> matrixB = new(3, 3, 9);
            Matrix<int> expected = new(3, 3, 12);
            (matrixA + matrixB).Should().Be(expected);
            (matrixB + matrixA).Should().Be(expected);
        }

        [Fact]
        public void AddTwoMatrices_WrongDimensions_Negative()
        {
            Matrix<int> matrixA = new(3, 3, 3);
            Matrix<int> matrixB = new(4, 4, 9);

            Action summation = () => { var res = matrixA + matrixB; };
            summation.Should().Throw<MatrixDimensionException>();
        }
    }

    public class ScalarMultiplication
    {
        [Fact]
        public void MultiplyByScalar_Positive()
        {
            Matrix<int> matrix = new(3, 3, 3);
            int scalar = 6;
            Matrix<int> expected = new(3, 3, 18);
            (matrix.ScalarMultiplication(scalar)).Should().Be(expected);
        }

        [Fact]
        public void MultiplyByScalar_WithOperator_Positive()
        {
            Matrix<int> matrix = new(3, 3, 3);
            int scalar = 6;
            Matrix<int> expected = new(3, 3, 18);
            (scalar * matrix).Should().Be(expected);
            (matrix * scalar).Should().Be(expected);
        }
    }

    public class Subtraction
    {
        [Fact]
        public void SubtractTwoMatrices_Positive()
        {
            Matrix<int> matrixA = new(3, 3, 3);
            Matrix<int> matrixB = new(3, 3, 9);
            Matrix<int> expected = new(3, 3, -6);
            (matrixA.Subtract(matrixB)).Should().Be(expected);
            (matrixA.Subtract(matrixB)).Should().Be(matrixA.Add(-1 * matrixB));
        }

        [Fact]
        public void SubtractTwoMatrices_WithOverload_Positive()
        {
            Matrix<int> matrixA = new(3, 3, 3);
            Matrix<int> matrixB = new(3, 3, 9);
            Matrix<int> expected = new(3, 3, -6);
            (matrixA - matrixB).Should().Be(expected);
            (matrixA - matrixB).Should().Be(matrixA + -1 * matrixB);
        }

        [Fact]
        public void SubtractTwoMatrices_WrongDimensions_Negative()
        {
            Matrix<int> matrixA = new(3, 3, 3);
            Matrix<int> matrixB = new(4, 4, 9);

            Action subtraction = () => { var res = matrixA - matrixB; };
            subtraction.Should().Throw<MatrixDimensionException>();
        }
    }

    public class Negation
    {
        [Fact]
        public void Negation_Positive()
        {
            Matrix<int> matrix = new(3, 3, 5);
            Matrix<int> expected = new(3, 3, -5);
            (-matrix).Should().Be(expected);
        }
    }

    public class Transposition
    {
        [Fact]
        public void Transposition_Positive()
        {
            Matrix<int> matrix = new(new int[,] { 
                {  1,  2,  3 },
                {  4,  5,  6 },
                {  7,  8,  9 },
                { 10, 11, 12 } }) ;
            Matrix<int> expected = new(new int[,] {
                {  1,  4,  7,  10 },
                {  2,  5,  8,  11},
                {  3,  6,  9,  12 } });

            var transposed = matrix.T();
            (transposed).Should().Be(expected);
            (transposed.T()).Should().Be(matrix);
        }
    }
}