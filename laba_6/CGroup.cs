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
    public class CGroup: CObject
    {
		MyStorage group;

		public CGroup(Graphics g, CObject[] obj, int formX, int formY)
		{
			this.group = new MyStorage(20);
			for (int i = 0; i < obj.Length; i++)
			{
				this.group.setCObject(g, obj[i]);
				this.group.unSelectedObject();
			}

			this.selected = true;

			this.pointMin = this.group.getMinPoint(formX, formY);
			this.pointMax = this.group.getMaxPoint();
		}

		// функция, двигающая группу влево
		public override void left()
		{
			if (this.pointMin.X - speed > 0)
            {
				this.pointMin.X = this.pointMin.X - speed;
				this.pointMax.X = this.pointMax.X - speed;

				this.group.leftObject();
			}
		}

		// функция, двигающая группу вправо
		public override void right(int formRight)
		{
			if (this.pointMax.X + speed < formRight)
			{
				this.pointMin.X = this.pointMin.X + speed;
				this.pointMax.X = this.pointMax.X + speed;

				this.group.rightObject(formRight);
			}
		}

		// функция, двигающая группу вверх
		public override void up()
		{
			if (this.pointMin.Y - speed > 0)
			{
				this.pointMin.Y = this.pointMin.Y - speed;
				this.pointMax.Y = this.pointMax.Y - speed;

				this.group.upObject();
			}
		}

		// функция, двигающая группу вниз
		public override void down(int formDown)
		{
			if (this.pointMax.Y + speed < formDown)
			{
				this.pointMin.Y = this.pointMin.Y + speed;
				this.pointMax.Y = this.pointMax.Y + speed;

				this.group.downObject(formDown);
			}
		}

		//функция, увеличивающая элементы группы
		public override void increase(int formUpX, int formUpY)
		{
			this.group.increaseObjects(formUpX, formUpY);
			this.pointMax = this.group.getMaxPoint();

			if (this.pointMax.X > formUpX || this.pointMax.Y > formUpY)
            {
				this.group.decreaseObjects();
				this.pointMax = this.group.getMaxPoint();
			}
		}

		// функция, уменьшающая элементы группы
		public override void decrease()
		{
			this.group.decreaseObjects();
			this.pointMax = this.group.getMaxPoint();
		}

		// функция, уменьшающая элементы группы
		public override bool isGroup()
		{
			return true;
		}

		// функция рисует элементы группы
		public override void showObject(Graphics g)
		{
			this.group.callShowMethod(g);

			if (selected)
			{
				pen = new Pen(Color.Red, 3);
			}
			else
			{
				pen = new Pen(Color.Black, 3);
			}

			g.DrawRectangle(pen, this.pointMin.X, this.pointMin.Y, this.pointMax.X - this.pointMin.X, this.pointMax.Y - this.pointMin.Y);
		}


		// функция проверяет кликнул ли пользователь внутрь группы, или нет
		// возвращает true - если внутри, false - иначе
		public override bool checkCoord(int x, int y)
		{
			bool checkX = this.pointMin.X < x && this.pointMax.X > x;
			bool checkY = this.pointMin.Y < y && this.pointMax.Y > y;

			return checkX && checkY;
		}

		// удалить все объекты в группе
		public void deleteObjects()
		{
			this.group.deleteAllObjects();
		}

		// получить все объекты в группе
		public CObject[] getObjects()
		{
			return this.group.getAllObjects();
		}

		// сохранить в файл
		public override void save(StreamWriter stream)
		{
			stream.WriteLine("CGroup");
			stream.WriteLine(this.group.getCount());
			stream.WriteLine(this.pointMin.X + " " + this.pointMin.Y + " " + this.pointMax.X + " " + this.pointMax.Y);
			for (int i = 0; i < this.group.getCount(); i++)
            {
				if (this.group.getObject(i) != null)
                {
					this.group.getObject(i).save(stream);
				}
            }
		}

		// загрузить данные группы
		public override void load(StreamReader stream, AbstractFactory factory, Graphics g, int formX, int formY)
		{
			int k = Convert.ToInt32(stream.ReadLine());
			string[] data = stream.ReadLine().Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

			this.pointMin.X = int.Parse(data[0]);
			this.pointMin.Y = int.Parse(data[1]);
			this.pointMax.X = int.Parse(data[2]);
			this.pointMax.Y = int.Parse(data[3]);

			for (int i = 0; i < k; i++)
			{
				string t = stream.ReadLine();
				if (this.group != null && t != null)
                {
					this.group.setCObject(g, factory.createBase(t, g, formX, formY));
					this.group.unSelectedObject();
					this.group.getObject(i).load(stream, factory, g, formX, formY);
                }
			}
		}
	}
}
