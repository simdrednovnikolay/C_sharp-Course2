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
        static BaseObject[] _objs;

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
            timer.Start();
            Load();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        
        static public void Load()
        {
            Random rnd = new Random();

            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length / 2; i++)
                _objs[i] = new BaseObject(new Point(rnd.Next(0,799),rnd.Next(10,500)), new Point(0, 0), new Size(30, 30));   
            for (int i = 15; i < _objs.Length; i++)
                _objs[i] = new Star(new Point(800, i+100), new Point(rnd.Next(0-i,0), 0), new Size(0, 0)); //new Point(800, i+100), new Point(0-i, 0), new Size(0, 0)
        }

        static public void Draw()
        {
            Buffer.Graphics.DrawImage(background, 0, 0);
            foreach (BaseObject obj in _objs)
                obj?.Draw();
            Buffer.Render();
        }
        
        static public void Update()
        {
            foreach (BaseObject obj in _objs)
                obj?.Update();

        }
    }
}