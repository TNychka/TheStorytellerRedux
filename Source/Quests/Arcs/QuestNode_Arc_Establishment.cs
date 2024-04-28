using System.Collections.Generic;
using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class QuestNode_Arc_Establishment : QuestNode_Arc_Root
    {
        public override void AddArcQuestParts(string arcStartSignal, ref List<string> arcPartCompleteSignals, ref Quest quest)
        {
            #if DEBUG
                Log.Message("Arc Establishment Adding Quest Parts");
            #endif

            string playerWealthSuccess = QuestGen.GenerateNewSignal("questPartPlayerWealthSuccess");
            QuestPart_PlayerWealth playerWealthQuestPart = new QuestPart_PlayerWealth
            {
                playerWealth = 20000
            };
            playerWealthQuestPart.outSignalsCompleted.Add(playerWealthSuccess);
            playerWealthQuestPart.inSignalEnable = arcStartSignal;
            quest.AddPart(playerWealthQuestPart);
            arcPartCompleteSignals.Add(playerWealthSuccess);
        }
    }
}