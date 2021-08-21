using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Common.ValidateHH
{
    public static class DataTableValidate
    {
        public static async Task<bool> validateDataTable(DataTable pdt,string pmessage)
        {
            try
            {
                if (pdt.Rows != null)
                {
                    if (pdt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        await LogMessageError.generalError(pmessage);

                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch {
                return false;
            }
            
        }

        public static bool validateDataTable(DataTable pdt)
        {
            bool _value = false;

            if (pdt.Rows != null)
            {
                if (pdt.Rows.Count > 0)
                {
                    _value = true;
                }
                else
                {
                    _value = false;
                }
            }

            return _value;
        }

        public static bool validateDataTable(DataTable pdt,Log plog)
        {
            if (pdt.Rows != null)
            {
                if (pdt.Rows.Count > 0)
                {
                    plog.setDetailDataTableFilled(pdt.TableName);

                    return true;
                }
                else
                {
                    plog.setDetailDataTableEmpty(pdt.TableName);

                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool validateDataTable(DataTable pdt,string ptable,Editor ptextBox,Log plog)
        {
            if (pdt.Rows == null)
            {
                plog.addErrorLineDataTableNull(ptextBox, ptable);

                return false;
            }
            else
            {
                if (pdt.Rows.Count != 0 && pdt.Rows != null)
                {
                    return true;
                }
                else
                {
                    plog.addAlertLineDataTable(ptextBox, ptable);

                    return false;
                }
            }
        }
    }
}
