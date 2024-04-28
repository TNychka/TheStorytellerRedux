using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class ChoiceLetter_ArcSelection : ChoiceLetter
    {
        public string signalOptionA;
        public string signalOptionB;
        public string signalOptionC;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<string>(ref signalOptionA, "OptionA");
            Scribe_Values.Look<string>(ref signalOptionB, "OptionB");
            Scribe_Values.Look<string>(ref signalOptionC, "OptionC");
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
                ChoiceLetter_ArcSelection accept = this;
                if (accept.ArchivedOnly)
                {
                    yield return accept.Option_Close;
                }
                else
                {
                    // TODO
                    DiaOption optionA = new DiaOption("First Thing");
                    DiaOption optionB = new DiaOption("Second Thing");
                    DiaOption optionC = new DiaOption("Third Thing");

                    optionA.action = (() =>
                            {
                                Log.Message("Option First");
                                Find.LetterStack.RemoveLetter(this);
                                Find.SignalManager.SendSignal(new Signal(signalOptionA));
                            }
                        );
                    optionA.resolveTree = true;
                    optionB.action = (() =>
                            {
                                Log.Message("Option Third");
                                Find.LetterStack.RemoveLetter(this);
                                Find.SignalManager.SendSignal(new Signal(signalOptionB));
                            }
                        );
                    optionB.resolveTree = true;
                    optionC.action = (() =>
                            {
                                Log.Message("Option Third");
                                Find.LetterStack.RemoveLetter(this);
                                Find.SignalManager.SendSignal(new Signal(signalOptionC));
                            }
                        );
                    optionC.resolveTree = true;

                    yield return optionA;
                    yield return optionB;
                    yield return optionC;
                    yield return accept.Option_Postpone;
                }
            }
        }
    }
}