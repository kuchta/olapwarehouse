using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infor.BI.Applications.OlapApi
{
    public class OlapObjectBase
    {
        private object _tag;

        public OlapObjectBase()
        {
            _tag = null;
        }

        public object Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                _tag = value;
            }
        }
    }
}
