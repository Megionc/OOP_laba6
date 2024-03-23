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
    public class CObject
    {
		public int x, y;
		public Pen pen; // цвет контура фигуры
		public bool selected; // флаг, выбрал ли объект

		public int speed = 10; //скорость перемещения объекта
		public Brush color = Brushes.Thistle; // цвет фона объекта

		public Point pointMin;
		public Point pointMax;

		public virtual void showObject(Graphics g) { }

		public virtual bool checkCoord(int x, int y)
		{
			return false;
		}

		// помечает объект как не выбранный
		public void unSelected()
		{
			selected = false;
		}

		//делает объект выбранным
		public void setSelected()
		{
			selected = true;
		}

		// возвращает флаг выбранности объекта
		public bool isSelected()
		{
			return selected;
		}

		public void purpleObject(Graphics g)
		{
			color = Brushes.Purple;
		}

		public void orangeObject(Graphics g)
		{
			color = Brushes.Orange;
		}

		public void indigoObject(Graphics g)
		{
			color = Brushes.Indigo;
		}

		public virtual bool isGroup()
		{
			return false;
		}

		public Point getpointMin()
		{
			return pointMin;
		}

		public Point getpointMax()
		{
			return pointMax;
		}

		public string colorForSave()
		{
			if (this.color == Brushes.Purple)
			{
				return "Purple";
			}
			if (this.color == Brushes.Orange)
			{
				return "Orange";
			}
			if (this.color == Brushes.Indigo)
			{
				return "Indigo";
			}
			if (this.color == Brushes.Thistle)
			{
				return "Thistle";
			}
			return "";
		}

		public void setColor(string color)
		{
			if (color == "Purple")
            {
				this.color = Brushes.Purple;
			}
			if (color == "Orange")
			{
				this.color = Brushes.Orange;
			}
			if (color == "Indigo")
			{
				this.color = Brushes.Indigo;
			}
			if (color == "Thistle")
			{
				this.color = Brushes.Thistle;
			}
		}

		public virtual void left() { }
		public virtual void right(int formRight) { }
		public virtual void up() { }
		public virtual void down(int formDown) { }
		public virtual void increase(int formUpX, int formUpY) { }
		public virtual void decrease() { }
		public virtual void save(StreamWriter stream) {return;}
		public virtual void load(StreamReader stream, AbstractFactory factory, Graphics g, int formX, int formY)
		{
			return;
		}
	}
}
