# C-Football-Players-MVC-2025
Hello, thanks for looking at my submission for this. I'm going to briefly list the reqs and how I met them + give details on what's needed to run this app locally. Uploaded the project with all its CSS so hopefully it looks as it should, can change the upload if needed.

Addiitonally, I implemented the API call values with local text files, i.e. you will need to put values into the empty local text files for the API call to work. I won't immediately share my API key just as good practice, but you can put your own in **'apiKey.txt'** (if you don't want to get your own, you can find mine in the file history for that text file here - it's a free one that only allows about a dozen requests per minute).

The URL used for development (and in my eyes meets the Reqs) should be present in **'footballURL.txt'**  and is the following: **https://api.football-data.org/v4/teams/57/matches**

Ideally these would have been values input by the user on a form, but time constraints prevented me from adding that feature.

**1 - REQS**

The requirements were as follows:

Backend Development

Requirements:

    a) Set up a SQL Server database with a table named Players with the following columns:
        ? PlayerId (Primary Key, auto-increment)
        ? PlayerName (string)
        ? Position (string: e.g., "Forward", "Midfielder", "Defender", "Goalkeeper")
        ? JerseyNumber (integer)
        ? GoalsScored (integer)
	
	DONE

    b) Create an ASP.NET MVC web application.

	DONE

    c) Create a Player model in the application corresponding to the Players table.

	DONE

    d) Develop a controller named PlayersController with actions to:
        ? Add a new player
        ? View all players
        ? Update an existing player's details using the PlayerId
        ? Remove a player using the PlayerId

	DONE
    e) Write appropriate SQL scripts and use ADO.NET or Entity Framework to interact with the database.

	DONE
 
2. Frontend Development
   
Requirements:

    a) Create a view that lists all players from the database in a Bootstrap-styled table using DataTables. The table should show: Player Name, Position, Jersey Number, and Goals Scored.

	DONE

    b) Add buttons or links next to each player to:
        ? Edit the player's details
        ? Remove the player
        ? Create a form to add a new player. Ensure it uses Bootstrap styling.

	DONE

    c) Use JavaScript, JQuery, etc to add form validation:
        ? Ensure PlayerName is not empty
        ? Ensure JerseyNumber is a unique integer

	DONE - Exception is that I set PlayerName to be non-nullable in its Model definiton so did not need to JS this 	req, and I should also say I'd prefer to keep things non-JS for reqs like this but did implement a JS solution 	for JerseyNumber being unique (though I feel it's a little hacky and not great code) 
 

4. Integration with APIs
   
Requirements:

    a) Create a RESTful API endpoint in your ASP.NET application to retrieve all players as a JSON list.

	DONE

    b) Use JavaScript to call this API endpoint and populate the players' table on the web page.

	DONE

    c) Add an external API integration:
        ? Integrate with Football-Data.org and surface Arsenal’s results from this season as well as their next 5 fixtures. You can use creative license to design this as you’d like. 
        ? https://www.football-data.org/ 

	DONE - This part of the app uses the following API call for Arsenal matches which is defined in the 	footballURL.txt file in root: https://api.football-data.org/v4/teams/57/matches

Bonus:
        ? Implement a feature to sort players by the number of goals scored.

	DONE - Enable by Default on List.cshtml

        ? Implement error handling and show meaningful error messages to the user.

	NOT DONE - Some error checking in places, but nothing susbtantial

        ? Create unit tests for the controller actions.
		
	NOT DONE - Time constraints precluded this
