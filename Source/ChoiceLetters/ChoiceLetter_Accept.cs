using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class ChoiceLetter_Accept : ChoiceLetter
    {
        public string signalAccept = null;
        public string acceptOptionString = "DefaultLetterAcceptOption".Translate();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref signalAccept, "SignalAccept");
        }

        public override bool CanDismissWithRightClick => false;

        public override bool CanShowInLetterStack
        {
            get
            {
                if (!base.CanShowInLetterStack || quest == null)
                {
                    return false;
                }

                return quest.State is QuestState.Ongoing or QuestState.NotYetAccepted;
            }
        }

        public override IEnumerable<DiaOption> Choices
        {
            get
            {
                ChoiceLetter_Accept accept = this;
                if (accept.ArchivedOnly)
                {
                    yield return accept.Option_Close;
                }
                else
                {
                    DiaOption acceptOption = new DiaOption(acceptOptionString);
                    acceptOption.action = (() =>
                            {
                                #if DEBUG
                                    Log.Message("Letter Accepted, sending signal " + signalAccept);
                                #endif
                                Find.LetterStack.RemoveLetter(this);
                                Find.SignalManager.SendSignal(new Signal(signalAccept));
                            }
                        );
                    acceptOption.resolveTree = true;
                    yield return acceptOption;
                }
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Signal Accept: " + signalAccept;
        }
    }
}