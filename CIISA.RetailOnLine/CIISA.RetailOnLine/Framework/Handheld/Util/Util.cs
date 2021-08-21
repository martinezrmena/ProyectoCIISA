using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ValidateHH;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XLabs.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Util
{
    public class Util
    {
        public void fillComboBoxTodayAll(Picker pcomboBox)
        {
            pcomboBox.Items.Clear();

            pcomboBox.Items.Add(VarComboBox._cbxToday);
            pcomboBox.Items.Add(VarComboBox._cbxAll);

            pcomboBox.SelectedIndex = Numeric._zeroInteger;
        }

        public void fillComboBoxSearchForCodeAndDescription(Picker pcomboBox)
        {
            pcomboBox.Items.Clear();

            pcomboBox.Items.Add(VarComboBox._cbxCode);
            pcomboBox.Items.Add(VarComboBox._cbxDescription);

            pcomboBox.SelectedIndex = Numeric._zeroInteger;
        }

        public void fillComboBox(DataTable pdt,Picker pcomboBox,string prowName)
        {
            pcomboBox.Items.Clear();

            if (DataTableValidate.validateDataTable(pdt))
            {
                for (int i = Numeric._zeroInteger; i < pdt.Rows.Count; i++)
                {
                    pcomboBox.Items.Add(pdt.Rows[i][prowName].ToString());
                }

                pcomboBox.SelectedIndex = Numeric._zeroInteger;
            }
        }

        public string recordListInt(List<int> plist)
        {
            string _list = string.Empty;

            int i = 1;

            foreach (int _record in plist)
            {
                _list += _record;

                if (plist.Count > i)
                {
                    _list += ", ";
                }

                i++;
            }

            return _list.ToString();
        }

        public decimal sumarItemsColumnaLista<T>(ListView plistView, int pcolumnIndex)
        {
            decimal _total = Numeric._zeroInteger;

            var Source = plistView.ItemsSource as ObservableCollection<T>;

            //Sacando las propiedades de la clase (T)
            Type Tipo = typeof(T);
            var Propiedades = Tipo.GetProperties();
            List<string> NombreDePropiedades = new List<string>();
            foreach (PropertyInfo info in Propiedades)
            {
                NombreDePropiedades.Add(info.Name);
            }
            //Obteniendo la propiedad que nos importa
            string PropiedadClave = NombreDePropiedades[pcolumnIndex];

            if (Source != null)
            {
                if (Source.Count > 0)
                {
                    foreach (T _lvi in Source)
                    {
                        string[] Dato = _lvi as string[];

                        var PropiedadDeClase = _lvi.GetType().GetProperty(PropiedadClave);
                        string Valor = PropiedadDeClase.GetValue(_lvi).ToString();
                        _total += FormatUtil.convertStringToDecimal(Valor);
                        //_total += FormatUtil.convertStringToDecimal(Dato[pcolumnIndex]);
                    }
                }
            }
            else
            {
                var Source2 = plistView.ItemsSource as SelectableObservableCollection<T>;

                if (Source2 != null)
                {
                    if (Source2.Count > 0)
                    {
                        foreach (var item in Source2)
                        {
                            T Data = item.Data;
                            var PropiedadDeClase = Data.GetType().GetProperty(PropiedadClave);
                            string Valor = PropiedadDeClase.GetValue(Data).ToString();
                            _total += FormatUtil.convertStringToDecimal(Valor);
                        }
                    }
                }
            }

            return _total;
        }

        public void sumarItemsColumnaLista<T>(ListView plistView,Label pcolumn,int pcolumnIndex,string ptext)
        {
            decimal _total = Numeric._zeroInteger;

            var Source = plistView.ItemsSource as ObservableCollection<T>;

            //Sacando las propiedades de la clase (T)
            Type Tipo = typeof(T);
            var Propiedades = Tipo.GetProperties();
            List<string> NombreDePropiedades = new List<string>();
            foreach (PropertyInfo info in Propiedades)
            {
                NombreDePropiedades.Add(info.Name);
            }
            //Obteniendo la propiedad que nos importa
            string PropiedadClave = NombreDePropiedades[pcolumnIndex];


            if (Source != null)
            {
                if (Source.Count > 0)
                {
                    foreach (T _lvi in Source)
                    {
                        var PropiedadDeClase = _lvi.GetType().GetProperty(PropiedadClave);
                        string Valor = PropiedadDeClase.GetValue(_lvi).ToString();
                        _total += FormatUtil.convertStringToDecimal(Valor);
                    }
                }
            }
            else
            {
                var Source2 = plistView.ItemsSource as SelectableObservableCollection<T>;

                if (Source2 != null)
                {
                    if (Source2.Count > 0)
                    {
                        foreach (var item in Source2)
                        {
                            T Data = item.Data;
                            var PropiedadDeClase = Data.GetType().GetProperty(PropiedadClave);
                            string Valor = PropiedadDeClase.GetValue(Data).ToString();
                            _total += FormatUtil.convertStringToDecimal(Valor);
                        }
                    }
                }
            }

            string _amountTotal = FormatUtil.applyCurrencyFormat(_total);

            string _string = string.Empty;

            _string += ptext;
            _string += " [";
            _string += _amountTotal;
            _string += "]";

            pcolumn.Text = _string;
        }

        public void deleteItemComboBox(Picker pcomboBox,string pnameTypeTransaction)
        {
            for (int i = Numeric._zeroInteger; i < pcomboBox.Items.Count; i++)
            {
                string _value = pcomboBox.Items[i].ToString();

                if (_value.Equals(pnameTypeTransaction))
                {
                    pcomboBox.Items.RemoveAt(i);
                }
            }
        }

        public bool VerificaExiste(ListView plistView, int pindex, Producto producto)
        {
            bool existe = false;

            ValidateHH _validateHH = new ValidateHH();

            var Source = plistView.ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(plistView))
            {
                pnlTransacciones_ltvProductos _lvi = Source[pindex];

                var seleccionado = Source.Contains(_lvi);
                decimal cantidad = decimal.Parse(_lvi._pt_cantidad);

                if ((_lvi._pt_codigo == producto.v_codProducto) &&
                    (cantidad == producto.v_cantTransaccion))
                {
                    existe = true;
                }
            }

            return existe;
        }

        public bool VerificaExisteDR(ListView plistView, int pindex, Producto producto)
        {
            bool existe = false;

            ValidateHH _validateHH = new ValidateHH();

            var Source = plistView.ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(plistView))
            {
                pnlTransacciones_ltvProductos _lvi = Source[pindex];

                var seleccionado = Source.Contains(_lvi);
                decimal cantidad = decimal.Parse(_lvi._pt_cantidad);

                if (_lvi._pt_codigo == producto.v_codProducto)
                {
                    existe = true;
                }
            }

            return existe;
        }

        public void deleteElementListView<T>(ListView plistView,int pindex)
        {
            ValidateHH _validateHH = new ValidateHH();

            var Source = plistView.ItemsSource as ObservableCollection<T>;

            if (!_validateHH.emptyListView<T>(plistView))
            {
                T _lvi = Source[pindex];

                Source.Remove(_lvi);

                plistView.ItemsSource = Source;
            }
        }

        public async Task deleteElementListView<T>(ListView plistView)
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<T>(plistView))
            {
                bool _selectedItem = await _validateHH.listViewItemSelected(plistView);

                if (_selectedItem)
                {
                    var Source = plistView.ItemsSource as ObservableCollection<T>;

                    var seleccionado = plistView.SelectedItem;

                    var Convertido = (T)seleccionado;
                    
                    int _index = Source.IndexOf(Convertido);

                    var _lvi = Source[_index];

                    Source.Remove(_lvi);

                    plistView.ItemsSource = Source;
                }
            }
        }

        //public void evaluateAddNode(TreeView ptreeView,string pnodeText)
        public void evaluateAddNode(ListView ptreeView, string pnodeText, Color color)
        {
            var Source = ptreeView.ItemsSource as ObservableCollection<CollapsableItem>;

            //if (ptreeView.Nodes.Count == 0)
            if (Source.Count == 0)
            {
                CollapsableItem _tn = new CollapsableItem(pnodeText);
                //TreeNode _tn = new TreeNode(pnodeText);
                _tn.CustomTextColor = color;
                //ptreeView.Nodes.Add(_tn);
                Source.Add(_tn);
            }
            else
            {
                bool _add = false;

                //foreach (TreeNode _tn in ptreeView.Nodes)
                foreach (var _tn in Source)
                {
                    //string _value = _tn.Text.ToString();
                    string _value = _tn.Encabezado.ToString();

                    if (_value != pnodeText)
                    {
                        if (_value == pnodeText)
                        {
                            _add = false;
                        }
                        else
                        {
                            _add = true;
                        }
                    }
                    else
                    {
                        _add = false;
                    }
                }

                if (_add)
                {
                    CollapsableItem _tn = new CollapsableItem(pnodeText);
                    //TreeNode _tn = new TreeNode(pnodeText);
                    _tn.CustomTextColor = color;
                    //ptreeView.Nodes.Add(_tn);
                    Source.Add(_tn);

                    _add = false;
                }
            }

            ptreeView.ItemsSource = Source;
        }


        public void evaluateAddNode(ListView ptreeView, string pnodeText)
        {
            var Source = ptreeView.ItemsSource as ObservableCollection<CollapsableItem>;

            //if (ptreeView.Nodes.Count == 0)
            if (Source != null) {

                if (Source.Count == 0)
                {
                    CollapsableItem _tn = new CollapsableItem(pnodeText);
                    //TreeNode _tn = new TreeNode(pnodeText);

                    //ptreeView.Nodes.Add(_tn);
                    Source.Add(_tn);
                }
                else
                {
                    bool _add = false;

                    //foreach (TreeNode _tn in ptreeView.Nodes)
                    foreach (var _tn in Source)
                    {
                        //string _value = _tn.Text.ToString();
                        string _value = _tn.Encabezado.ToString();

                        if (_value != pnodeText)
                        {
                            if (_value == pnodeText)
                            {
                                _add = false;
                            }
                            else
                            {
                                _add = true;
                            }
                        }
                        else
                        {
                            _add = false;
                        }
                    }
                    if (_add)
                    {
                        CollapsableItem _tn = new CollapsableItem(pnodeText);
                        //TreeNode _tn = new TreeNode(pnodeText);

                        //ptreeView.Nodes.Add(_tn);
                        Source.Add(_tn);

                        _add = false;
                    }
                }

                ptreeView.ItemsSource = Source;
            }
            
        }

        public string recordListString_dataTable(DataTable pdt,string pfieldName)
        {
            string _list = string.Empty;

            int i = 1;

            foreach (DataRow _row in pdt.Rows)
            {
                _list += _row[pfieldName].ToString();

                if (pdt.Rows.Count > i)
                {
                    _list += ", ";
                }

                i++;
            }

            return _list.ToString();
        }

        public List<string> cloneList(List<string> plist)
        {
            List<string> newList = new List<string>();

            foreach (string _line in plist)
            {
                newList.Add(_line);
            }

            return newList;
        }
    }
}
