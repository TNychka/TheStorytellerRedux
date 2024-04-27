using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class StorytellerComp_TheStorytellerReduxIntro : StorytellerComp
    {
        protected int IntervalsPassed => Find.TickManager.TicksGame / 1000;

        public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
        {
            // TODO, make intro to the storyteller intro incident series 
            // TODO, is allowedTargetTags enough?
            StorytellerComp_TheStorytellerReduxIntro source = this;
            if (source.IntervalsPassed == 1)
            {
                yield return new FiringIncident();
            }
        }
    }
}