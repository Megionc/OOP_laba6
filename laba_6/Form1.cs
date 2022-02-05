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

        private void Form1_MouseClick(object sender, MouseEventArgs e)
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
					myStorage.setCircle(g, click.X, click.Y);
				}
			}
		}

        private void Form1_Paint(object sender, PaintEventArgs e)
		{
			g = CreateGraphics();
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
						myStorage.setCircle(g, X, Y);
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
				myStorage.rightSelected(ClientSize.Width);
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}
			if (e.KeyCode == Keys.Up)
			{
				myStorage.upSelected();
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}
			if (e.KeyCode == Keys.Down)
			{
				myStorage.downSelected(ClientSize.Height);
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}
			
			if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Up)
			{
				myStorage.increaseSelected(ClientSize.Width, ClientSize.Height);
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}

			if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Down)
			{
				myStorage.decreaseSelected();
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}
		}
	}



	public class CCircle
	{
		public int speed = 10;
		public int x;
		public int y;
		public int r;
		private Pen pen; // цвет контура круга
		private bool selected; // флаг, выбрал ли объект

		public CCircle(int x, int y)
		{
			r = 50;
			this.x = x - r;
			this.y = y - r;
			selected = true;
		}

		// функция, двигающая круг влево
		public void left() 
        {
			if (x-speed > 0)
			this.x = this.x - speed;
        }

		// функция, двигающая круг вправо
		public void right(int formRight)
		{
			if (this.x + r + r + speed < formRight)
				this.x = this.x + speed;
		}

		// функция, двигающая круг вверх
		public void up()
		{
			if (y-speed > 0)
			this.y = this.y - speed;
		}

		// функция, двигающая круг вниз
		public void down(int formDown)
		{
			if (this.y + r + r + speed < formDown)
				this.y = this.y + speed;
		}

		//функция, увеличивающая круг
		public void increase(int formUpX, int formUpY)
        {
			if (this.x + r + r + speed < formUpX)
				if (this.y + r + r + speed < formUpY)
						this.r = this.r + speed;
		}

		// функция, уменьшающая круг
		public void decrease()
        {
			if (this.r - speed > 0)
				this.r = this.r - speed;
		}

		// функция рисует круг
		public void showCircle(Graphics g)
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

		// возвращает флаг выбранности круга
		public bool isSelected()
		{
			return selected;
		}

		// функция проверяет кликнул ли пользователь внутрь круга, или нет
		// возвращает true - если внутри, false - иначе
		public bool checkCoord(int x, int y)
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
		private CCircle[] storage;// хранилище

		//конструктор
		public MyStorage(int size)
		{
			this.storage = new CCircle[size];
			for (int i = 0; i < size; i++)
			{
				this.storage[i] = null;
			}
			this.size = size;
		}
				
		//функция, двигающая все выбранные круги влево
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

		//функция, двигающая все выбранные круги вправо
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

		//функция, двигающая все выбранные круги вверх
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

		//функция, двигающая все выбранные круги вниз
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

		//функция, увеливающая все выбранные круги
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

		//функция, уменьшающая все выбранные круги
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

		// функция проверяет кликнул ли пользователь внутрь какого либо круга, или нет
		// если "внутрь круга", то возвращается индекс круга
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
		public void setCircle(Graphics g, int x, int y)
		{
			int emptyPosition = getEmptyPosition();
			if (emptyPosition == -1) // значит в массиве нет места для создания нового объекта
			{
				unSelectedObject();
				setObject(size, new CCircle(x, y));
				callShowMethod(g);

			}
			else
			{
				unSelectedObject();
				setObject(emptyPosition, new CCircle(x, y));
				callShowMethod(g);
			}

		}

		// добавить объект на указанную позицию
		// если хранилище меньше, чем заданная позиция,
		// то расширяем хранилище
		void setObject(int position, CCircle circle)
		{
			if (position < size)
			{
				storage[position] = circle;
			}
			else
			{
				int newSize = position + 1;
				Array.Resize(ref storage, newSize);
				storage[position] = circle;
				this.size = newSize;
			}

		}

		// получить объект на i-той позиции (без удаления)
		CCircle getObject(int i)
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

		//функция проходит по всему массиву и вызывает метод showCircle у всех объектов
		public void callShowMethod(Graphics g)
		{
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					storage[i].showCircle(g);
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
