# TrueLayer PokemonAPI

0 - Running this Solution

    0.1 - Build and run the Dockerfile without Visual Studio:
        Go to the solution folder
        docker build . -t "truelayer" -f "TrueLayer.WebApi\Dockerfile"
        docker run -it --rm -p 5000:5000 --name truelayer truelayer 
        Go on your browser to http://localhost:5000/swagger it should works.

    0.2 - Run project through Visual Studio 2019.
        Open the solution file TrueLayer.sln.
        Requirements .NetCore 3.1.
        Run the solution on Docker or TrueLayer.WebApi.
        Go on your browser to http://localhost:5000/swagger it should works.

2 - Testing this Solution

    2.1 - Run Tests projects through Visual Studio 2019.
        Open the solution file TrueLayer.sln.
        Build it. In the Test Explorer Windows, both Integration and Unit Tests should appear.
        If you have a running solution, all the tests should pass.

3 - Database Interaction

    3.1 - Only stores and keeps in the database, the last searchs from Pokemon/{Name} Get Requests. 
        If you run it twice, you will notice that the result is persisted.
        If you re-run the solution in DEBUG, the database will be cleaned.

4 - Current Methods:

    4.1 http://localhost:5000/swagger/ - Get.
    To get the latest OpenApi swagger.json file for the WebApi.Project 

    4.2  http://localhost:5000/pokemon/{nameOfPokemon}/ - Get. 
    To see a Pokemon Shakespeare description 

    4.3 http://localhost:5000/pokemon/  - Get. 
    To see the list of Pokemon names, in case you don't know how a pokemon should be typed

