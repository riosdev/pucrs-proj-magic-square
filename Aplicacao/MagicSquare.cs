using System;

namespace RMS
{
    class MagicSquareManager
    {
        //variables
        int[][] matrix;
        int ORDER;
        //constructors
        public MagicSquareManager(int n)
        {
            ORDER = n;
            init(out matrix, ORDER);
        }
        //properties
        //methods
        public void main()
        {
            criaMagicSquare();
            int N = calculaSoma();
            bool colunas_ok = verificaColunas(N);
            if (colunas_ok)
            {
                Console.Write($"SOMA DE CADA COLUNA É {N}");
                System.Threading.Thread.Sleep(1000);
            }
            bool linhas_ok = verificaLinhas(N);
            if (linhas_ok)
            {
                Console.Write($"SOMA DE CADA LINHA É {N}");
                System.Threading.Thread.Sleep(1000);
            }
            bool diagonais_ok = verificaDiagonais(N);
            if (diagonais_ok)
            {
                Console.Write($"SOMA DE CADA DIAGONAL É {N}");
                System.Threading.Thread.Sleep(1000);
            }
        }
        void criaMagicSquare()
        {
            populateFirstColumn();
            for (int i = 1; i < ORDER; i++)
            {
                matrix[i] = shiftBy2(matrix[i - 1]);
            }
            var _transpose = transpose(matrix);
            //A + ((n x AˆT) - n)
            matrix = sum(matrix, sum(multiply(transpose(matrix), ORDER), -ORDER));
        }
        int calculaSoma()
        {
            return sumColumn(matrix, 0);
        }
        bool verificaColunas(int N)
        {
            bool OK = true;
            for (int i = 0; i < ORDER; i++)
            {
                Console.Clear();
                print(matrix);
                printColumn(matrix, i);
                Console.CursorTop++;
                Console.CursorLeft -= Console.CursorLeft==0?0:3;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(sumColumn(matrix, i));
                System.Threading.Thread.Sleep(250);
                if (sumColumn(matrix, i) != N)
                {
                    OK = false;
                    break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorLeft = 0;
            Console.CursorTop = 19;
            return OK;
        }
        bool verificaLinhas(int N)
        {
            bool OK = true;
            for (int i = 0; i < ORDER; i++)
            {
                Console.Clear();
                print(matrix);
                printRow(matrix, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorLeft++;
                Console.Write(sumRow(matrix, i));
                System.Threading.Thread.Sleep(250);
                if (sumRow(matrix, i) != N)
                {
                    OK = false;
                    break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorLeft = 0;
            Console.CursorTop = 19;
            return OK;
        }
        bool verificaDiagonais(int N)
        {
            bool OK = true;
            for (int i = 0; i < ORDER; i++)
            {
                Console.Clear();
                print(matrix);
                printDiagonal(matrix, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorLeft++;
                Console.Write(sumDiagonal(matrix, i));
                System.Threading.Thread.Sleep(250);
                if (sumDiagonal(matrix, i) != N)
                {
                    OK = false;
                    break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorLeft = 0;
            Console.CursorTop = 19;
            return OK;
        }
        void init(out int[][] mat, int n)
        {
            mat = new int[n][];
            for (int i = 0; i < n; i++)
            {
                mat[i] = new int[n];
            }
        }
        int sumColumn(int[][] mat, int n)
        {
            var size = mat.Length;
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += mat[n][i];
            }
            return sum;
        }
        int sumRow(int[][] mat, int n)
        {
            var size = mat.Length;
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += mat[i][n];
            }
            return sum;
        }
        int sumDiagonal(int[][] mat, int n){
            var size = mat.Length;
            int sum, j;
            sum = j = 0;
            while(j < size){
                n = n > size - 1 ? 0 : n;
                sum += mat[n][j];
                n++;
                j++;
            }
            return sum;
        }
        int[][] sum(int[][] mat_a, int[][] mat_b)
        {
            var n = mat_a.Length;
            int[][] result_mat;
            init(out result_mat, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result_mat[i][j] = mat_a[i][j] + mat_b[i][j];
                }
            }
            return result_mat;
        }
        int[][] sum(int[][] mat, int s)
        {
            var n = mat.Length;
            int[][] result_mat;
            init(out result_mat, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result_mat[i][j] = mat[i][j] + s;
                }
            }
            return result_mat;
        }
        int[][] multiply(int[][] mat, int f)
        {
            var n = mat.Length;
            int[][] result_mat;
            init(out result_mat, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result_mat[i][j] = mat[i][j] * f;
                }
            }
            return result_mat;
        }
        int[][] transpose(int[][] mat)
        {
            var n = mat.Length;
            int[][] result_mat;
            init(out result_mat, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result_mat[i][j] = mat[j][i];
                }
            }
            return result_mat;
        }
        void populateFirstColumn()
        {
            for (int i = 0; i < ORDER; i++)
            {
                matrix[0][i] = i + 1;
            }
        }
        int[] shiftBy2(int[] arr)
        {
            var result_arr = new int[arr.Length];
            for (int i = 0; i < arr.Length - 2; i++)
            {
                result_arr[i + 2] = arr[i];
            }
            result_arr[0] = arr[arr.Length - 2];
            result_arr[1] = arr[arr.Length - 1];
            return result_arr;
        }
        void print(int[][] mat)
        {
            var n = mat.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(j * 4, i);
                    Console.Write(string.Format("{0,3}", mat[j][i]) + '|');
                }
                Console.WriteLine();
            }
        }
        void printColumn(int[][] mat, int n)
        {
            var size = mat.Length;
            for (int i = 0; i < size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(n * 4, i);
                Console.Write(string.Format("{0,3}", mat[n][i]));
            }
        }
        void printRow(int[][] mat, int n)
        {
            var size = mat.Length;
            for (int i = 0; i < size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(i * 4, n);
                Console.Write(string.Format("{0,3}", mat[i][n]));
            }
        }
        void printDiagonal(int[][] mat, int n){
            var size = mat.Length;
            int j = 0;
            while(j < size){
                n = n > size - 1 ? 0 : n;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(j * 4, n);
                Console.Write(string.Format("{0,3}", mat[j][n]));
                n++;
                j++;
            }
        }
    }
}