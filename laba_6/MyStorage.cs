using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace laba_6
{
    public class MyStorage
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
				if (storage[i] != null && storage[i].isSelected())
				{
					this.storage[i].left();
				}
			}
		}

		//функция, двигающая все объекты влево
		public void leftObject()
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null)
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

		//функция, двигающая все объекты вправо
		public void rightObject(int formRight)
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null)
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

		//функция, двигающая все объекты вверх
		public void upObject()
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null)
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

		//функция, двигающая все объекты вниз
		public void downObject(int formDown)
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null)
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

		//функция, увеливающая все объекты
		public void increaseObjects(int formX, int formY)
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null)
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

		//функция, уменьшающая все объекты
		public void decreaseObjects()
		{
			for (int i = 0; i < size; i++)
			{
				if (storage[i] != null)
				{
					this.storage[i].decrease();
				}
			}
		}

		// получить размер хранилища
		public int getCount()
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
		public void setObject(int position, CObject obj)
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

		// получить все объекты
		public CObject[] getAllObjects()
		{
			return storage;
		}

		// получить объект на i-той позиции (без удаления)
		public CObject getObject(int i)
		{
			return storage[i];
		}

		// проверяет наличие объекта на i-той позици
		public bool isEmptyPosition(int i)
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

		//удаляет все объекты
		public void deleteAllObjects()
		{
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					storage[i] = null;
				}
			}
		}

		//удаляет объект с выбранной позиции
		public void deleteObject(int i)
		{
			storage[i] = null;
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

		// Получить все выбранные объекты и удалить их
		public CObject[] getSelectedAndDelete()
		{
			// считаем количество выбранных объектов
			int countSelected = 0;
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].isSelected())
					{
						countSelected = countSelected + 1;
					};
				}
			}

			//создаем массив для записи в него выбранных объектов
			CObject[] selected = new CObject[countSelected];

			// заполняем созданный массив выбранными объектами
			int emptyPosition = 0;
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].isSelected())
					{
						selected[emptyPosition] = storage[i];
						emptyPosition = emptyPosition + 1;
						storage[i] = null;
					};
				}
			}

			return selected;
		}

		// Получить все выбранные объекты
		public CObject[] getSelected()
		{
			// считаем количество выбранных объектов
			int countSelected = 0;
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].isSelected())
					{
						countSelected = countSelected + 1;
					};
				}
			}

			//создаем массив для записи в него выбранных объектов
			CObject[] selected = new CObject[countSelected];

			// заполняем созданный массив выбранными объектами
			int emptyPosition = 0;
			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].isSelected())
					{
						selected[emptyPosition] = storage[i];
						emptyPosition = emptyPosition + 1;
					};
				}
			}

			return selected;
		}

		// получить минимальные координаты
		public Point getMinPoint(int formX, int formY)
		{
			int x = formX;
			int y = formY;

			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].pointMin.X < x)
                    {
						x = storage[i].pointMin.X;

					}
					if (storage[i].pointMin.Y < y)
					{
						y = storage[i].pointMin.Y;

					}
				}
			}

			return new Point(x, y);
		}

		// получить максимальные координаты
		public Point getMaxPoint()
		{
			int x = 0;
			int y = 0;

			for (int i = 0; i < size; i++)
			{
				if (!isEmptyPosition(i))
				{
					if (storage[i].pointMax.X > x)
					{
						x = storage[i].pointMax.X;

					}
					if (storage[i].pointMax.Y > y)
					{
						y = storage[i].pointMax.Y;

					}
				}
			}

			return new Point(x, y);
		}
	}
}
