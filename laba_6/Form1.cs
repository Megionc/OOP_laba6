using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace laba_6
{
	public partial class Form1 : Form
	{
		MyStorage myStorage;
		Graphics g;
		Color colorForm = Color.Thistle; // цвет фона

		OpenFileDialog ofd;
		SaveFileDialog sfd;

		public Form1()
		{
			InitializeComponent();
			myStorage = new MyStorage(20);
			Size = new System.Drawing.Size(700, 500);

			ofd = new OpenFileDialog();
			sfd = new SaveFileDialog();

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
				for (int i = 0; i < myStorage.getCount(); i++)
				{
					if (!myStorage.isEmptyPosition(i))
					{
						if (myStorage.getObject(i).isSelected())
						{
							if (myStorage.getObject(i).isGroup())
							{
								CGroup gr = (CGroup)myStorage.getObject(i);
								gr.deleteObjects();
							}
							myStorage.setObject(i, null);
						}
					}
				}

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

			if (e.KeyCode == Keys.D3)
			{
				int X = 0, Y = 0;
				int choiceOval = myStorage.checkCoord(X, Y);

				if (Control.ModifierKeys == Keys.Control)
				{
					if (choiceOval != -1) // попадаем в овал
					{
						myStorage.setSelected(choiceOval);
						myStorage.callShowMethod(g);
					}
				}
				else
				{
					if (choiceOval != -1) // попадаем в овал
					{
						myStorage.unSelectedObject();
						myStorage.setSelected(choiceOval);
						myStorage.callShowMethod(g);
					}
					else
					{
						myStorage.setCObject(g, new COval(X, Y));
					}
				}
			}

			if (e.KeyCode == Keys.D4)
			{
				int X = 0, Y = 0;
				int choiceSquare = myStorage.checkCoord(X, Y);

				if (Control.ModifierKeys == Keys.Control)
				{
					if (choiceSquare != -1) // попадаем в квадрат
					{
						myStorage.setSelected(choiceSquare);
						myStorage.callShowMethod(g);
					}
				}
				else
				{
					if (choiceSquare != -1) // попадаем в квадрат
					{
						myStorage.unSelectedObject();
						myStorage.setSelected(choiceSquare);
						myStorage.callShowMethod(g);
					}
					else
					{
						myStorage.setCObject(g, new CSquare(X, Y));
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
					myStorage.increaseSelected(panel1.Width, panel1.Height);
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
				for (int i = 0; i < myStorage.getCount(); i++)
				{
					if (!myStorage.isEmptyPosition(i))
					{
						if (myStorage.getObject(i).isSelected())
						{
							if (myStorage.getObject(i) != null)
							{
								if (myStorage.getObject(i).isGroup())
								{
									CGroup gr = (CGroup)myStorage.getObject(i);
									CObject[] objs = gr.getObjects();
									for (int t = 0; t < objs.Length; t++)
									{
										if (objs[t] != null)
										{
											objs[t].purpleObject(g);
										}
									}
								}
								else
								{
									myStorage.getObject(i).purpleObject(g);
								}
							}
						}
					}
				}

				g.Clear(colorForm);
				myStorage.callShowMethod(g);				
			}

			if (e.KeyCode == Keys.O)
			{
				for (int i = 0; i < myStorage.getCount(); i++)
                {
					if (!myStorage.isEmptyPosition(i))
					{
						if (myStorage.getObject(i).isSelected())
						{
							if (myStorage.getObject(i) != null)
								{
								if (myStorage.getObject(i).isGroup())
									{
									CGroup gr = (CGroup)myStorage.getObject(i);
									CObject[] objs = gr.getObjects();
									for (int t = 0; t < objs.Length; t++)
                                    {
										if (objs[t] != null)
										{
											objs[t].orangeObject(g);
										}
									}
								}
								else
                                {
									myStorage.getObject(i).orangeObject(g);
								}
                            }
						}
					}
				}

				g.Clear(colorForm);
				myStorage.callShowMethod(g);				
			}

			if (e.KeyCode == Keys.I)
			{
				for (int i = 0; i < myStorage.getCount(); i++)
				{
					if (!myStorage.isEmptyPosition(i))
					{
						if (myStorage.getObject(i).isSelected())
						{
							if (myStorage.getObject(i) != null)
							{
								if (myStorage.getObject(i).isGroup())
								{
									CGroup gr = (CGroup)myStorage.getObject(i);
									CObject[] objs = gr.getObjects();
									for (int t = 0; t < objs.Length; t++)
									{
										if (objs[t] != null)
                                        {
											objs[t].indigoObject(g);
										}
									}
								}
								else
								{
									myStorage.getObject(i).indigoObject(g);
								}
							}
						}
					}
				}

				g.Clear(colorForm);
				myStorage.callShowMethod(g);				
			}

			if (e.KeyCode == Keys.G)
			{
				g.Clear(colorForm);
				createGroup(g);
				myStorage.callShowMethod(g);
			}

			if (e.KeyCode == Keys.Z)
			{
				g.Clear(colorForm);
				notGroup(g);
				myStorage.callShowMethod(g);
			}

			if (e.KeyCode == Keys.C)
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					FileStream file = new FileStream(sfd.FileName, FileMode.Create);
					StreamWriter stream = new StreamWriter(file);
					stream.WriteLine(myStorage.getCount());
					for (int i = 0; i < myStorage.getCount(); i++)
                    {
						if (myStorage.getObject(i) != null)
                        {
							myStorage.getObject(i).save(stream);
						}
					}
					stream.Close();
					file.Close();
				}
			}

			if (e.KeyCode == Keys.V)
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					FileStream file = new FileStream(ofd.FileName, FileMode.Open);

					StreamReader stream = new StreamReader(file);

					AbstractFactory factory = new MyFactory();

					int k = Convert.ToInt32(stream.ReadLine());

					for (int i = 0; i < k; i++)
					{
						string t = stream.ReadLine();
						//ддя чего нам createBase(t, g, panel1.Width, panel1.Height)???
						CObject tmp = factory.createBase(t, g, panel1.Width, panel1.Height);
						myStorage.setCObject(g, tmp);
						if (myStorage.getObject(i) != null)
							myStorage.getObject(i).load(stream, factory, g, panel1.Width, panel1.Height);
					}
					stream.Close();
					file.Close();
				}
				g.Clear(colorForm);
				myStorage.callShowMethod(g);
			}
		}

		void createGroup(Graphics g)
        {
			// получили выбранные объекты и удалили их из хранилища
			CObject[] selectedObj = myStorage.getSelectedAndDelete();

			CObject[] newGroup = new CObject[20];
			int position = 0;

			for (int i = 0; i < selectedObj.Length; i++)
            {
				if (selectedObj[i] != null)
                {
					if (selectedObj[i].isGroup())
					{
						CGroup gr = (CGroup)selectedObj[i];
						CObject[] objs = gr.getObjects();
						for (int k = 0; k < objs.Length; k++)
						{
							if (objs[k] != null)
                            {
								newGroup[position] = objs[k];
								position = position + 1;
							}
						}
					}
					else
					{
						newGroup[position] = selectedObj[i];
						position = position + 1;
					}
				}
            }
			CObject group = new CGroup(g, newGroup, panel1.Width, panel1.Height);
			myStorage.setCObject(g, group);
		}

		void notGroup(Graphics g)
        {
			for (int i = 0; i < myStorage.getCount(); i++)
            {
				if (myStorage.getObject(i) != null && myStorage.getObject(i).isSelected() && myStorage.getObject(i).isGroup())
                {
					CGroup gr = (CGroup)myStorage.getObject(i);
					CObject[] objs = gr.getObjects();
					// удалить группу
					myStorage.deleteObject(i);
					// добавили в хранилище все разгруппированные объекты
					for (int k = 0; k < objs.Length; k++)
                    {
						if (objs[i] != null)
                        {
							myStorage.setCObject(g, objs[k]);
						}
                    }
				}
            }
		}


		private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
			Point click;
			click = e.Location;
			int choiceObj = myStorage.checkCoord(click.X, click.Y);

			if (Control.ModifierKeys == Keys.Control)
			{
				if (choiceObj != -1) // попадаем в объект
				{
					myStorage.setSelected(choiceObj);
					myStorage.callShowMethod(g);
				}
			}
			else
			{
				if (choiceObj != -1) // попадаем в объект
				{
					myStorage.unSelectedObject();
					myStorage.setSelected(choiceObj);
					myStorage.callShowMethod(g);
				}
				else
				{
					//myStorage.setCObject(g, new CCircle(click.X, click.Y));
				}
			}
		}
    }


}
