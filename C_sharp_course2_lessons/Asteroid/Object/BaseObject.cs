using System;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid.Object
{
    class BaseObject
    {
        protected Point pos, dir;
        protected Size size;

        Image image = Image.FromFile("Picture\\rock.png");
        protected Point Pos
        {
            get => pos;
            set => pos=value;
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
        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, new Point(Pos.X, Pos.Y));
        }

        public void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X < 0 | pos.X > Game.Width) dir.X = -dir.X;
            if (pos.Y < 0 | pos.Y > Game.Height) dir.Y = -dir.Y;

        }
    }

    class Star:BaseObject
    {
        
        Image image = Image.FromFile("Picture\\star.png");

        public Star(Point pos, Point dir, Size size):base(pos,dir,size)
        {
            
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, new Point(Pos.X, Pos.Y));
        }
    }
}
