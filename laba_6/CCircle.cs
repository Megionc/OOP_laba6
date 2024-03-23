using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using System.IO;

namespace laba_6
{
    public class CCircle: CObject
    {
		public int r;

		public CCircle(int x, int y)
		{
			r = 50;
			this.x = x - r;
			this.y = y - r;
			selected = true;

			this.pointMin = new Point(this.x, this.y);
			this.pointMax = new Point(this.x + this.r + this.r, this.y + this.r + this.r);
		}

		// функция, двигающая круг влево
		public override void left()
		{
			if (x - speed > 0)
            {
				this.x = this.x - speed;
				this.pointMin.X = this.pointMin.X - speed;
				this.pointMax.X = this.pointMax.X - speed;
			}
		}

		// функция, двигающая круг вправо
		public override void right(int formRight)
		{
			if (this.x + r + r + speed < formRight)
            {
				this.x = this.x + speed;
				this.pointMin.X = this.pointMin.X + speed;
				this.pointMax.X = this.pointMax.X + speed;
			}
				
		}

		// функция, двигающая круг вверх
		public override void up()
		{
			if (y - speed > 0)
            {
				this.y = this.y - speed;
				this.pointMin.Y = this.pointMin.Y - speed;
				this.pointMax.Y = this.pointMax.Y - speed;
			}
		}

		// функция, двигающая круг вниз
		public override void down(int formDown)
		{
			if (this.y + r + r + speed < formDown)
            {
				this.y = this.y + speed;
				this.pointMin.Y = this.pointMin.Y + speed;
				this.pointMax.Y = this.pointMax.Y + speed;
			}
		}

		//функция, увеличивающая круг
		public override void increase(int formUpX, int formUpY)
		{
			if (this.x + r + r + speed < formUpX)
				if (this.y + r + r + speed < formUpY)
                {
					this.r = this.r + speed / 2;
					this.pointMax.X = this.pointMax.X + speed;
					this.pointMax.Y = this.pointMax.Y + speed;
				}
		}

		// функция, уменьшающая круг
		public override void decrease()
		{
			if (this.r - speed > 0)
            {
				this.r = this.r - speed / 2;
				this.pointMax.X = this.pointMax.X - speed;
				this.pointMax.Y = this.pointMax.Y - speed;
			}
		}

		// функция рисует круг
		public override void showObject(Graphics g)
		{
			if (selected)
			{
				pen = new Pen(Color.Red, 3);
			}
			else
			{
				pen = new Pen(Color.Black, 3);
			}

			g.DrawEllipse(pen, x, y, r + r, r + r);
			if (color != Brushes.Thistle)
				g.FillEllipse(color, x, y, r + r, r + r);

		}

		// функция проверяет кликнул ли пользователь внутрь круга, или нет
		// возвращает true - если внутри, false - иначе
		public override bool checkCoord(int x, int y)
		{
			int x_center = this.x + r;
			int y_center = this.y + r;

			double distance = Math.Sqrt((x - x_center) * (x - x_center) + (y - y_center) * (y - y_center));

			return distance <= r;
		}

		// сохранить в файл
		public override void save(StreamWriter stream)
		{
			stream.WriteLine("CCircle");
			stream.WriteLine(this.pointMin.X + " " + this.pointMin.Y + " " + this.r + " " + this.colorForSave());
		}

		// загрузить данные квадрата
		public override void load(StreamReader stream, AbstractFactory factory, Graphics g, int formX, int formY)
		{
			string[] data = stream.ReadLine().Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
			this.x = int.Parse(data[0]);
			this.y = int.Parse(data[1]);
			this.pointMin.X = int.Parse(data[0]);
			this.pointMin.Y = int.Parse(data[1]);
			this.pointMax = new Point(this.x + this.r + this.r, this.y + this.r + this.r);
			this.r = int.Parse(data[2]);
			this.setColor(data[3]);
		}
	}
}
