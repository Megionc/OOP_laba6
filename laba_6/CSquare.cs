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
    public class CSquare: CObject
    {
		public int width, height;

		public CSquare(int x, int y)
		{
			this.width = 50;
			this.height = width;
			this.x = x;
			this.y = y;
			selected = true;

			this.pointMin = new Point(this.x, this.y);
			this.pointMax = new Point(this.x + this.width, this.y + this.height);
		}

		// функция, двигающая квадрат влево
		public override void left()
		{
			if (x - speed > 0)
            {
				x = x - speed;
				this.pointMin.X = this.pointMin.X - speed;
				this.pointMax.X = this.pointMax.X - speed;
			}	
		}

		// функция, двигающая квадрат вправо
		public override void right(int formRight)
		{
			if (x + width + speed < formRight)
            {
				x = x + speed;
				this.pointMin.X = this.pointMin.X + speed;
				this.pointMax.X = this.pointMax.X + speed;
			}
		}

		// функция, двигающая квадрат вверх
		public override void up()
		{
			if (y - speed > 0)
            {
				y = y - speed;
				this.pointMin.Y = this.pointMin.Y - speed;
				this.pointMax.Y = this.pointMax.Y - speed;
			}
				
		}

		// функция, двигающая квадрат вниз
		public override void down(int formDown)
		{
			if (y + height + speed < formDown)
            {
				y = y + speed;
				this.pointMin.Y = this.pointMin.Y + speed;
				this.pointMax.Y = this.pointMax.Y + speed;
			}
		}

		//функция, увеличивающая квадрат
		public override void increase(int formUpX, int formUpY)
		{
			if (x + width + speed < formUpX)
				if (y + height + speed < formUpY)
				{
					width = width + speed;
					height = height + speed;
					this.pointMax.X = this.pointMax.X + speed;
					this.pointMax.Y = this.pointMax.Y + speed;
				}
		}

		// функция, уменьшающая квадрат
		public override void decrease()
		{
			if (height - speed > 0)
			{
				width = width - speed;
				height = height - speed;
				this.pointMax.X = this.pointMax.X - speed;
				this.pointMax.Y = this.pointMax.Y - speed;
			}
		}

		// функция рисует квадрат
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

			g.DrawRectangle(pen, x, y, width, height);
			if (color != Brushes.Thistle)
				g.FillRectangle(color, x, y, width, height);
		}


		// функция проверяет кликнул ли пользователь внутрь квадрат, или нет
		// возвращает true - если внутри, false - иначе
		public override bool checkCoord(int x, int y)
		{
			bool checkY = this.y < y && this.y + height > y;
			bool checkX = this.x < x && this.x + width > x;
			return checkY && checkX;
		}

		// сохранить в файл
		public override void save(StreamWriter stream)
		{
			stream.WriteLine("CSquare");
			stream.WriteLine(this.pointMin.X + " " + this.pointMin.Y + " "+ this.width + " " + this.colorForSave());
		}

		// загрузить данные квадрата
		public override void load(StreamReader stream, AbstractFactory factory, Graphics g, int formX, int formY)
		{
			string[] data = stream.ReadLine().Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
			this.x = int.Parse(data[0]);
			this.y = int.Parse(data[1]);
			this.pointMin.X = int.Parse(data[0]);
			this.pointMin.Y = int.Parse(data[1]);
			this.pointMax = new Point(this.x + this.width, this.y + this.height);
			this.width = int.Parse(data[2]);
			this.height = int.Parse(data[2]);
			this.setColor(data[3]);
		}
	}
}
