using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class ChoiceLetter_StorytellerIntro : ChoiceLetter
    {
        public string signalAccepted;
        public string playerFactionName;
        
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<string>(ref signalAccepted, "signalAccept");
            Scribe_Values.Look<string>(ref playerFactionName, "playerFactionName");
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
                ChoiceLetter_StorytellerIntro storyIntro = this;
                if (storyIntro.ArchivedOnly)
                {
                    yield return storyIntro.Option_Close;
                }
                else
                {
                    DiaOption optionA = new DiaOption("ChoiceAcceptStorytellerTale".Translate(playerFactionName));
                    optionA.action = (() =>
                            {
                                #if DEBUG
                                    Log.Message("Storyteller Intro Accepted");
                                #endif
                                Find.LetterStack.RemoveLetter(this);
                                Find.SignalManager.SendSignal(new Signal(signalAccepted));
                            }
                        );
                    optionA.resolveTree = true;
                    yield return optionA;
                }
            }
        }
    }
}