using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.Core;
using Zamba.Diagrams.Shapes;

namespace Zamba.Shapes.Interfaces
{
    interface IGenericRuleShape
    {
        /// <summary>
        /// Regla de un proceso graficada
        /// </summary>
        IRule Rule { get; set; }

        /// <summary>
        /// Etapa de Regla de un proceso graficada
        /// </summary>
        StepShape stepShape { get; set; }
    }
}
