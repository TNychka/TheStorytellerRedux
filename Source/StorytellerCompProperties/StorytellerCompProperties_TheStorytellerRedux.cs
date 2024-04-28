using BST_TheStorytellerRedux;
using RimWorld;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class StorytellerCompProperties_TheStorytellerRedux : StorytellerCompProperties
    {
        public int IntervalsToStoryStart = 0;
        public StorytellerCompProperties_TheStorytellerRedux()
        {
            compClass = typeof (StorytellerComp_TheStorytellerRedux);
        }

        public override string ToString()
        {
            return base.ToString() 
                + "\nIntervalsToStoryStart " + IntervalsToStoryStart;
        }
    }
}