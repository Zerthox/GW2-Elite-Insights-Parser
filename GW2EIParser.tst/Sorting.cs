﻿using GW2EIEvtcParser.ParsedData;
using NUnit.Framework;
using System.Collections;

namespace GW2EIEvtcParser.tst.Internals;

sealed class Sorting
{
    public sealed class TestEvent(long t, int order) : TimeCombatEvent(t)
    {
        public int Order = order;

        public override string ToString() => $"({Time} {Order})";


        public sealed class Comparer : IComparer
        {
            public static readonly Comparer Instance = new();
            public int Compare(object? x, object? y)
            {
                return (int)((TestEvent)x!).Time.CompareTo(((TestEvent)y!).Time);
            }
        }
    }

    [Test]
    public void StableSortByTime_AbstractTimeCombatEvent()
    {
        //NOTE(Rennorb): Robustly testing this is actually quite hard, as the default implementation will still work for a bunch of cases.
        // But this should at least catch obvious issues.
        var list = new List<TestEvent>() {
            new(1, 3),
            new(0, 1),
            new(1, 4),
            new(0, 2),
        };

        list.SortByTime();

        for(int i = 0; i < list.Count - 1; i++)
        {
            var a = list[i];
            var b = list[i + 1];
            Assert.That(a.Time <= b.Time, "SortByTime must sort by time ascending");
            Assert.Less(a.Order, b.Order, "SortByTime must be a stable sort");
        }
    }


    sealed class TestCombatItem(long t, int v) : CombatItem(t, default, default, v, default, default, default, default, default, default, default, 
            default, default, default, default, default, default, default, default, default, default, default, default, default)
    {
        public override string ToString()
        {
            return $"({Time} {Value})";
        }
    }

