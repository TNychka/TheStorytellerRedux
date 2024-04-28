using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class QuestNode_Root_TheStorytellerReduxIntro : QuestNode
    {
        public List<QuestNode> rootNodes = new List<QuestNode>();
        public QuestScriptDef introQuestScriptDef = null;
        public int maximumActiveSubquests = 1;
        public int maximumSuccessfulSubquests = 1;
        public int intervalMin = 1;
        public int intervalMax = 1;

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

            string introAcceptedSignal = QuestGenUtility.HardcodedSignalWithQuestID("IntroAccepted");
            string introCompletedSignal = QuestGenUtility.HardcodedSignalWithQuestID("IntroArcCompleted");
            string introArcCompletedSignal = QuestGenUtility.HardcodedSignalWithQuestID("IntroCompleted");
            string playerFactionName = GetPlayerFactionName(slate);

            QuestPart_SendLetter questPartSendLetter = new QuestPart_SendLetter
            {
                outSingalComplete = introAcceptedSignal,
                titleString = "IntroLetterTitle",
                labelString = "IntroLetterLabel",
                textString = "IntroLetterText",
                letterDefOf = BSTChoiceLetterDefOf.BST_LetterAccept,
            };
            quest.AddPart(questPartSendLetter);

            QuestPart_ArcGenerator introArcGenerator = new QuestPart_ArcGenerator();
            introArcGenerator.subquestDefs.Add(introQuestScriptDef);
            introArcGenerator.interval = new IntRange(intervalMin, intervalMax);
            introArcGenerator.maxActiveSubquests = maximumActiveSubquests;
            introArcGenerator.maxSuccessfulSubquests = maximumSuccessfulSubquests;
            introArcGenerator.inSignalEnable = introAcceptedSignal;
            introArcGenerator.outSignalsCompleted.Add(introArcCompletedSignal);
            quest.AddPart(introArcGenerator);

            QuestPart_SendLetter questPartSendLetterComplete = new QuestPart_SendLetter
            {
                inSignalEnable = introArcCompletedSignal,
                outSingalComplete = introArcCompletedSignal,
                titleString = "IntroCompleteLetterTitle".Translate(),
                labelString = "IntroCompleteLetterLabel".Translate(),
                textString = "IntroCompleteLetterText".Translate(playerFactionName),
                acceptOptionString = "IntroCompleteLetterAcceptOption".Translate(playerFactionName),
                letterDefOf = BSTChoiceLetterDefOf.BST_LetterAccept,
            };
            quest.AddPart(questPartSendLetterComplete);

            QuestPart_NameFaction nameFactionReward = new QuestPart_NameFaction();
            nameFactionReward.inSignalEnable = introArcCompletedSignal;
            nameFactionReward.outSingalComplete = introCompletedSignal;
            quest.AddPart(nameFactionReward);

            quest.End(QuestEndOutcome.Success, inSignal: introCompletedSignal, sendStandardLetter: true);
        }

        private static String GetPlayerFactionName(Slate slate)
        {
            Faction playerFaction;
            string playerFactionName = "DefaultColonyName".Translate();
            if (slate.TryGet("playerFaction", out playerFaction))
            {
                playerFactionName = playerFaction.Name;
            }

            return playerFactionName;
        }


        protected override bool TestRunInt(Slate slate) => true;
    }
}