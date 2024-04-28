using RimWorld;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class QuestPart_SendLetter : QuestPart
    {
        public string inSignalEnable = null;
        public string outSingalComplete = null;

        private bool letterSent = false;
        
        public string titleString = "DefaultLetterTitle".Translate();
        public string labelString = "DefaultLetterLabel".Translate();
        public string textString = "DefaultLetterText".Translate();
        public string acceptOptionString = "DefaultLetterAcceptOption".Translate();
        public LetterDef letterDefOf = BSTChoiceLetterDefOf.BST_LetterAccept;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref inSignalEnable, "InSignalEnable");
            Scribe_Values.Look(ref outSingalComplete, "OutSignalComplete");
            Scribe_Values.Look(ref letterSent, "LetterSentBool");
        }

        public override void Notify_QuestSignalReceived(Signal signal)
        {
            if (inSignalEnable == null || signal.tag == inSignalEnable)
            {
                if (!letterSent)
                {
                    #if DEBUG
                        Log.Message("QuestPart SendLetter sending letter with title: " + titleString);
                    #endif 
                    letterSent = true;
                    SendLetter();
                }
            }
        }

        private void SendLetter()
        {
            ChoiceLetter_Accept letter = (ChoiceLetter_Accept)LetterMaker.MakeLetter(labelString, textString, letterDefOf, quest: quest);
            letter.signalAccept = outSingalComplete;
            letter.title = titleString;
            letter.acceptOptionString = acceptOptionString;

            Find.LetterStack.ReceiveLetter(letter);
            letter.OpenLetter();
        }
    }
}