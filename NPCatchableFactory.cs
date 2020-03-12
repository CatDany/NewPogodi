using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPogodi
{
    interface NPCatchableFactory
    {
        /// <summary>
        /// Создаёт NPCatchable, характерный для данной фабрики.
        /// </summary>
        /// <returns></returns>
        NPCatchable Create(NPGame game);

        /// <summary>
        /// Возвращает относительную частоту, с которой для создания нового NPCatchable будет вызываться эта фабрика.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        double GetCommonness(NPGame game);

        /// <summary>
        /// Вызывается, когда NPCatchable пойман, до того как начислены очки (reward)
        /// </summary>
        /// <param name="catchable"></param>
        void OnCatch(NPCatchable catchable);

        /// <summary>
        /// Вызывается, когда NPCatchable пропущен, до того как отняты очки (penalty)
        /// </summary>
        /// <param name="catchable"></param>
        void OnMiss(NPCatchable catchable);
    }
}
