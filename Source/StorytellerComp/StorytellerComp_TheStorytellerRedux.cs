using System.Collections.Generic;
using System.Linq;
using System;
using RimWorld;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class StorytellerComp_TheStorytellerRedux : StorytellerComp
    {
        private int IntervalsPassed => Find.TickManager.TicksGame / 1000;

        private StorytellerCompProperties_TheStorytellerRedux Props
        {
            get => (StorytellerCompProperties_TheStorytellerRedux)this.props;
        }

        public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
        {
            StorytellerComp_TheStorytellerRedux source = this;
            if (source.IntervalsPassed ==  Props.IntervalsToStoryStart) // TODO bump up this interval
            {
                IncidentDef storytellerIntro = BSTIncidentDefOf.BST_GiveQuest_TheStorytellersTale;
#if DEBUG
                Log.Message("Storyteller Intro Incident Begin. " + storytellerIntro);
#endif
                yield return new FiringIncident(storytellerIntro, source)
                {
                    parms =
                    {
                        target = target
                    }
                };
            }
        }
    }
}