    [Test]
    public void StableSortByTime_CombatItem()
    {
        //NOTE(Rennorb): Robustly testing this is actually quite hard, as the default implementation will still work for a bunch of cases.
        // But this should at least catch obvious issues.
        var list = new List<TestCombatItem>() {
            new(1, 3),
            new(0, 1),
            new(1, 4),
            new(0, 2),
        };

        list.SortByTime();

        for(int i = 0; i < list.Count - 1; i++)
        {
            var a = list[i];
            var b = list[i + 1];
            Assert.That(a.Time <= b.Time, "SortByTime must sort by time ascending");
            Assert.Less(a.Value, b.Value, "SortByTime must be a stable sort");
        }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    sealed class TestCastEvent(long t, bool swap, int order) : CastEvent(t, new(swap), null!)
    {
        public int Order = order;

        public override string ToString()
        {
            return $"({Time} {Skill.IsSwap} {Order})";
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete

    [Test]
    public void StableSortByTimeThenSwap()
    {
        //NOTE(Rennorb): Robustly testing this is actually quite hard, as the default implementation will still work for a bunch of cases.
        // But this should at least catch obvious issues.
        var list = new List<TestCastEvent>() {
            new(0, false, 0),
            new(1, false, 3),
            new(0, true , 1),
            new(1, true , 5),
            new(0, true , 2),
            new(1, false, 4),
        };

        list.SortByTimeThenSwap();

        for(int i = 0; i < list.Count - 1; i++)
        {
            var a = list[i];
            var b = list[i + 1];
            Assert.That(a.Time < b.Time || (a.Time == b.Time && Convert.ToInt32(a.Skill.IsSwap) <= Convert.ToInt32(b.Skill.IsSwap)), "SortByTimeThenSwap must sort by time ascending, then by isSwap");
            Assert.Less(a.Order, b.Order, "SortByTimeThenSwap must be a stable sort");
        }
    }

    [Test]
    public void StableSortByTimeThenNegatedSwap()
    {
        //NOTE(Rennorb): Robustly testing this is actually quite hard, as the default implementation will still work for a bunch of cases.
        // But this should at least catch obvious issues.
        var list = new List<TestCastEvent>() {
            new(0, true , 0),
            new(1, true , 3),
            new(0, true , 1),
            new(0, false, 2),
            new(1, false, 4),
            new(1, false, 5),
        };

        list.SortByTimeThenNegatedSwap();

        for(int i = 0; i < list.Count - 1; i++)
        {
            var a = list[i];
            var b = list[i + 1];
            Assert.That(a.Time < b.Time || (a.Time == b.Time && Convert.ToInt32(a.Skill.IsSwap) >= Convert.ToInt32(b.Skill.IsSwap)), "SortByTimeThenNegatedSwap must sort by time ascending, then by not isSwap");
            Assert.Less(a.Order, b.Order, $"SortByTimeThenNegatedSwap must be a stable sort. index {i} (+1)");
        }
    }

    //TODO(Rennorb) @cleanup: this might need to run multiple times because the branch depends a bit on the data
    [Test]
    public void Branch2()
    {
        var r = new Random();
        //  260031, 116634, 261071
        int len = 3000;
        var list = new List<TestEvent>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(new(r.Next(), i));
        }
        list.SortByTime();

        CollectionAssert.IsOrdered(list, TestEvent.Comparer.Instance);
    }

     [Test]
    public void Branch5()
    {
        int len = 1288;
        #region cursed
        var list = new List<long>(len) { -48,432,832,1274,1757,2606,3041,4128,5088,5638,6076,6432,7356,7808,8160,8716,9154,9526,10717,11163,11530,12077,12286,13233,13686,14043,14115,14594,14752,16802,17207,18489,18925,19367,19845,20327,20636,21391,21886,22370,22852,23311,23812,24403,25165,25558,26005,26159,26648,27487,27530,27977,28405,29094,30045,30606,31035,31035,31392,31846,32473,32928,32928,33297,33838,34294,34294,34294,34649,35240,35978,36396,36396,36396,36753,37314,37404,38360,38435,38919,39768,40927,41315,42083,42514,42996,43485,43961,44437,44894,46167,46646,47118,47153,47643,48242,49012,49520,49962,50325,50814,51921,52369,52761,53528,54482,55032,55494,55494,55494,55837,56207,56879,57325,57325,57325,57681,58240,58683,58683,59034,59569,60241,60675,60675,61038,61606,62035,63010,63495,64337,65612,66017,66765,67209,67693,68642,69481,69724,70232,71714,72488,72961,73439,73601,74010,74436,74608,75089,75164,76012,76445,77034,77644,78609,79316,79886,80318,86726,93880,94360,94841,95326,95686,96444,96799,97202,97686,98164,98643,99120,100262,101617,102093,102970,103536,104378,104793,105173,105807,106772,107334,107769,108247,108496,109176,109606,109970,110518,110957,111324,111919,112598,113045,113392,113961,114324,115280,115280,115764,116236,116236,117088,118155,118155,118559,118559,118760,119517,119517,120481,120481,120961,120961,121440,121440,121917,121917,122402,123165,123165,123643,124128,124593,124758,124758,125233,125848,126607,126715,126715,127112,127112,127570,127684,127684,128161,128207,128207,129042,129042,129477,129927,130569,131529,132072,132525,132887,133285,133971,134412,134769,135324,135768,136115,136675,137367,137798,138166,138727,138999,139960,139960,140446,140528,140528,141361,142551,142551,142965,143730,143730,144674,145175,145645,146114,146552,147314,147807,148283,148768,148877,148877,149365,149967,150724,150724,151112,151112,151564,151564,152049,152295,152295,153120,153239,153239,154154,154768,155724,156274,156715,157084,157476,158128,158570,158924,159493,159930,160286,160881,161491,161926,162299,162299,162842,163120,164086,164086,164563,164563,165395,168164,168164,168552,169317,169317,170289,170769,171243,171710,172206,172964,173440,173916,174435,174435,174929,175516,175763,176529,176529,176962,177449,177497,177497,177879,178361,178361,179197,179197,179925,180780,181569,182532,182532,182532,183077,183520,183520,183885,183885,184288,185084,185532,185532,185886,185886,186436,187760,188564,189357,189799,189799,190164,190164,190719,190766,190766,190766,191718,191815,192298,192321,193159,194368,194757,195517,198528,200403,202842,203608,204196,204644,205635,206524,207446,208154,208154,208154,209118,209118,209118,209684,210116,210116,210488,210883,211610,212046,212399,212960,213401,213756,214272,214969,215405,215756,216330,216410,216410,216410,217364,217408,217880,218037,218877,219958,221135,224527,226040,227970,228396,228922,229119,229683,230649,231090,231564,232361,232402,232402,232402,233364,233364,233927,234374,234719,235041,235839,236286,236646,237204,237635,238000,238573,239324,239759,240120,240688,241088,241088,242035,243116,243596,244076,244313,244797,245289,245770,246245,246723,247200,247682,248168,248644,249114,249613,249675,249885,250000,250113,251036,251437,251883,251918,252410,253770,253919,254362,254955,255689,255689,256652,256652,257201,257637,257999,258410,259078,259520,259883,260445,260883,261236,261800,262233,262605,265352,266399,267037,267475,267840,268395,268759,269725,270153,270515,270677,271515,272118,272600,273176,273679,274845,275292,275758,276244,276725,277198,278805,279283,279808,280286,280874,282368,283314,283794,283881,284722,285155,285603,286293,287241,287800,288236,288616,289672,291044,293088,294452,295441,297931,300035,300520,301194,301680,302165,304127,305003,306133,306896,313197,315009,319358,321771,324875,326196,326690,327157,327638,328122,329875,330486,332409,338131,339875,341235,343320,344694,352121,355554,357171,357768,359247,361915,363735,365105,366433,370926,371359,371796,373212,373794,376012,377096,378368,379277,379770,380237,380714,381195,383164,383655,383714,386326,386808,389051,391612,393410,259078,259520,259883,260445,260883,261236,261800,262233,262605,265352,266399,267037,267475,267840,268395,268759,269725,270153,270515,270677,271515,272118,272600,273176,273679,274845,275292,275758,276244,276725,277198,278805,279283,279808,280286,280874,282368,283314,283794,283881,284722,285155,285603,286293,287241,287800,288236,288616,289672,291044,293088,294452,295441,297931,300035,300520,301194,301680,302165,304127,305003,306133,306896,313197,315009,319358,321771,324875,326196,326690,327157,327638,328122,329875,330486,332409,338131,339875,341235,343320,344694,352121,355554,357171,357768,359247,361915,363735,365105,366433,370926,371359,371796,373212,373794,376012,377096,378368,379277,379770,380237,380714,381195,383164,383655,383714,386326,386808,389051,391612,393410,395087,396770,398803,417565,419169,420060,421398,423054,423526,424015,424485,424959,425248,426001,426482,426965,427453,427925,428399,428734,429493,429960,430445,430928,434015,311009,311689,312648,314396,315443,317885,318675,319798,320166,320327,321277,321845,322673,324478,325766,329996,330650,331919,332553,332968,334047,334894,335920,336597,337569,338554,338919,339274,340335,340680,341683,342035,342560,343762,344150,344847,345804,346284,347126,348156,349320,352881,354008,354486,356337,356729,357291,357959,358806,359770,360364,361369,362371,362733,363097,364166,364515,365520,365879,366544,372234,373329,373842,374687,375444,376693,377846,378834,383992,385448,385912,386363,387285,387769,388606,389474,390089,391038,392049,392410,392765,394001,394368,396084,397200,397568,397844,399313,400333,409393,414062,416610,416934,418000,418360,418676,419217,420407,420842,422172,422659,423102,431459,431927,432805,433448,434440,434815,438680,439725,440086,440519,441917,442005,442929,443395,444092,445758,446364,447354,447440,447882,448235,449252,450317,452282,453283,454517,455877,457236,457357,458573,460440,461954,462569,463930,464559,465653,467093,468296,469963,472003,472915,475285,475926,478007,478395,479353,480210,481170,481795,482760,483777,484557,485610,487136,487916,488834,490048,490810,491887,492366,493211,494449,495597,496038,499371,501041,502917,503402,504281,507686,508766,509690,510569,511357,512317,513330,514095,515336,516698,517282,518916,2317,2317,2317,2317,3085,13393,13393,13394,13685,14170,14312,16325,16326,18203,18394,25804,26205,27130,27131,27131,27132,27895,37634,37634,37634,38040,38400,38525,40444,40446,41846,42083,49810,50414,51603,51604,51605,51605,52369,62219,62220,62221,62477,62954,63010,65114,65115,66643,66837,74123,74812,75567,75568,75569,75570,76330,88642,88642,88642,88955,88955,89403,94888,95032,95326,96796,103970,103971,103971,103971,104794,114560,114561,114562,114885,115313,115916,117798,117799,120047,120366,127294,127805,128642,128643,128644,128644,129411,139208,139208,139209,139520,139960,140161,142168,142171,143612,144916,151157,151880,152847,152848,152849,152849,153605,163333,163333,163334,163680,164086,164086,167246,168882,169127,176893,177851,179577,179578,179579,179579,180319,190994,190994,190995,191289,191766,192057,193884,193886,195581,195804,203353,204074,205325,206236,206237,206237,206238,207009,216635,216635,216638,217001,217408,217614,219566,219568,221133,221249,228643,229167,230041,230042,230042,230042,230801,241280,241280,241280,241796,241797,242035,243035,243196,243436,243725,253645,253646,253647,253647,254410,260365,269395,269395,269396,269809,270153,270315,273236,273237,274640,274887,282842,283531,284288,284289,284290,284290,285053,294755,294755,294756,295167,295516,295608,298009,298010,299633,299956,306774,307724,308534,308534,308535,308535,309292,320564,320564,320565,320893,321327,321559,323400,323403,325563,325806,331432,333656,334538,334539,334539,334539,335285,345073,345073,345074,345321,345851,345888,347766,347767,349320,349479,356797,357522,358323,358323,358324,358324,359093,372364,372607,372609,372930,373373,373534,376300,376301,379317,379610,386612,387285,388092,388093,388093,388093,388835,398476,398476,398477,398853,399246,416243,417692,418094,418335,418886,420059,420060,420817,421118,421121,430612,430612,430613,430614,430614,430928,431370,431480,433969,434125,442393,442994,447123,447124,447882,463208,463208,463209,463563,463971,464011,466721,466723,468651,468897,473973,474807,479009,479930,479930,479931,479932,480676,491053,491054,491056,491397,491811,491934,493959,493960,495800,496016,507325,508366,509284,509285,509286,509286,510034,3205,16325,28114,40444,52440,65113,76681,88955,104967,117798,129641,142168,153797,167246,180497,193884,207117,219566,231278,241796,254634,273236,285274,298009,310816,323400,335554,347766,359446,376300,389170,421118,432478,466721,480805,493958,510166 };
        #endregion
        StableSort<long>.fluxsort(list.AsSpan(), (a, b) => a.CompareTo(b));

        CollectionAssert.IsOrdered(list);
    }
}


[TestFixtureSource(typeof(Sort_Generated), nameof(GenerateTests))]
sealed class Sort_Generated
{
    public static IEnumerable GenerateTests
    {
        get
        {
            var r = new Random();
            var list = new List<Sorting.TestEvent>(1000);
            for(int i = 0; i < 1000; i++)
            {
                list.Add(new(r.Next(), i));
                yield return new TestFixtureData(new List<Sorting.TestEvent>(list)); // clone so every list has to be sorted
            }
        }
    }


    List<Sorting.TestEvent> data;
    public Sort_Generated(List<Sorting.TestEvent> data) => this.data = data;

    [Test]
    public void IsSorted()
    {
        data.SortByTime();

        CollectionAssert.IsOrdered(data, Sorting.TestEvent.Comparer.Instance, $"{data.Count} failed");
    }
}