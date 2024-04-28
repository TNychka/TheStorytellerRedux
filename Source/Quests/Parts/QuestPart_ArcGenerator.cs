using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class QuestPart_ArcGenerator : QuestPart_SubquestGenerator
    {
        protected override QuestScriptDef GetNextSubquestDef()
        {
            // TODO, input choice here? 
            #if DEBUG
            Log.Message("Arc Generator getting next quest");
            #endif
            subquestDefs.Shuffle();
            return subquestDefs.First();
        }

        protected override Slate InitSlate()
        {
            Slate slate = new Slate();
            return slate;
        }
    }
}