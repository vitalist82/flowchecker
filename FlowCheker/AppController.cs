using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker
{
    class AppController
    {
        IForm form;

        public AppController(IForm form)
        {
            this.form = form;
            Init();
        }

        public void Init()
        {
            LoadSettings();
            this.form.StartEvent += Form_StartEvent;
        }

        private void LoadSettings()
        {

        }

        private void Form_StartEvent(object sender, EventArgs e)
        {
            
        }
    }
}
