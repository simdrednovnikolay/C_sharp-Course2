using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asteroid.Object;

namespace Asteroid
{
    static class Game
    {
        static public ulong Counter { get; private set; } = 0;

        static BufferedGraphicsContext context;
        static public BufferedGraphics Buffer { get; private set; }

        // Свойства
        // Ширина и высота игрового поля

        static public Random Random { get; } = new Random();
        static public int Width { get; private set; }
        static public int Height { get; private set; }
        static public Image background = Image.FromFile("Picture\\back.png");
        static Timer timer = new Timer();
        static List<BaseObject> _objs;
        static List<Bullet> _bullets;
        static Ship _ship;

        static Game()
        {

        }

        static public void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            timer.Start();
            Load();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            _bullets.Add(new Bullet(new Point(_ship.x, _ship.y), new Point(18,0), new Size(10,2)));

            if (e.KeyCode == Keys.Up)
                _ship.Up();
            if (e.KeyCode == Keys.Down)
                _ship.Down();
            if (e.KeyCode == Keys.Left)
                _ship.Left();
            if (e.KeyCode == Keys.Right)
                _ship.Right();

        }

        static public Random rnd { get; } = new Random();
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        
        static public void Load()
        {


            _objs = new List<BaseObject>();
            for (int i = 0; i < 30; i++)
                _objs.Add(new Rock(new Point(Game.rnd.Next(0, 799), Game.rnd.Next(10, 500)), new Point(0, 0), new Size(30, 30)));
            for (int i = 15; i < 60; i++)
                _objs.Add(new Star(new Point(800, i+100), new Point(rnd.Next(0-i,0), 0), new Size(40, 40)));
            _bullets = new List<Bullet>();
            _ship = new Ship(new Point(0, Game.Width / 3), new Point(5, 0), new Size(60, 60));
        }

        static public void Draw()
        {
            Buffer.Graphics.DrawImage(background, 0, 0);
            foreach (BaseObject obj in _objs)
                obj?.Draw();
            foreach(BaseObject obj in _bullets)
                obj?.Draw();
            _ship.Draw();
            Buffer.Render();
            
        }
        
        static public void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            foreach (Bullet obj in _bullets) obj.Update();

            for(int i=0; i< _objs.Count; i++)
            {
                _objs[i].Update();
                for(int j=0; j<_bullets.Count; j++)
                if (_objs[i].Collision(_bullets[j]))
                {
                    _objs.RemoveAt(i);
                    i--;
                        _bullets.RemoveAt(j);
                    j--;
                        break;
                }
            }
                
            
        }
    }
}