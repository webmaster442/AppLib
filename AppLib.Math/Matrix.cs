using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace AppLib.Maths
{
    /// <summary>
    /// A class for matrix representation
    /// </summary>
    public class Matrix : ICloneable, IFormattable, IEquatable<Matrix>, IEnumerable<double>
    {
        private int nRows, nCols;
        private readonly double[] mData;

        /// <summary>
        /// Creates a new instance of matrix
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="columns">Number of columns</param>
        public Matrix(int rows, int columns)
        {
            if (rows < 1) throw new ArgumentException("must be greater than 0", nameof(rows));
            if (columns < 1) throw new ArgumentException("must be greater than 0", nameof(columns));
            nRows = rows;
            nCols = columns;
            mData = new double[rows * columns];
        }

        #region Constructors
        /// <summary>
        /// Creates a new instance of matrix with default values
        /// </summary>
        /// <param name="rows">number of rows</param>
        /// <param name="columns">number of columns</param>
        /// <param name="value">default value</param>
        public Matrix(int rows, int columns, double value) : this(rows, columns)
        {
            for (int i = 0; i < mData.Length; ++i)
            {
                mData[i] = value;
            }
        }

        /// <summary>
        /// Cretes a matrix from an other matrix
        /// </summary>
        /// <param name="other">Other matrix to use as source</param>
        public Matrix(Matrix other) : this(other.Rows, other.Columns)
        {
            if (other != null)
            {
                Buffer.BlockCopy(other.mData, 0, mData, 0, mData.Length * sizeof(double));
            }
        }

        /// <summary>
        /// Creates a matrix from a 2d array
        /// </summary>
        /// <param name="array">Array to use</param>
        public Matrix(double[,] array) : this(array.GetLength(0), array.GetLength(1))
        {
            for (int j = 0; j < Columns; ++j)
            {
                int jIndex = j * Rows;
                for (int i = 0; i < Rows; ++i)
                {
                    mData[jIndex + i] = array[i, j];
                }
            }
        }
        #endregion

        internal double[] Data
        {
            get { return mData; }
        }

        /// <summary>
        /// Number of collums
        /// </summary>
        public int Columns
        {
            get { return nCols; }
            protected set { nCols = value; }
        }

        /// <summary>
        /// Number of rows
        /// </summary>
        public int Rows
        {
            get { return nRows; }
            protected set { nRows = value; }
        }

        /// <summary>
        /// Gets or sets a value at the specified position
        /// </summary>
        /// <param name="row">Row position</param>
        /// <param name="column">Column position</param>
        /// <returns>value at position</returns>
        public double this[int row, int column]
        {
            get
            {
                RangeCheck(row, column);
                return mData[column * Rows + row];
            }
            set
            {
                RangeCheck(row, column);
                mData[column * Rows + row] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value at the specified location
        /// </summary>
        /// <param name="loc">location index</param>
        /// <returns>value at position</returns>
        internal double this[int loc]
        {
            get { return mData[loc]; }
            set { mData[loc] = value; }
        }


        private void RangeCheck(int row, int column)
        {
            if (row < 0 || row >= nRows) throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0 || column >= nCols) throw new ArgumentOutOfRangeException(nameof(column));
        }

        /// <summary>
        /// Copies matrix to a nother matrix
        /// </summary>
        /// <param name="target">Target matrix</param>
        public void CopyTo(Matrix target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (Rows != target.Rows || Columns != target.Columns) throw new Exception("Target and Source row/column count mismatch");

            Buffer.BlockCopy(mData, 0, target.mData, 0, target.mData.Length * sizeof(double));
        }

        /// <summary>
        /// Negates the matrix
        /// </summary>
        public void Negate()
        {
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    this[i, j] = -1 * this[i, j];
                }
            }
        }


        private Matrix mMatrix;
        private bool mComputed;
        private double mDeterminant = Double.MinValue;
        private bool mIsSingular;
        private int[] mPivots;

        private void Compute()
        {
            if (!mComputed)
            {
                mMatrix = (Matrix)this.Clone();
                mPivots = new int[mMatrix.Rows];

                DoCompute(mMatrix, mPivots);
                for (int j = 0; j < mMatrix.Rows; j++)
                {
                    if (mMatrix[j, j] == 0)
                    {
                        mIsSingular = true;
                        break;
                    }
                }
                mComputed = true;
            }
        }


        private static void Pivot(int m, int n, double[] B, int[] pivots)
        {
            for (int i = 0; i < pivots.Length; i++)
            {
                if (pivots[i] != i)
                {
                    int p = pivots[i];
                    for (int j = 0; j < n; j++)
                    {
                        int indexk = j * m;
                        int indexkp = indexk + p;
                        int indexkj = indexk + i;
                        double temp = B[indexkp];
                        B[indexkp] = B[indexkj];
                        B[indexkj] = temp;
                    }
                }
            }
        }

        private static void Solve(int order, int columns, double[] factor, int[] pivots, double[] data)
        {
            Pivot(order, columns, data, pivots);

            // Solve L*Y = B(piv,:)
            for (int k = 0; k < order; k++)
            {
                int korder = k * order;
                for (int i = k + 1; i < order; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        int index = j * order;
                        data[i + index] -= data[k + index] * factor[i + korder];
                    }
                }
            }
            // Solve U*X = Y;
            for (int k = order - 1; k >= 0; k--)
            {
                int korder = k + k * order;
                for (int j = 0; j < columns; j++)
                {
                    data[k + j * order] /= factor[korder];
                }
                korder = k * order;
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        int index = j * order;
                        data[i + index] -= data[k + index] * factor[i + korder];
                    }
                }
            }
        }

        private void Solve(Matrix factor, int[] pivots, Matrix result)
        {
            Solve(factor.Rows, result.Columns, ((Matrix)factor).Data, pivots, ((Matrix)result).Data);
        }

        private void DoCompute(Matrix data, int[] pivots)
        {

            Matrix matrix = data;
            int order = matrix.Rows;
            for (int i = 0; i < order; i++) pivots[i] = i;
            double[] LUcolj = new double[order];
            int indexj, indexjj;

            // Outer loop.
            for (int j = 0; j < order; j++)
            {
                indexj = j * order;
                indexjj = indexj + j;
                // Make a copy of the j-th column to localize references.
                for (int i = 0; i < order; i++) LUcolj[i] = matrix.Data[indexj + i];

                // Apply previous transformations.
                for (int i = 0; i < order; i++)
                {
                    // Most of the time is spent in the following dot product.
                    int kmax = System.Math.Min(i, j);
                    double s = 0.0;
                    for (int k = 0; k < kmax; k++) s += matrix.Data[k * order + i] * LUcolj[k];
                    matrix.Data[indexj + i] = LUcolj[i] -= s;
                }

                // Find pivot and exchange if necessary.
                int p = j;
                for (int i = j + 1; i < order; i++)
                {
                    if (Math.Abs(LUcolj[i]) > Math.Abs(LUcolj[p])) p = i;
                }
                if (p != j)
                {
                    for (int k = 0; k < order; k++)
                    {
                        int indexk = k * order;
                        int indexkp = indexk + p;
                        int indexkj = indexk + j;
                        double temp = matrix.Data[indexkp];
                        matrix.Data[indexkp] = matrix.Data[indexkj];
                        matrix.Data[indexkj] = temp;
                    }
                    pivots[j] = p;
                }

                // Compute multipliers.
                if (j < order & matrix.Data[indexjj] != 0.0)
                {
                    for (int i = j + 1; i < order; i++) matrix.Data[indexj + i] /= matrix.Data[indexjj];
                }
            }
        }

        /// <summary>
        /// Computes the determinant of the current matrix
        /// </summary>
        /// <returns>determinant value</returns>
        public double Determinant()
        {
            Compute();
            if (mIsSingular) return 0;
            if (mDeterminant == double.MinValue)
            {
                lock (mMatrix)
                {
                    mDeterminant = 1.0;
                    for (int j = 0; j < mMatrix.Rows; ++j)
                    {
                        if (mPivots[j] != j) mDeterminant = -mDeterminant * mMatrix[j, j];
                        else mDeterminant *= mMatrix[j, j];
                    }
                }
            }
            return mDeterminant;
        }

        /// <summary>
        /// Transposes the current matrix
        /// </summary>
        /// <returns>Transposed matrix</returns>
        public Matrix Transpose()
        {
            var ret = new Matrix(Columns, Rows);
            for (int j = 0; j < Columns; j++)
            {
                int index = j * Rows;
                for (int i = 0; i < Rows; i++)
                {
                    ret.mData[i * Columns + j] = mData[index + i];
                }
            }
            return ret;
        }


        /// <summary>
        /// Inverts the current matrix
        /// </summary>
        /// <returns>Inverse matrix</returns>
        public Matrix Inverse()
        {
            Compute();
            if (mIsSingular) throw new ArgumentException("Can't compute inverse, because matrix is singular");
            int order = this.Rows;
            var inverse = new Matrix(order, order);
            for (int i = 0; i < order; i++)
            {
                inverse.Data[i + order * i] = 1.0;
            }

            Solve(mMatrix, mPivots, inverse);
            return inverse;
        }

        /// <summary>
        /// Get a copy of a row
        /// </summary>
        /// <param name="index">row to copy</param>
        /// <returns>row data as double array</returns>
        public double[] GetRowCopy(int index)
        {
            double[] array = new double[Columns];
            for (int i = 0; i < Columns; ++i) array[i] = this[index, i];
            return array;
        }

        /// <summary>
        /// Return row as an IEnumerable
        /// </summary>
        /// <param name="index">Row to return</param>
        /// <returns>row data in an IEnumerable</returns>
        public IEnumerable<double> GetRow(int index)
        {
            for (int i = 0; i < Columns; ++i)
                yield return this[index, i];
        }

        /// <summary>
        /// Get a copy of a column
        /// </summary>
        /// <param name="index">column to copy</param>
        /// <returns>column data as a double array</returns>
        public double[] GetColumnCopy(int index)
        {
            double[] array = new double[Rows];
            for (int i = 0; i < Rows; ++i) array[i] = this[i, index];
            return array;
        }

        /// <summary>
        /// Return column as an IEnumerable
        /// </summary>
        /// <param name="index">column to return</param>
        /// <returns>column data as an IEnumerable</returns>
        public IEnumerable<double> GetColumn(int index)
        {
            for (int i = 0; i < Rows; ++i)
                yield return this[i, index];
        }

        /// <summary>
        /// Set column data from an array
        /// </summary>
        /// <param name="index">Column index</param>
        /// <param name="source">Column data</param>
        public void SetColumn(int index, double[] source)
        {
            if (index < 0 || index >= Columns) throw new ArgumentOutOfRangeException(nameof(index));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Length != Rows) throw new Exception("Array element number is not equal with matrix row count");
            Buffer.BlockCopy(source, 0, mData, index * Rows * sizeof(double), source.Length * sizeof(double));
        }

        /// <summary>
        /// Set Row data from an array
        /// </summary>
        /// <param name="index">Row index</param>
        /// <param name="source">Row data</param>
        public void SetRow(int index, double[] source)
        {
            if (index < 0 || index >= Rows) throw new ArgumentOutOfRangeException(nameof(index));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Length != Columns) throw new Exception("Array element number is not equal with matrix Column count");
            for (int j = 0; j < Columns; j++) this[index, j] = source[j];
        }

        /// <summary>
        /// Trims a matrix to a specified size
        /// </summary>
        /// <param name="rows">number of rows</param>
        /// <param name="columns">number of coluns</param>
        /// <returns>Trimmed matrix</returns>
        public Matrix TrimTo(int rows, int columns)
        {
            if (rows > Rows || columns > Columns) throw new ArgumentException("Can't trim the matrix to a bigger matrix");
            var ret = new Matrix(rows, columns);

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    ret[i, j] = this[i, j];
                }
            }
            return ret;
        }

        /// <summary>
        /// default getHashCode overrade
        /// </summary>
        /// <returns>HashCode of this object</returns>
        public override int GetHashCode()
        {
            return mData.GetHashCode();
        }

        /// <summary>
        /// default Equals override
        /// </summary>
        /// <param name="obj">other object</param>
        /// <returns>true, if the other object equals this</returns>
        public override bool Equals(object obj)
        {
            var m = obj as Matrix;
            if (m == null) return false;
            return this == m;
        }

        /// <summary>
        /// Default ToString override
        /// </summary>
        /// <returns>string representation of this instance</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sb.Append(this[i, j].ToString());
                    if (j != Columns - 1)
                    {
                        sb.Append(", ");
                    }
                }
                if (i != Rows - 1)
                {
                    sb.Append(Environment.NewLine);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts this instance to string with format
        /// </summary>
        /// <param name="format">Format to use</param>
        /// <param name="formatProvider">Format provider</param>
        /// <returns>string representation of this instance</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sb.Append(this[i, j].ToString(format, formatProvider));
                    if (j != Columns - 1)
                    {
                        sb.Append(", ");
                    }
                }
                if (i != Rows - 1)
                {
                    sb.Append(Environment.NewLine);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Clones this instance
        /// </summary>
        /// <returns>A copy of the current instance</returns>
        public object Clone()
        {
            return new Matrix(this);
        }

        /// <summary>
        /// returns true, if two matrices are identical
        /// </summary>
        /// <param name="other">other matrix to compare</param>
        /// <returns>true, if the other matrix is identical to this instance</returns>
        public bool Equals(Matrix other)
        {
            return this == other;
        }

        /// <summary>
        /// IEnumerable implementation
        /// </summary>
        /// <returns></returns>
        public IEnumerator<double> GetEnumerator()
        {
            for (int i=0; i<Rows; ++i)
            {
                for (int j=0; j<Columns; ++j)
                {
                    yield return this[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));
            if (left.Rows != right.Rows || left.Columns != right.Columns) throw new Exception("Input matrix row/column count mismatch");
            var ret = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < left.Rows; i++)
            {
                for (int j = 0; j < right.Columns; j++)
                {
                    ret[i, j] = left[i, j] + right[i, j];
                }
            }
            return ret;
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }
            if (left.Columns != right.Columns || left.Rows != right.Rows) return false;
            if (ReferenceEquals(left, right)) return true;
            for (int i=0; i<left.Rows; i++)
            {
                for (int j=0; j<right.Columns; j++)
                {
                    if (left[i, j] != right[i, j]) return false;
                }
            }
            return true;
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));
            if (left.Rows != right.Rows || left.Columns != right.Columns) throw new Exception("Input matrix row/column count mismatch");
            var ret = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < left.Rows; i++)
            {
                for (int j = 0; j < right.Columns; j++)
                {
                    ret[i, j] = left[i, j] - right[i, j];
                }
            }
            return ret;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));
            if (left.Columns != right.Rows) throw new Exception("Input matrix row/column count mismatch");
            var ret = new Matrix(left.Rows, right.Columns);

            for (int j = 0; j != right.Columns; j++)
            {
                for (int i = 0; i != left.Rows; i++)
                {
                    double s = 0;
                    for (int l = 0; l != left.Columns; l++)
                    {
                        s += left[i, l] * right[l, j];
                    }
                    ret[i, j] = s;
                }
            }
            return ret;
        }

        public static Matrix operator /(Matrix left, Matrix right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));
            if (left.Rows != right.Rows && left.Columns != right.Columns) throw new Exception("Input matrix row/column count mismatch");
            var ret = new Matrix(left.Rows, left.Columns);

            for (int i = 0; i < ret.Rows; i++)
            {
                for (int j = 0; j < ret.Columns; j++)
                {
                    ret[i, j] = left[i, j] / right[i, j];
                }
            }
            return ret;
        }

        public static Matrix operator +(Matrix left, double right)
        {
            var ret = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < ret.Rows; i++)
            {
                for (int j = 0; j < ret.Columns; j++)
                {
                    ret[i, j] = left[i, j] + right;
                }
            }
            return ret;
        }

        public static Matrix operator -(Matrix left, double right)
        {
            var ret = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < ret.Rows; i++)
            {
                for (int j = 0; j < ret.Columns; j++)
                {
                    ret[i, j] = left[i, j] - right;
                }
            }
            return ret;
        }

        public static Matrix operator *(Matrix left, double right)
        {
            var ret = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < ret.Rows; i++)
            {
                for (int j = 0; j < ret.Columns; j++)
                {
                    ret[i, j] = left[i, j] * right;
                }
            }
            return ret;
        }

        public static Matrix operator /(Matrix left, double right)
        {
            var ret = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < ret.Rows; i++)
            {
                for (int j = 0; j < ret.Columns; j++)
                {
                    ret[i, j] = left[i, j] / right;
                }
            }
            return ret;
        }

        public static Matrix operator %(Matrix left, double right)
        {
            var ret = new Matrix(left.Rows, left.Columns);
            for (int i = 0; i < ret.Rows; i++)
            {
                for (int j = 0; j < ret.Columns; j++)
                {
                    ret[i, j] = left[i, j] % right;
                }
            }
            return ret;
        }
    }
}
