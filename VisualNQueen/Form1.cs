using System;
using System.Drawing;
using System.Windows.Forms;

namespace VisualNQueen
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Graphics g;
        Brush black;
        Brush white;

        static int size;
        
        public Form()
        {
            InitializeComponent();
            g = canvas.CreateGraphics();
            black = Brushes.DarkSlateGray;
            white = Brushes.FloralWhite;
        }

        private void Solve_Click(object sender, EventArgs e)
        {
            ResetBoard();
            GetParams();

            // Logic Here

            int[,] board = new int[size, size];

            // Initialize Board
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i,j] = 0;
                }
            }

            if (SolveNQ(board, 0))
                DrawBoard(board);
            else
                MessageBox.Show("No Possible Solutions");
        }

        bool SolveNQ (int[,] board, int col)
        {
            // Solution Found
            if (col >= size)
                return true;

            for(int i=0; i<size; i++)
            {
                if (isSafe(board, i, col))
                {
                    board[i, col] = 1;
                    if (SolveNQ(board, col + 1))
                        return true;
                    board[i, col] = 0;
                }
            }

            // Not Found
            return false;
        }

        bool isSafe(int[,] b, int row, int col)
        {
            // Check Left
            for (int i = 0; i < col; i++)
            {
                if (b[row, i] == 1)
                    return false;
            }

            // Check Upper Left Diagonal
            for (int i = row, j = col; i >= 0 && j >= 0; i--, j--)
            {
                if (b[i, j] == 1)
                    return false;
            }

            // Check Lower Left Diagonal
            for (int i = row, j = col; i < size && j >= 0; i++, j--)
            {
                if (b[i, j] == 1)
                    return false;
            }
            return true;
        }

        void DrawBoard (int[,] board)
        {
            Bitmap queen = Properties.Resources.Queen;
            int widthFactor = canvas.Width / size;
            int heightFactor = canvas.Height / size;
            bool b = true;
            
            for(int i=0; i<size; i++)
            {
                for(int j=0; j<size; j++)
                {
                    Size bound = new Size(widthFactor, heightFactor);
                    Point point = new Point(widthFactor * j, heightFactor * i);
                    if (b)
                        g.FillRectangle(black, new Rectangle(point, bound));
                    else
                        g.FillRectangle(white, new Rectangle(point, bound));
                    b = !b;
                    if (board[i, j] == 1)
                        g.DrawImage(queen, new Rectangle(point, bound));
                }
                if (size % 2 == 0)
                    b = !b;
            }
        }

        void ResetBoard ()
        {
            g.Clear(Color.White);
        }

        void GetParams()
        {
            size = (int)sizeSelect.Value;  
        }
    }
}
