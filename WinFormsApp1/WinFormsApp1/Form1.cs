namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Rectangle> createPickups(int num)
        {
            int spacer = 80;
            List<Rectangle> pickups = new List<Rectangle>();
            for(int i =0; i < num * spacer; i+=spacer) {
                pickups.Add(new Rectangle(400 + i, 120, 20, 20));
            }
            return pickups;
        }

        private void drawPickups(List<Rectangle> pickups, Graphics g)
        {
            Brush whiteBrush = new SolidBrush(Color.AntiqueWhite);
            foreach (Rectangle r in pickups)
            {
                g.FillEllipse(whiteBrush, r);
            }
        }

        private PointF[] createHexagon(int x, int y, int scale)
        {
            PointF[] points = new PointF[6];

            for(int i =0; i < 6; i++)
            {
                points[i] = new PointF((x + scale) * i, (y + scale) * i);
            }

            return points;
        }

        private void drawHexagons(Graphics g, Pen pen, PointF[] points)
        {
            for(int i = 0; i < 10; i++)
            {
                g.DrawPolygon(pen, points);
                for(int j =0; j < 6; j++)
                {
                    points[j].X += 9;
                    points[j].Y += 50;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int numofpickups = 6;
            Graphics g = e.Graphics;

            //pacman
            Rectangle eyeRect = new Rectangle(20, 20, 200, 200);
            Rectangle pacmanBody = new Rectangle(200, 20, 200, 200);
            Rectangle pacmanEye = new Rectangle(310, 50, 10, 10);
            List<Rectangle> pickups = createPickups(numofpickups);
            

            //creating pens and brushes
            Pen bluePen = new Pen(Color.BlueViolet);
            Pen blackPen = new Pen(Color.Black);
            Brush orangeBrush = new SolidBrush(Color.Orange);
            Brush yellowBrush = new SolidBrush(Color.Yellow);
            Brush blackBrush = new SolidBrush(Color.Black);


            //making play button
            Rectangle playButton = new Rectangle(400, 300, 150, 40);
            Font font = new Font("Arial", 16, FontStyle.Bold);
            PointF playStringPoint = new PointF(400, 305);

            //hexagon
            PointF[] hexPoints = { new PointF(20, 20), new PointF(30, 10), new PointF(50, 10), new PointF(50, 20), new PointF(40, 30), new PointF(20, 30) };

            //all my draw calls
            g.DrawLine(bluePen, 2, 2, 100, 500);
            g.DrawLine(bluePen, 2, 2, 50, 50);
            g.DrawLine(bluePen, 50, 50, 100, 500);
            g.FillPie(yellowBrush, pacmanBody, 30, 300);
            g.FillEllipse(blackBrush, pacmanEye);
            drawPickups(pickups, g);
            g.DrawRectangle(bluePen, playButton);
            g.DrawString("Play Game", font, orangeBrush, playStringPoint);
            
            drawHexagons(g, bluePen, hexPoints);
            g.DrawArc(blackPen, new Rectangle(270, 5, 50, 50), 30, 60);

        }
    }
}