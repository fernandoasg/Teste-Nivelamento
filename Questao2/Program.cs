using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questao2;
using Questao2.Models;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;

        bool team1 = true;
        bool hasPages = true;
        int page = 1;

        while (hasPages)
        {
            PaginatedResponse response = getNextPage(year, team, page, team1);

            var list = (JArray)response.data;
            List<FootballMatchInfo> footballInfoList = list.ToObject<List<FootballMatchInfo>>();
            
            foreach(var info in footballInfoList)
            {
                totalGoals += team1 ? Int32.Parse(info.team1goals) : Int32.Parse(info.team2goals);
            }

            page++;

            if (page > response.total_pages)
            {
                if (!team1)
                {
                    hasPages = false;
                }
                else
                {
                    page = 1;
                    team1 = false;
                }
            }
        }

        return totalGoals;
    }

    public static PaginatedResponse getNextPage(int year, string team, int page, bool team1)
    {
        string queryParams = "?year=" + year;

        if (team1)
        {
            queryParams += "&team1=" + team;
        }
        else
        {
            queryParams += "&team2=" + team;
        }

        queryParams += "&page=" + page;

        var result = HttpClientService.GetRequest(queryParams);

        PaginatedResponse response = JsonConvert.DeserializeObject<PaginatedResponse>(result.Result);

        return response;
    }
}