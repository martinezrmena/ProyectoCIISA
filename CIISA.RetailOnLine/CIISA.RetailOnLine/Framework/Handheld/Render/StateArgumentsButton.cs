namespace CIISA.RetailOnLine.Framework.Handheld.Render
{
    public class StateArgumentsButton : System.EventArgs
    {
        public bool v_enable;


        public StateArgumentsButton()
        {
        }

        public StateArgumentsButton(bool penable)
        {
            v_enable = penable;
        }


        public bool v_btnSend;
        public bool v_btnAbort;

        public StateArgumentsButton(bool pbtnSend, bool pbtnAbort)
        {
            v_btnSend = pbtnSend;
            v_btnAbort = pbtnAbort;
        }


        public bool v_btnUpload;
        public bool v_btnClose;

        public StateArgumentsButton(bool pbtnUpload, bool pbtnAbort, bool pbtnClose)
        {
            v_btnUpload = pbtnUpload;
            v_btnAbort = pbtnAbort;
            v_btnClose = pbtnClose;
        }

    }
}
