using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Droid.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using Xamarin.Forms;
using System.Reflection;
using System.Collections;

[assembly: Dependency(typeof(FillDataSet_Table))]
namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Connection
{
    public class FillDataSet_Table : IFillDataSet_Table
    {

        public void FillTable(DataSet pds, DataTable tabla) {

            pds.Tables.Add(tabla);
        }

        public DataTable DataTable_Format(DataTable tabla)
        {
            DataTable TableFormat = new DataTable();

            foreach (DataRow item in tabla.Rows)
            {
                DataRow _dataRow = TableFormat.NewRow();
                for (int i = 0; i < tabla.Columns.Count; i++)
                {
                    object data = item[i];

                    DataColumn _dataColumn = new DataColumn();
                    _dataColumn.ColumnName = tabla.Columns[i].ColumnName;
                    _dataColumn.DataType = typeof(string);

                    if (string.IsNullOrEmpty(data.ToString()))
                    {
                        data = string.Empty;
                    }

                    if (!TableFormat.Columns.Contains(tabla.Columns[i].ColumnName))
                    {
                        TableFormat.Columns.Add(_dataColumn);
                    }
                    TableFormat.Columns[i].ColumnName = tabla.Columns[i].ColumnName;
                    _dataRow[tabla.Columns[i].ColumnName] = data;
                }
                TableFormat.Rows.Add(_dataRow);
            }

            return TableFormat;
        }

        #region Bitacora
        public DataTable DataTable_FormatBitacora(pnlBitacoraModel datos)
        {
            DataTable TableFormat = new DataTable();
            List<string> Columns = datos.GetType().GetProperties().Select(p => p.Name).ToList();

            List<string> tabla = objToListBitacora(datos);

            DataRow _dataRow = TableFormat.NewRow();

            for (int i = 0; i < Columns.Count; i++)
            {
                object data = tabla[i];

                DataColumn _dataColumn = new DataColumn();
                _dataColumn.ColumnName = Columns[i];
                _dataColumn.DataType = typeof(string);

                if (string.IsNullOrEmpty(data.ToString()))
                {
                    data = string.Empty;
                }

                if (!TableFormat.Columns.Contains(Columns[i]))
                {
                    TableFormat.Columns.Add(_dataColumn);
                }
                TableFormat.Columns[i].ColumnName = Columns[i];
                _dataRow[Columns[i]] = data;
            }

            TableFormat.Rows.Add(_dataRow);
            TableFormat.AcceptChanges();

            return TableFormat;
        }

        private List<string> objToListBitacora(pnlBitacoraModel obj)
        {
            List<string> values = new List<string>();
            values.Add(obj.Cod_Cliente);
            values.Add(obj.FechaVisita);
            values.Add(obj.Vol_Compra);
            values.Add(obj.Porcentaje_Compra);
            values.Add(obj.SituacionNegocio);
            values.Add(obj.Quejas);
            values.Add(obj.Oportunidades);
            values.Add(obj.Competencias);

            return values;
        }
        #endregion

        public DataTable DataTable_Format_EncDoc(DataTable tabla) {

            DataTable TableFormat = new DataTable();
            
            foreach (DataRow item in tabla.Rows)
            {
                DataRow _dataRow = TableFormat.NewRow();
                for (int i = 0; i < tabla.Columns.Count; i++)
                {
                    object data = item[i];

                    DataColumn _dataColumn = new DataColumn();
                    _dataColumn.ColumnName = tabla.Columns[i].ColumnName;
                    _dataColumn.DataType = typeof(string);

                    if (!TableFormat.Columns.Contains(tabla.Columns[i].ColumnName))
                    {
                        TableFormat.Columns.Add(_dataColumn);
                    }
                    TableFormat.Columns[i].ColumnName = tabla.Columns[i].ColumnName;

                    if (string.IsNullOrEmpty(data.ToString()))
                    {
                        data = string.Empty;
                    }
                    
                    _dataRow[tabla.Columns[i].ColumnName] = data;


                }
                TableFormat.Rows.Add(_dataRow);
            }

            return TableFormat;
        }

        public bool ExistsCampo(DataTable tabla, string campo, string column)
        {
            var filtered = tabla.Select(column + " LIKE '%" + campo + "%'");

            if (filtered.Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}