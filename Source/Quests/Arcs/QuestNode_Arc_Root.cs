using System.Collections.Generic;
using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class QuestNode_Arc_Root : QuestNode
    {
        protected string arcStartSignal;
        protected string arcPartSuccessSignal;
        protected string arcSuccessSignal;

        private ChoiceLetter_Accept letter = null;

        // Config
        public List<QuestNode> rootNodes = new();

        protected string letterLabel = "DefaultArcLabel";
        protected string letterTitle = "DefaultArcTitle";
        protected string letterText = "DefaultArcText";

        protected override void RunInt()
        {
#if DEBUG
            Log.Message("Arc Base Init");
#endif

            Quest quest = QuestGen.quest;
            Slate slate = QuestGen.slate;

            foreach (QuestNode node in rootNodes)
            {
                node.Run();
            }

            arcStartSignal = QuestGen.GenerateNewSignal("ArcAccepted");
            arcPartSuccessSignal = QuestGen.GenerateNewSignal("ArcPartSuccess");
            arcSuccessSignal = QuestGen.GenerateNewSignal("ArcSuccess");


            QuestPart_PassAllActivable passAllQuestPart;
            if (!quest.TryGetFirstPartOfType(out passAllQuestPart))
            {
                passAllQuestPart = quest.AddPart<QuestPart_PassAllActivable>();
                passAllQuestPart.inSignalEnable = arcStartSignal;
            }
            passAllQuestPart.outSignalAny = arcPartSuccessSignal;
            passAllQuestPart.outSignalsCompleted.Add(arcSuccessSignal);

            List<string> questPartCompleteSignals = new List<string>();
            AddArcQuestParts(arcStartSignal, ref questPartCompleteSignals, ref quest);

            if (questPartCompleteSignals.Count > 0)
            {
                passAllQuestPart.inSignals.AddRange(questPartCompleteSignals);
            }
            else
            {
                Find.SignalManager.SendSignal(new Signal(arcSuccessSignal));
            }

            quest.End(QuestEndOutcome.Success, inSignal: arcSuccessSignal, sendStandardLetter: true);

            DispatchLetter(ref quest);
        }

        private void DispatchLetter(ref Quest quest)
        {
            string label = letterLabel.Translate();
            string title = letterTitle.Translate();
            string text = letterText.Translate();

            letter = (ChoiceLetter_Accept)LetterMaker.MakeLetter(label, text, BSTChoiceLetterDefOf.BST_LetterAccept,
                quest: quest);
            letter.title = title;
            letter.signalAccept = arcStartSignal;

            #if DEBUG
            Log.Message("Sending Arc Letter " + letter);
            #endif

            Find.LetterStack.ReceiveLetter(letter);
            letter.OpenLetter();
        }

        public virtual void AddArcQuestParts(string arcStartSignal, ref List<string> arcPartCompleteSignals, ref Quest quest)
        {
#if DEBUG
            Log.Message("Arc Base Start Arc");
#endif
        }

        protected override bool TestRunInt(Slate slate) => true;
    }
}