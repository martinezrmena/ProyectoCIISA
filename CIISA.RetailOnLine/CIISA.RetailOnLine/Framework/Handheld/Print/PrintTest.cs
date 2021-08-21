using Acr.UserDialogs;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Print
{
    public class PrintTest
    {
        public async Task testPrint(string pcomPort)
        {
            //pcomPort = "AC3FA4A165F6";
            Printer _objPrinter = new Printer(pcomPort);

            bool HayImpresora = false;

            HayImpresora = await _objPrinter.ValidarImpresorasConectadas();

            if (HayImpresora)
            {
                await _objPrinter.connect();

                List<string> Lineas = new List<string>();

                Lineas.Add(Document._firstLine);
                Lineas.Add(Environment.NewLine);
                Lineas.Add(Environment.NewLine);
                Lineas.Add("SISTEMA CHARGING ON LINE - PRUEBA IMPRESIÓN");
                Lineas.Add(Environment.NewLine);
                Lineas.Add(Environment.NewLine);
                Lineas.Add("000000000000000000000000000000000000000000000000");
                Lineas.Add("111111111111111111111111111111111111111111111111");
                Lineas.Add("222222222222222222222222222222222222222222222222");
                Lineas.Add("333333333333333333333333333333333333333333333333");
                Lineas.Add("444444444444444444444444444444444444444444444444");
                Lineas.Add("555555555555555555555555555555555555555555555555");
                Lineas.Add("666666666666666666666666666666666666666666666666");
                Lineas.Add("777777777777777777777777777777777777777777777777");
                Lineas.Add("888888888888888888888888888888888888888888888888");
                Lineas.Add("999999999999999999999999999999999999999999999999");
                Lineas.Add("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                Lineas.Add("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
                Lineas.Add("CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC");
                Lineas.Add("DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
                Lineas.Add("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                Lineas.Add("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
                Lineas.Add("GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
                Lineas.Add("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
                Lineas.Add("IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
                Lineas.Add("JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ");
                Lineas.Add("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
                Lineas.Add("LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
                Lineas.Add("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
                Lineas.Add("NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN");
                Lineas.Add("ÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑ");
                Lineas.Add("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
                Lineas.Add("PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
                Lineas.Add("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ");
                Lineas.Add("RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                Lineas.Add("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
                Lineas.Add("TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
                Lineas.Add("UUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
                Lineas.Add("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
                Lineas.Add("WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
                Lineas.Add("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Lineas.Add("YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");
                Lineas.Add("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
                Lineas.Add(Environment.NewLine);
                Lineas.Add(Environment.NewLine);
                Lineas.Add(Document._lastLine);
                Lineas.Add(Environment.NewLine);
                Lineas.Add(Environment.NewLine);
                Lineas.Add(Environment.NewLine);


                foreach (var item in Lineas)
                {
                    await _objPrinter.print(item);
                }

                _objPrinter.disconnect();
            }
        }
    }
}
