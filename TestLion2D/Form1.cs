using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

using Lion2D;


namespace TestLion2D
{
    public partial class Form1 : Form
    {
        private ResourceManager rm = new ResourceManager("Lion2DApp",
            typeof(Program).Assembly);

        private Scene scene;
        private TileMap bg;
        private Ball redBall;
        private Ball blueBall;
        private Ball greenBall;
        private Ball multicolorBall;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Text = "Lion2D";
            this.ClientSize = new Size(512, 384);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.KeyPreview = true;
            this.KeyDown += App_KeyDown;

            //Create scene object
            scene = new Scene(512, 384);
            scene.Animate += scene_Animate;
            scene.SpriteCollision += scene_Collide;
            this.Controls.Add(scene);

            //Create background
            Image[] tiles = new Image[]
            {
                Image.FromFile(@"Data\Tile.png")
            };

            byte[,] data = new byte[384 / 32, 512 / 32 + 1];

            for (uint y = 0; y < data.GetLength(0); y++)
                for (uint x = 0; x < data.GetLength(1); x++)
                    data[y, x] = 1;

            bg = new TileMap(tiles, data);
            bg.TileSize = new Size(32, 32);
            bg.Location = new Point(32, 0);
            scene.Backgrounds.Add(bg);

            //Create balls
            Image imgRedBall = Image.FromFile(@"Data\Red Ball.png");
            Image imgGreenBall = Image.FromFile(@"Data\Green Ball.png");
            Image imgBlueBall = Image.FromFile(@"Data\Blue Ball.png");

            redBall = new Ball(new Pose(imgRedBall));
            redBall.Location = new Point(512 - redBall.Size.Width, 0);
            scene.Sprites.Add(redBall);

            blueBall = new Ball(new Pose(imgBlueBall));
            scene.Sprites.Add(blueBall);

            greenBall = new Ball(new Pose(imgGreenBall));
            greenBall.Location = new Point(0, 384 - greenBall.Size.Height);
            scene.Sprites.Add(greenBall);

            multicolorBall = new Ball(new AnimatedPose(new Image[]
            {
                imgRedBall,
                imgGreenBall,
                imgBlueBall
            }, 500));
            multicolorBall.Location = new Point(512 - multicolorBall.Size.Width,
                384 - multicolorBall.Size.Height);
            ((AnimatedPose)multicolorBall.Poses["normal"]).Start();
            scene.Sprites.Add(multicolorBall);

            //Add a text block and image block
            scene.Overlay.Add(new TextBlock("Lion2D Demo\nAnimating...", new Font("Verdana", 12),
                Brushes.White));
            scene.Overlay.Add(new ImageBlock(((Pose)blueBall.Poses["normal"]).Image, 480, 0));

            //Begin auto-update
            scene.AutoUpdate = true;
        }

        private void App_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F)
                Console.WriteLine("Fullscreen Requested");
        }

        private void scene_Animate(Scene scene)
        {
            //Move the balls
            redBall.Bounce();
            greenBall.Bounce();
            blueBall.Bounce();
            multicolorBall.Bounce();

            //Scroll the background
            bg.Location = new Point(bg.Location.X + 1, bg.Location.Y + 1);
        }

        private void scene_Collide(Sprite sp1, Sprite sp2)
        {
            Ball ball1 = (Ball)sp1;
            Ball ball2 = (Ball)sp2;

            ball1.ChangeDirection(ball2);
            ball2.ChangeDirection(ball1);
        }

        private MemoryStream GetResourceStream(string name)
        {
            return new MemoryStream((byte[])rm.GetObject(name));
        }
    }
}
