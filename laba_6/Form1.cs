using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_6
{
	public partial class Form1 : Form
	{
		MyStorage myStorage;
		Graphics g;
		Color colorForm = Color.Thistle; // цвет фона

		public Form1()
		{
			InitializeComponent();
			myStorage = new MyStorage(10);
			Size = new System.Drawing.Size(700, 500);

		}


        private void Form1_Paint(object sender, PaintEventArgs e)
		{
			g = panel1.CreateGraphics();
			g.Clear(colorForm);
			myStorage.callShowMethod(g);
		}		


		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				myStorage.deleteSelectedObject();
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}

			if (e.KeyCode == Keys.D1)
			{
				int X = 50, Y = 50;
				int choiceCircle = myStorage.checkCoord(X, Y);

				if (Control.ModifierKeys == Keys.Control)
				{
					if (choiceCircle != -1) // попадаем в объект
					{
						myStorage.setSelected(choiceCircle);
						myStorage.callShowMethod(g);
					}
				}
				else
				{
					if (choiceCircle != -1) // попадаем в объект
					{
						myStorage.unSelectedObject();
						myStorage.setSelected(choiceCircle);
						myStorage.callShowMethod(g);
					}
					else
					{
						myStorage.setCObject(g, new CCircle(X, Y));
					}
				}
			}

			if (e.KeyCode == Keys.D2)
            {
				int X = 0, Y = 0;
				int choiceRectangle = myStorage.checkCoord(X, Y);

				if (Control.ModifierKeys == Keys.Control)
				{
					if (choiceRectangle != -1) // попадаем в прямоугольник
					{
						myStorage.setSelected(choiceRectangle);
						myStorage.callShowMethod(g);
					}
				}
				else
				{
					if (choiceRectangle != -1) // попадаем в прямоугольник
					{
						myStorage.unSelectedObject();
						myStorage.setSelected(choiceRectangle);
						myStorage.callShowMethod(g);
					}
					else
					{
						myStorage.setCObject(g, new CRectangle(X, Y));
					}
				}
			}

			if (e.KeyCode == Keys.Left)
			{
				myStorage.leftSelected();
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}
			if (e.KeyCode == Keys.Right)
			{
				myStorage.rightSelected(panel1.Width);
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}
			if (e.KeyCode == Keys.Up)
			{
				if (Control.ModifierKeys == Keys.Control)
                {
					myStorage.increaseSelected(ClientSize.Width, ClientSize.Height);
					g.Clear(colorForm);
					myStorage.callShowMethod(g);
				}
                else
                {
					myStorage.upSelected();
					g.Clear(colorForm);
					myStorage.callShowMethod(g);
				}
						
			}
			if (e.KeyCode == Keys.Down)
			{
				if (Control.ModifierKeys == Keys.Control)
                {
					myStorage.decreaseSelected();
					g.Clear(colorForm);
					myStorage.callShowMethod(g);
				}
				else
                {
					myStorage.downSelected(panel1.Height);
					g.Clear(colorForm);
					myStorage.callShowMethod(g);
				}				
			}

			
			if (e.KeyCode == Keys.P)
			{
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
				myStorage.colorP(g);
			}

			if (e.KeyCode == Keys.O)
			{
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
				myStorage.colorO(g);
			}

			if (e.KeyCode == Keys.I)
			{
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
				myStorage.colorI(g);
			}
		}

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
			Point click;
			click = e.Location;
			int choiceCircle = myStorage.checkCoord(click.X, click.Y);

			if (Control.ModifierKeys == Keys.Control)
			{
				if (choiceCircle != -1) // попадаем в круг
				{
					myStorage.setSelected(choiceCircle);
					myStorage.callShowMethod(g);
				}
			}
			else
			{
				if (choiceCircle != -1) // попадаем в круг
				{
					myStorage.unSelectedObject();
					myStorage.setSelected(choiceCircle);
					myStorage.callShowMethod(g);
				}
				else
				{
					myStorage.setCObject(g, new CCircle(click.X, click.Y));
				}
			}
		}
    }

    public class CObject
	{
		public int x, y;
		public Pen pen; // цвет контура фигуры
		public bool selected; // флаг, выбрал ли объект

		public int speed = 10; //скорость перемещения объекта
		public Brushes brushe; // цвет фона объекта


		public virtual void showObject(Graphics g)
		{ }

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

		public virtual void left()
		{
		}

		public virtual void right(int formRight) { }

		public virtual void up() { }
		public virtual void down(int formDown) { }
		public virtual void increase(int formUpX, int formUpY) { }
		public virtual void decrease() { }
		public virtual void purpleObject(Graphics g)
		{
		}

		public virtual void orangeObject(Graphics g)
		{
		}

		public virtual void indigoObject(Graphics g)
		{
		}

	}

	public class CRectangle : CObject
	{
		public int width, height;

		public CRectangle(int x, int y)
		{
			width = 65;
			height = 50;
			this.x = x;
			this.y = y;
			selected = true;
		}

		// функция, двигающая прямоугольник влево
		public override void left()
		{
			if (x - speed > 0)
				x = x - speed;
		}

		// функция, двигающая прямоугольник вправо
		public override void right(int formRight)
		{
			if (x + width + speed < formRight)
				x = x + speed;
		}

		// функция, двигающая прямоугольник вверх
		public override void up()
		{
			if (y - speed > 0)
				y = y - speed;
		}

		// функция, двигающая прямоугольник вниз
		public override void down(int formDown)
		{
			if (y + height + speed < formDown)
				y = y + speed;
		}

		//функция, увеличивающая прямоугольник
		public override void increase(int formUpX, int formUpY)
		{
			if (x + width + speed < formUpX)
				if (y + height + speed < formUpY)
                {
					width = width + speed;
					height = height + speed;
				}					
		}

		// функция, уменьшающая прямоугольник
		public override void decrease()
		{
			if (height - speed > 0)
			{
				width = width - speed;
				height = height - speed;
			}
		}

		// функция рисует прямоугольник
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
		}

		public override void purpleObject(Graphics g)
		{
			g.FillRectangle(Brushes.Purple, x, y, width, height);
		}

		public override void orangeObject(Graphics g)
		{
			g.FillRectangle(Brushes.Orange, x, y, width, height);
		}

		public override void indigoObject(Graphics g)
		{
			g.FillRectangle(Brushes.Indigo, x, y, width, height);
		}

		// функция проверяет кликнул ли пользователь внутрь прямоугольника, или нет
		// возвращает true - если внутри, false - иначе
		public override bool checkCoord(int x, int y)
		{
			bool checkY = this.y < y && this.y + height > y;
			bool checkX = this.x < x && this.x + width > x;
			return checkY && checkX;
		}
	}
		

	public class CCircle : CObject
	{
		public int r;

		public CCircle(int x, int y)
		{
			r = 50;
			this.x = x - r;
			this.y = y - r;
			selected = true;
		}

		// функция, двигающая круг влево
		public override void left() 
        {
			if (x-speed > 0)
			this.x = this.x - speed;
        }

		// функция, двигающая круг вправо
		public override void right(int formRight)
		{
			if (this.x + r + r + speed < formRight)
				this.x = this.x + speed;
		}

		// функция, двигающая круг вверх
		public override void up()
		{
			if (y-speed > 0)
			this.y = this.y - speed;
		}

		// функция, двигающая круг вниз
		public override void down(int formDown)
		{
			if (this.y + r + r + speed < formDown)
				this.y = this.y + speed;
		}

		//функция, увеличивающая круг
		public override void increase(int formUpX, int formUpY)
        {
			if (this.x + r + r + speed < formUpX)
				if (this.y + r + r + speed < formUpY)
						this.r = this.r + speed;
		}

		// функция, уменьшающая круг
		public override void decrease()
        {
			if (this.r - speed > 0)
				this.r = this.r - speed;
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
		}

		public override void purpleObject(Graphics g)
        {
			g.FillEllipse(Brushes.Purple, x, y, r + r, r + r);
		}

		public override void orangeObject(Graphics g)
		{
			g.FillEllipse(Brushes.Orange, x, y, r + r, r + r);
		}

		public override void indigoObject(Graphics g)
		{
			g.FillEllipse(Brushes.Indigo, x, y, r + r, r + r);
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
	}

	class MyStorage
	{
		private int size;//размер массива
		private CObject[] storage;// хранилище

		//конструктор
		public MyStorage(int size)
		{
			this.storage = new CObject[size];
			for (int i = 0; i < size; i++)
			{
				this.storage[i] = null;
			}
			this.size = size;
		}
				
		//функция, двигающая все выбранные объекты влево
		public void leftSelected() 
		{
			for (int i = 0; i < size; i++)
            {
				if (storage[i]!=null && storage[i].isSelected())
                {
					this.storage[i].left();
                }
            }
		}

		//функция, двигающая все выбранные объекты вправо
		public void rightSelected(int formRight)
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null && storage[i].isSelected())
				{
					this.storage[i].right(formRight);
				}
			}
		}

		//функция, двигающая все выбранные объекты вверх
		public void upSelected()
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null && storage[i].isSelected())
				{
					this.storage[i].up();
				}
			}
		}

		//функция, двигающая все выбранные объекты вниз
		public void downSelected(int formDown)
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null && storage[i].isSelected())
				{
					this.storage[i].down(formDown);
				}
			}
		}

		//функция, увеливающая все выбранные объекты
		public void increaseSelected(int formX, int formY)
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null && storage[i].isSelected())
				{
					this.storage[i].increase(formX, formY);
				}
			}
		}

		//функция, уменьшающая все выбранные объекты
		public void decreaseSelected()
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null && storage[i].isSelected())
				{
					this.storage[i].decrease();
				}
			}
		}

		// получить размер хранилища
		int getCount()
		{
			return size;
		}

		// Получить индекс свободной ячейки.
		// Если такая ячейка найдена, то метод возвращает ее позицию.
		// Иначе возвращает -1
		protected int getEmptyPosition()
		{
			int position = -1;
			for (int i = 0; i < size; i++)
			{
				if (position == -1 && isEmptyPosition(i))
				{
					position = i;
				}
			}
			return position;
		}

		// функция проверяет кликнул ли пользователь внутрь какого либо объект, или нет
		// если "внутрь объекта", то возвращается индекс объекта
		// иначе -1
		public int checkCoord(int x, int y)
		{
			int result = -1;
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].checkCoord(x, y))
					{
						result = i;
					}
				}
			}
			return result;
		}

		//определяет на какую позицию добавить объект в массив
		public void setCObject(Graphics g, CObject newObj)
		{
			int emptyPosition = getEmptyPosition();
			if (emptyPosition == -1) // значит в массиве нет места для создания нового объекта
			{
				unSelectedObject();
				setObject(size, newObj);
				callShowMethod(g);

			}
			else
			{
				unSelectedObject();
				setObject(emptyPosition, newObj);
				callShowMethod(g);
			}
		}

		// добавить объект на указанную позицию
		// если хранилище меньше, чем заданная позиция,
		// то расширяем хранилище
		void setObject(int position, CObject obj)
		{
			if (position < size)
			{
				storage[position] = obj;
			}
			else
			{
				int newSize = position + 1;
				Array.Resize(ref storage, newSize);
				storage[position] = obj;
				this.size = newSize;
			}

		}

		// получить объект на i-той позиции (без удаления)
		CObject getObject(int i)
		{
			return storage[i];
		}

		// проверяет наличие объекта на i-той позици
		bool isEmptyPosition(int i)
		{
			bool result;
			if (storage[i] == null)
			{
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		//удаляет выбранные объекты
		public void deleteSelectedObject()
		{
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].isSelected())
					{
						storage[i] = null;
					}
				}
			}
		}

		//функция проходит по всему массиву и вызывает метод showObject у всех объектов
		public void callShowMethod(Graphics g)
		{
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					storage[i].showObject(g);
				}
			}
		}

		public void colorP (Graphics g)
		{
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].isSelected())
					{
						storage[i].purpleObject(g);
					}
				}
			}
		}

		public void colorO(Graphics g)
		{
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].isSelected())
					{
						storage[i].orangeObject(g);
					}
				}
			}
		}

		public void colorI(Graphics g)
		{
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].isSelected())
					{
						storage[i].indigoObject(g);
					}
				}
			}
		}

		// функция делает все объекты не выбранными
		public void unSelectedObject()
		{
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					storage[i].unSelected();
				}
			}
		}

		// функция делает конкретный объект выбранным
		// принимает позицию объекта который нужно выделить
		public void setSelected(int i)
		{
			storage[i].setSelected();
		}
	};

}
