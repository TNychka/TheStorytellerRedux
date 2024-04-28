using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class QuestNode_Root_TheStorytellerRedux : QuestNode
    {
        public List<QuestNode> rootNodes = new List<QuestNode>();
        public List<QuestPart> arcNodes = new List<QuestPart>();
        public QuestPart introNode = null;

        private ChoiceLetter_StorytellerIntro letterInfo;

        protected override void RunInt()
        {
#if DEBUG
            Log.Message("Init the storyteller's tale quest");
#endif

            Quest quest = QuestGen.quest;
            Slate slate = QuestGen.slate;

            foreach (QuestNode node in rootNodes)
            {
                node.Run();
            }

            Faction playerFaction;
            string playerFactionName = "DefaultColonyName".Translate();
            if (slate.TryGet("playerFaction", out playerFaction))
            {
                playerFactionName = playerFaction.Name;
            }

            string signalAccepted = QuestGenUtility.HardcodedSignalWithQuestID("Accepted");

            string title = "LetterLabelTheStoryBegins".Translate().CapitalizeFirst();
            string text = "LetterTextTheStoryBegins".Translate(playerFaction);

            letterInfo = (ChoiceLetter_StorytellerIntro)LetterMaker.MakeLetter(title, text,
                BSTChoiceLetterDefOf.BST_StorytellerIntro, quest: quest);
            letterInfo.signalAccepted = signalAccepted;
            letterInfo.playerFactionName = playerFactionName;

            Find.LetterStack.ReceiveLetter(letterInfo);
            letterInfo.OpenLetter();
        }

        protected override bool TestRunInt(Slate slate) => true;
    }
}