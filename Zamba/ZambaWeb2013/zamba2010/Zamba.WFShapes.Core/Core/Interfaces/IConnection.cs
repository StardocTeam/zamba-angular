using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;


namespace Zamba.WFShapes {
	public interface IConnection : IDiagramEntity {

		IConnector From{
			get;
			set;
		}

		IConnector To{
			get;
			set;
		}
        long Id1{
            get;
            set;
        }
        long Id2{
            get;
            set;
        }
        Label label{get;set;}
	} 

}