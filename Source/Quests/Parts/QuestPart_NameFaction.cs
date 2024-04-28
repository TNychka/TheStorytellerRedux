using System;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace BST_TheStorytellerRedux
{
    public class QuestPart_NameFaction : QuestPart
    {
        public string inSignalEnable;
        public string outSingalComplete;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref inSignalEnable, "SignalEnable");
        }

        public override void Notify_QuestSignalReceived(Signal signal)
        {
            if (signal.tag == inSignalEnable)
            {
                #if DEBUG
                Log.Message("Name faction quest part activated");
                #endif
                NameFactionAndSettlement();
            }
        }

        private void NameFactionAndSettlement()
        {
            if (NamePlayerFactionAndSettlementUtility.CanNameFactionNow())
            {
                Settlement settlement = Find.WorldObjects.Settlements.Find(
                    (Predicate<Settlement>)(x => NamePlayerFactionAndSettlementUtility.CanNameSettlementSoon(x)));
                if (settlement != null)
                    Find.WindowStack.Add((Window)new Dialog_NamePlayerFactionAndSettlement(settlement));
                else
                    Find.WindowStack.Add((Window)new Dialog_NamePlayerFaction());
            }
            else
            {
                Settlement settlement = Find.WorldObjects.Settlements.Find(
                    (Predicate<Settlement>)(x => NamePlayerFactionAndSettlementUtility.CanNameSettlementNow(x)));
                if (settlement != null)
                {
                    if (NamePlayerFactionAndSettlementUtility.CanNameFactionSoon())
                        Find.WindowStack.Add((Window)new Dialog_NamePlayerFactionAndSettlement(settlement));
                    else
                        Find.WindowStack.Add((Window)new Dialog_NamePlayerSettlement(settlement));
                }
            }
        }
    }
}