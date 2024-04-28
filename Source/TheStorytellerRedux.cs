using Verse;
using RimWorld;
using UnityEngine;

namespace BST_TheStorytellerRedux
{
    public class TheStorytellerReduxSettings : ModSettings
    {
        public bool testBool = false;
        public float testFloat = 0.0f;
        public override void ExposeData()
        {
            Scribe_Values.Look(ref testBool, "testBool");
            Scribe_Values.Look(ref testFloat, "testFloat");
            base.ExposeData();
        }
    }
    public class TheStorytellerRedux : Mod
    {
        private readonly TheStorytellerReduxSettings _settings;

        public TheStorytellerRedux(ModContentPack content) : base(content)
        {
            _settings = GetSettings<TheStorytellerReduxSettings>();
            
            #if DEBUG
                Log.Message("Core storyteller redux start");
            #endif
        }
        
        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("Setting a test bool", ref _settings.testBool, "this is the test bool tool tip");
            _settings.testFloat = listingStandard.Slider(_settings.testFloat, 100f, 300f);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "TheStorytellerRedux";
        }
    }
}