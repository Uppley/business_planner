using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BusinessPlanner.Utility
{
    class LoadingSpinner
    {
        private _LoadingDialog ld;
        private Form _context;
        public LoadingSpinner(Form f,string mssg)
        {
            ld = new _LoadingDialog(mssg);
            _context = f;
        }

        public void show()
        {
            Task.Factory.StartNew(() => {
                ld.ShowDialog();
            });
        }

        public void hide()
        {
            _context.Invoke(new MethodInvoker(() =>
            {
                ld.Close();
            }));
        }
    }
}
