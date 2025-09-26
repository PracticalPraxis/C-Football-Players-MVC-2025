namespace ArsenalPlayers.Models
{
    public class APIModels
    {
        public class Match
        {
            public Area area { get; set; }
            public Competition competition { get; set; }
            public Season season { get; set; }
            public int id { get; set; }
            public DateTime utcDate { get; set; }
            public String status { get; set; }
            public String minute { get; set; }
            public String injuryTime { get; set; }
            public String attendance { get; set; }
            public String venue { get; set; }
            public int matchday { get; set; }
            public String stage { get; set; }
            public String group { get; set; }
            public DateTime lastUpdated { get; set; }
            public HomeTeam homeTeam { get; set; }
            public AwayTeam awayTeam { get; set; }
            public Score score { get; set; }
            public String[] goals { get; set; }
            public String[] penalties { get; set; }
            public String[] bookings { get; set; }
            public String[] substiutions { get; set; }
            public Odds odds { get; set; }
            public String[] referees { get; set; }
        }

        public class Area
        {
            public int id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public string flag { get; set; }

        }
        public class Competition
        {
            public int id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public string type { get; set; }
            public string emblem { get; set; }
        }
        public class Season
        {
            public int id { get; set; }
            public DateTime startDate { get; set; }

            public DateTime lastUendDatepdated { get; set; }

            public int currentMatchday { get; set; }

            public String winner { get; set; }

            public String[] stages { get; set; }

        }
        public class HomeTeam
        {
            public int id { get; set; }
            public String name { get; set; }
            public String shortName { get; set; }
            public String tla { get; set; }
            public String crest { get; set; }
            public Coach coache { get; set; }
            public String leagueRank { get; set; }
            public String formation { get; set; }
            public String[] lineup { get; set; }
            public String[] bench { get; set; }


        }
        public class AwayTeam : HomeTeam
        {

        }

        public class Coach
        {
            public int id { get; set; }
            public String name { get; set; }
            public String nationality { get; set; }
        }
        public class Score
        {
            public String winner { get; set; }
            public String duration { get; set; }
            public FullTime fulltime { get; set; }
            public HalfTime halftime { get; set; }

        }
        public class FullTime
        {
            public String home { get; set; }
            public String away { get; set; }
        }
        public class HalfTime : FullTime
        {

        }
        public class Odds
        {

            public String homeWin { get; set; }
            public String draw { get; set; }
            public String awayWin { get; set; }

        }

        public class APIKey
        {
            public String apiKey { get; set; }
        }

        public class IntermediaryAPIObject
        {
            public int id { get; set; }
            public DateTime utcDate { get; set; }
            public String status { get; set; }
            public int matchday { get; set; }
            public String stage { get; set; }
            public String group { get; set; }
            public DateTime lastUpdated { get; set; }
            public int areaid { get; set; }

            public String areaname { get; set; }

            public String areaflag { get; set; }

            public int competitionid { get; set; }

            public String competitionname { get; set; }

            public String competitioncode { get; set; }

            public String competitiontype { get; set; }

            public String competitionemblem { get; set; }

            public int seasonid { get; set; }

            public DateTime seasonstartStartDate { get; set; }

            public DateTime seasonstartEndDate { get; set; }

            public int seasoncurrentMatchday { get; set; }

            public String seasonwinner { get; set; }


            public int homeTeamid { get; set; }


            public String homeTeamname { get; set; }


            public String homeTeamshortName { get; set; }


            public String homeTeamtla { get; set; }


            public String homeTeamcrest { get; set; }


            public int awayTeamid { get; set; }

            public String awayTeamname { get; set; }

            public String awayTeamshortName { get; set; }

            public String awayTeamtla { get; set; }

            public String awayTeamcrest { get; set; }

            public String scorewinner { get; set; }

            public String scoreduration { get; set; }

            public String oddsmsg { get; set; }




        }
    }
}
