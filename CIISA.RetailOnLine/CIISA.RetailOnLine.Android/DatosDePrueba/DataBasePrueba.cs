using CIISA.RetailOnLine.DatosDePrueba;
using CIISA.RetailOnLine.Droid.DatosDePrueba;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataBasePrueba))]
namespace CIISA.RetailOnLine.Droid.DatosDePrueba
{
    public class DataBasePrueba : IDataBasePrueba
    {
        public void InicializarBaseDeDatosDePrueba()
        {
            string fileName = "BDSROL.db3";

            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string destFile = Path.Combine(targetPath, fileName);

            try
            {
                if (!File.Exists(destFile))
                {                    
                    using (var br = new BinaryReader(Forms.Context.Assets.Open(fileName)))
                    {
                        using (var bw = new BinaryWriter(new FileStream(destFile, FileMode.Create)))
                        {
                            byte[] buffer = new byte[2048];
                            int length = 0;
                            while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bw.Write(buffer, 0, length);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}