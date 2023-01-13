using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.Core;

namespace Zamba.Services
{
    public class SDynamicButtons
    {
        #region Miembros de IService
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.DocType;
        }
        #endregion

        DynamicButtonBusiness _businessIntance;

        public  SDynamicButtons()
        {
            _businessIntance = DynamicButtonBusiness.GetInstance();
        }

        /// <summary>
        /// Obtiene los botones de home que puede ver el usuario, evaluando si tiene o no permisos.
        /// </summary>
        /// <param name="currUser"></param>
        /// <returns></returns>
        public List<IDynamicButton> GetRuleHomeButtons(IUser currUser)
        {
            return _businessIntance.GetHomeButtons(currUser);
        }

        public List<IDynamicButton> GetRuleHeaderButtons(IUser user)
        {
            return _businessIntance.GetHeaderButtons(user);
        }
    }
}
