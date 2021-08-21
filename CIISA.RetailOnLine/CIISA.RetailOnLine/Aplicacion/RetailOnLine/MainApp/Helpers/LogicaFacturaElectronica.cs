using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers
{
    internal class LogicaFacturaElectronica
    {
        internal CodigoProveedor codigoProveedor = new CodigoProveedor();
        internal ValidateHH _validateHH = new ValidateHH();
        internal vistaFacturaElectronica view = null;
        internal Indicador _objIndicador = new Indicador();
        internal string v_TipoDocumento;

        public LogicaFacturaElectronica(vistaFacturaElectronica p_view, string TipoDocumento, string p_Pantalla) {

            view = p_view;
            v_TipoDocumento = TipoDocumento;

            if (p_Pantalla.Equals(PantallasSistema._pantallaCarniceria))
            {
                _objIndicador = view.ctrlCarniceria.v_objCliente.v_objEstablecimiento.v_objIndicador;
            }

            if (p_Pantalla.Equals(PantallasSistema._pantallaVisita))
            {
                _objIndicador = view.ctrlVisita.v_objCliente.v_objEstablecimiento.v_objIndicador;
            }

            view.FindByName<Label>("pnlFacturaElectronicalblTitle").Text = TipoDocumento;
            
        }

        internal void IndicadoresRestriction() {


            if (v_TipoDocumento.Equals(ROLTransactions._facturaCreditoNombre) || v_TipoDocumento.Equals(ROLTransactions._facturaContadoNombre)) {

                if (_objIndicador.v_codProveedor)
                {
                    view.FindByName<StackLayout>("pnlFacturaElectronicaProveedor").IsVisible = true;
                    FillNumeroProveedor();
                }

                if (_objIndicador.v_numOrden)
                {
                    view.FindByName<StackLayout>("pnlFacturaElectronicaNumeroOrdenCompra").IsVisible = true;
                }

                if (_objIndicador.v_fechaOrden)
                {
                    view.FindByName<StackLayout>("pnlFacturaElectronicaFechaOrdenCompra").IsVisible = true;
                }

                if (_objIndicador.v_numRecibo)
                {
                    view.FindByName<StackLayout>("pnlFacturaElectronicaNumeroRecibo").IsVisible = true;
                }

                if (_objIndicador.v_fechaRecibo)
                {
                    view.FindByName<StackLayout>("pnlFacturaElectronicaFechaRecibo").IsVisible = true;
                }

            }else if (v_TipoDocumento.Equals(ROLTransactions._devolucionNombre)){

                if (_objIndicador.v_codProveedor)
                {
                    view.FindByName<StackLayout>("pnlFacturaElectronicaProveedor").IsVisible = true;
                    FillNumeroProveedor();
                }

                if (_objIndicador.v_numReclamo)
                {
                    view.FindByName<StackLayout>("pnlFacturaElectronicaNumeroReclamo").IsVisible = true;
                }

                if (_objIndicador.v_fechaReclamo)
                {
                    view.FindByName<StackLayout>("pnlFacturaElectronicaFechaReclamo").IsVisible = true;
                }
            }
        }

        internal void FillNumeroProveedor()
        {
            List<string> Lista = new List<string>();
            Lista.Add(codigoProveedor.dryText);
            Lista.Add(codigoProveedor.FrozenText);

            view.FindByName<Picker>("pnlFacturaElectronicaPickNumeroProveedor").ItemsSource = Lista;
            view.FindByName<Picker>("pnlFacturaElectronicaPickNumeroProveedor").SelectedIndex = 0;
        }

        internal string CodProveedorSelection()
        {
            string cod = view.FindByName<Picker>("pnlFacturaElectronicaPickNumeroProveedor").SelectedItem.ToString();

            switch (cod)
            {
                case "Seco":
                    cod = codigoProveedor.dryValue;
                    break;

                case "Congelado":
                    cod = codigoProveedor.FrozenValue;
                    break;
            }

            return cod;
        }

        internal bool ValidarIndicadores() {

            bool validar = true;

            if (v_TipoDocumento.Equals(ROLTransactions._facturaCreditoNombre) || v_TipoDocumento.Equals(ROLTransactions._facturaContadoNombre))
            {
                if (_objIndicador.v_numOrden)
                {
                    if (_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtOrdenCompra")))
                    {
                        return false;
                    }
                }

                if (_objIndicador.v_numRecibo)
                {
                    if (_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtOrdenRecibo")))
                    {
                        return false;
                    }
                }

            } else if ( v_TipoDocumento.Equals(ROLTransactions._devolucionNombre)){

                if (_objIndicador.v_numReclamo)
                {
                    if (_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtReclamo")))
                    {
                        return false;
                    }
                }
            }

            

            return validar;
        }

        internal FacturaElectronica FacturaElectronicaData() {

            FacturaElectronica facturaElectronica = new FacturaElectronica();

            if (v_TipoDocumento.Equals(ROLTransactions._facturaContadoNombre) || v_TipoDocumento.Equals(ROLTransactions._facturaCreditoNombre))
            {

                if (_objIndicador.v_codProveedor)
                {
                    facturaElectronica.v_codProveedor = CodProveedorSelection();
                }

                if (_objIndicador.v_numOrden)
                {
                    facturaElectronica.v_numOrden = view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtOrdenCompra").Text;
                }

                if (_objIndicador.v_fechaOrden)
                {
                    facturaElectronica.v_fechaOrden = view.FindByName<DatePicker>("pnlFacturaElectronicadtpFechaOrden").Date;
                }

                if (_objIndicador.v_numRecibo)
                {
                    facturaElectronica.v_numRecibo = view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtOrdenRecibo").Text;
                }

                if (_objIndicador.v_fechaRecibo)
                {
                    facturaElectronica.v_fechaRecibo = view.FindByName<DatePicker>("pnlFacturaElectronicadtpFechaRecibo").Date;
                }

            }
            else if(v_TipoDocumento.Equals(ROLTransactions._devolucionNombre)) {

                if (_objIndicador.v_codProveedor)
                {
                    facturaElectronica.v_codProveedor = CodProveedorSelection();
                }

                if (_objIndicador.v_numReclamo)
                {
                    facturaElectronica.v_numReclamo = view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtReclamo").Text;
                }

                if (_objIndicador.v_fechaReclamo)
                {
                    facturaElectronica.v_fechaReclamo = view.FindByName<DatePicker>("pnlFacturaElectronicadtpFechaReclamo").Date;
                }

            }

            return facturaElectronica;
        }

        internal string mensaje_adicionales_Fe_II(FacturaElectronica facturaElectronica)
        {
            StringBuilder sb = new StringBuilder();

            if (v_TipoDocumento.Equals(ROLTransactions._facturaContadoNombre) || v_TipoDocumento.Equals(ROLTransactions._facturaCreditoNombre))
            {
                if (_objIndicador.v_numOrden == true)
                {
                    sb.Append("Número Orden: " + facturaElectronica.v_numOrden);
                    sb.Append(Environment.NewLine);
                }

                if (_objIndicador.v_fechaOrden == true)
                {
                    sb.Append("Fecha Orden: " + VarTime.dateCR(facturaElectronica.v_fechaOrden.Value));
                    sb.Append(Environment.NewLine);
                }

                if (_objIndicador.v_numRecibo == true)
                {
                    sb.Append("Número Recibo: " + facturaElectronica.v_numRecibo);
                    sb.Append(Environment.NewLine);
                }

                if (_objIndicador.v_codProveedor)
                {
                    sb.Append("Cód Proveedor: " + facturaElectronica.v_codProveedor);
                }
            }

            if (v_TipoDocumento.Equals(ROLTransactions._devolucionNombre))
            {
                if (_objIndicador.v_numReclamo == true)
                {
                    sb.Append("Número Reclamo: " + facturaElectronica.v_numReclamo);
                    sb.Append(Environment.NewLine);
                }

                if (_objIndicador.v_fechaReclamo == true)
                {
                    sb.Append("Fecha Reclamo: " + VarTime.dateCR(facturaElectronica.v_fechaReclamo.Value));
                    sb.Append(Environment.NewLine);
                }

                if (_objIndicador.v_codProveedor == true)
                {
                    sb.Append("Cód Proveedor: " + facturaElectronica.v_codProveedor);
                }
            }

            return sb.ToString();
        }

    }
}
