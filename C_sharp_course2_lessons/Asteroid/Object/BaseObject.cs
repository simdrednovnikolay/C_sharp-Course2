using System;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid.Object
{

    interface ICollision
    {
        Rectangle Rect { get; }
        bool Collision(ICollision obj);
    }
     abstract class BaseObject: ICollision
    {
        protected Point pos, dir;
        protected Size size;

        
        protected Point Pos
        {
            get => pos;
            set => pos = value;
        }
        protected Point Dir
        {
            get => dir;
            set => dir = value;
        }
        protected Size Size
        {
            get => size;
            set => size = value;
        }

        public Rectangle Rect => new Rectangle(Pos, Size);

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public virtual void Draw()
        {
            
        }

        public virtual void Update()
        {
            

        }

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(this.Rect);
        }
    }

    class Rock : BaseObject
    {
        Image image = Image.FromFile("Picture\\unitaz.png");

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, new Point(Pos.X, Pos.Y));
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X > Game.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y > Game.Height) dir.Y = -dir.Y;

        }
        public Rock(Point pos, Point dir, Size size): base(pos, dir, size)
        {

        }
    }

    class Star : BaseObject
    {

        Image image = Image.FromFile("Picture\\banana.png");

        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, new Point(Pos.X, Pos.Y));
        }

        public override void Update()
        {
            pos.X += dir.X;
            if (pos.X < -image.Width)
            {
                pos.X = Game.Width + Game.rnd.Next(10, 100);
                pos.Y = Game.rnd.Next(0, Game.Height);
            }
        }
    }

    
    class Bullet: BaseObject
    {
        Point start;

        Image image = Image.FromFile("Picture\\bumaga.png");
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            start = pos;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, new Point(Pos.X, Pos.Y));
        }

        public override void Update()
        {
            Pos = new Point(Pos.X + Dir.X, Pos.Y);
        }

        public void Reset()
        {
            Pos = start;
        }

    }

    class Ship:BaseObject
    {
        Point start;
        Image image = Image.FromFile("Picture\\ship.png");

        public int x => Pos.X;
        public int y => Pos.Y;

        public void Up()
        {
            Pos = new Point(Pos.X, Pos.Y-Dir.X);
        }

        public void Down()
        {
            Pos = new Point(Pos.X, Pos.Y + Dir.X);
        }

        public void Right()
        {
            Pos = new Point(Pos.X+Dir.X, Pos.Y );
        }

        public void Left()
        {
            Pos = new Point(Pos.X-Dir.X, Pos.Y);
        }
        public Ship(Point pos, Point dir, Size size): base(pos, dir, size)
        {
            start = pos;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, new Point(Pos.X, Pos.Y));
        }

        public override void Update()
        {
            Pos = new Point(Pos.X + Dir.X, Pos.Y);
        }


    }
}
