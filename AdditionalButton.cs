using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    public class AdditionalButton
    {
        private string _c;
        private string _n;
        public string command {
            get {                
                return this._c.Trim().Equals("")?"<no command>":this._c;
            }
            set
            {
                _c = value;
            }
                
               }
        public string name
        {
            get
            {
                
                return this._n.Trim().Equals("") ? "<no name>" : this._n;
            }
            set
            {

                _n = value;
            }
        }

        public AdditionalButton(string command="",string name="")
        {
            this.command = command;
            this.name = name;
        }
    }
}
