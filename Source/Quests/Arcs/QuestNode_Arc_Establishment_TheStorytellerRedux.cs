using RimWorld;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class QuestNode_Arc_Establishment_TheStorytellerRedux : QuestNode_Arc_TheStorytellerRedux
    {
        private bool sentLetter = false;
        private ChoiceLetter_StoryArc letter;

        public void StartArc()
        {
            if (sentLetter)
            {
                return;
            }

            Find.LetterStack.ReceiveLetter(letter);
            letter.OpenLetter();
        }

        public override void Cleanup()
        {
            CloseLetter();
        }

        private void CloseLetter()
        {
            if (letter == null || !Find.LetterStack.LettersListForReading.Contains((Letter)this.letter))
            {
                return;
            }

            Find.LetterStack.RemoveLetter((Letter)letter);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref sentLetter, "sentLetter");
        }
    }
}