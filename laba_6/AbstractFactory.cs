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
    public class AbstractFactory
    {
        public virtual CObject createBase(string code, Graphics g, int formX, int formY)
        {
            return null; //мы записываем return null,так как мы должы вернуть объект
        }
    }

    public class MyFactory : AbstractFactory
    {
        public override CObject createBase(string code, Graphics g, int formX, int formY)
        {
            CObject obj = null; 
            switch (code)
            {
                case "CSquare":
                    obj = new CSquare(0, 0);
                    break;
                case "CRectangle":
                    obj = new CRectangle(0, 0);
                    break;
                case "COval":
                    obj = new COval(0, 0);
                    break;
                case "CCircle":
                    obj = new CCircle(0, 0);
                    break;
                case "CGroup":
                    obj = new CGroup(g, new CObject[20], formX, formY);
                    break;
                default:
                    break;
            }

            return obj;
        }
    }
}
